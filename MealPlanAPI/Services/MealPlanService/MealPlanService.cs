using AutoMapper;
using MealPlanAPI.Models.DTOs.MealPlan;
using Microsoft.EntityFrameworkCore;


public class MealPlanService : IMealPlanService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public MealPlanService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<MealPlanDto> CreateMealPlanAsync(CreateMealPlanDto createDto)
    {
        // Validação do paciente
        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.Id == createDto.PatientId && !p.IsDeleted);
        if (patient == null) throw new Exception("Paciente não encontrado");

        // Verifica se já existe plano para a data
        var existingPlan = await _context.MealPlans
            .FirstOrDefaultAsync(mp => mp.PatientId == createDto.PatientId
                                   && mp.Date.Date == createDto.Date.Date
                                   && !mp.IsDeleted);
        if (existingPlan != null) throw new Exception("Já existe um plano para esta data");

        var mealPlan = new MealPlan
        {
            PatientId = createDto.PatientId,
            Date = createDto.Date,
            Notes = createDto.Notes,
            MealPlanFoods = new List<MealPlanFood>()
        };

        // Adiciona cada alimento
        foreach (var foodItem in createDto.Foods)
        {
            var food = await _context.Foods
                .FirstOrDefaultAsync(f => f.Id == foodItem.FoodId && !f.IsDeleted);
            if (food == null) throw new Exception($"Alimento ID {foodItem.FoodId} não encontrado");

            mealPlan.MealPlanFoods.Add(new MealPlanFood
            {
                FoodId = foodItem.FoodId,
                PortionSize = foodItem.PortionSize,
                MealTime = foodItem.MealTime
            });
        }

        _context.MealPlans.Add(mealPlan);
        await _context.SaveChangesAsync();

        return await GetMealPlanByIdAsync(mealPlan.Id);
    }

    public async Task UpdateMealPlanAsync(int id, UpdateMealPlanDto updateDto)
    {
        var mealPlan = await _context.MealPlans
            .Include(mp => mp.MealPlanFoods)
            .FirstOrDefaultAsync(mp => mp.Id == id && !mp.IsDeleted);
        if (mealPlan == null) throw new KeyNotFoundException("Plano alimentar não encontrado");

        // Atualiza campos básicos
        if (updateDto.Date.HasValue) mealPlan.Date = updateDto.Date.Value;
        if (!string.IsNullOrWhiteSpace(updateDto.Notes)) mealPlan.Notes = updateDto.Notes;



        // Adiciona novos alimentos
        if (updateDto.FoodsToAdd?.Any() == true)
        {
            foreach (var foodItem in updateDto.FoodsToAdd)
            {
                var food = await _context.Foods.FindAsync(foodItem.FoodId);
                if (food == null || food.IsDeleted)
                    throw new Exception($"Alimento ID {foodItem.FoodId} não encontrado");

                mealPlan.MealPlanFoods.Add(new MealPlanFood
                {
                    FoodId = foodItem.FoodId,
                    PortionSize = foodItem.PortionSize,
                    MealTime = foodItem.MealTime
                });
            }
        }

        await _context.SaveChangesAsync();
    }

    public async Task DeleteMealPlanAsync(int id)
    {
        var mealPlan = await _context.MealPlans
            .FirstOrDefaultAsync(mp => mp.Id == id && !mp.IsDeleted);
        if (mealPlan == null) throw new KeyNotFoundException("Plano alimentar não encontrado");

        mealPlan.IsDeleted = true;
        await _context.SaveChangesAsync();
    }

    public async Task<MealPlanDto> GetMealPlanByIdAsync(int id)
    {
        var mealPlan = await _context.MealPlans
            .Include(mp => mp.Patient)
            .Include(mp => mp.MealPlanFoods)
                .ThenInclude(mpf => mpf.Food)
            .FirstOrDefaultAsync(mp => mp.Id == id && !mp.IsDeleted);

        if (mealPlan == null)
            throw new KeyNotFoundException("Plano alimentar não encontrado");

        var mealPlanDto = _mapper.Map<MealPlanDto>(mealPlan);
        mealPlanDto.TotalCalories = await CalculateTotalCaloriesAsync(id);
        mealPlanDto.TotalProtein = await CalculateTotalNutrientAsync(id, f => f.ProteinPer100g);
        mealPlanDto.TotalCarbs = await CalculateTotalNutrientAsync(id, f => f.CarbsPer100g);
        mealPlanDto.TotalFat = await CalculateTotalNutrientAsync(id, f => f.FatPer100g);

        return mealPlanDto;
    }

    private async Task<double> CalculateTotalNutrientAsync(int mealPlanId, Func<Food, double> nutrientSelector)
    {
        var mealPlanFoods = await _context.MealPlanFoods
            .Include(mpf => mpf.Food)
            .Where(mpf => mpf.Id == mealPlanId && !mpf.Food.IsDeleted)
            .ToListAsync();

        return mealPlanFoods.Sum(mpf =>
            (nutrientSelector(mpf.Food) * (double)mpf.PortionSize) / 100.0);
    }

    public async Task<decimal> CalculateTotalCaloriesAsync(int mealPlanId)
    {
        var mealPlanFoods = await _context.MealPlanFoods
            .Include(mpf => mpf.Food)
            .Where(mpf => mpf.Id == mealPlanId && !mpf.Food.IsDeleted)
            .ToListAsync();

        return (decimal)mealPlanFoods.Sum(mpf =>
            (mpf.Food.CaloriesPer100g * (double)mpf.PortionSize) / 100.0);
    }
}