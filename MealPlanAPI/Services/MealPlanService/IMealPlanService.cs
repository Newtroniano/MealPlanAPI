using MealPlanAPI.Models.DTOs.MealPlan;

public interface IMealPlanService
{
    Task<MealPlanDto> GetMealPlanByIdAsync(int id);
    Task<decimal> CalculateTotalCaloriesAsync(int mealPlanId);

    Task<MealPlanDto> CreateMealPlanAsync(CreateMealPlanDto createDto);
    Task UpdateMealPlanAsync(int id, UpdateMealPlanDto updateDto);
    Task DeleteMealPlanAsync(int id);

}