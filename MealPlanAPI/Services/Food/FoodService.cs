using AutoMapper;
using MealPlanAPI.Models;
using MealPlanAPI.Models.DTOs.FoodDto;
using Microsoft.EntityFrameworkCore;

public class FoodService : IFoodService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public FoodService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<FoodDto> CreateFoodAsync(CreateFoodDto foodDto)
    {
        var food = _mapper.Map<Food>(foodDto);
        _dbContext.Foods.Add(food);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<FoodDto>(food);
    }

    public async Task<FoodDto?> GetFoodByIdAsync(int id)
    {
        var food = await _dbContext.Foods.FindAsync(id);
        return food == null || food.IsDeleted ? null : _mapper.Map<FoodDto>(food);
    }

    public async Task<List<FoodDto>> GetAllFoodsAsync(string? nameFilter, int page, int pageSize)
    {
        var query = _dbContext.Foods.Where(f => !f.IsDeleted);

        if (!string.IsNullOrEmpty(nameFilter))
            query = query.Where(f => f.Name.Contains(nameFilter));

        var foods = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return _mapper.Map<List<FoodDto>>(foods);
    }

    public async Task DeleteFoodAsync(int id)
    {
        var food = await _dbContext.Foods.FindAsync(id);
        if (food == null || food.IsDeleted)
            throw new KeyNotFoundException("Comida não encontrada");

        food.IsDeleted = true;
        await _dbContext.SaveChangesAsync();
    }

    public async Task ReactivateFoodAsync(int id)
    {
        var food = await _dbContext.Foods.FindAsync(id);
        if (food == null)
            throw new KeyNotFoundException("Comida não encontrada");

        if (!food.IsDeleted)
            throw new InvalidOperationException("Comida já está ativa");

        food.IsDeleted = false;
        await _dbContext.SaveChangesAsync();
    }

    public async Task<FoodDto> UpdateFoodAsync(int id, UpdateFoodDto foodDto)
    {
        var food = await _dbContext.Foods.FindAsync(id);
        if (food == null || food.IsDeleted)
            throw new KeyNotFoundException("Comida não encontrada");

        _mapper.Map(foodDto, food);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<FoodDto>(food);
    }

    // Método adicional para buscar alimentos por valores nutricionais
    public async Task<List<FoodDto>> GetFoodsByNutritionAsync(
        double? minCalories,
        double? maxCalories,
        double? minProtein,
        double? maxProtein,
        int page,
        int pageSize)
    {
        var query = _dbContext.Foods.Where(f => !f.IsDeleted);

        if (minCalories.HasValue)
            query = query.Where(f => f.CaloriesPer100g >= minCalories.Value);

        if (maxCalories.HasValue)
            query = query.Where(f => f.CaloriesPer100g <= maxCalories.Value);

        if (minProtein.HasValue)
            query = query.Where(f => f.ProteinPer100g >= minProtein.Value);

        if (maxProtein.HasValue)
            query = query.Where(f => f.ProteinPer100g <= maxProtein.Value);

        var foods = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return _mapper.Map<List<FoodDto>>(foods);
    }
}