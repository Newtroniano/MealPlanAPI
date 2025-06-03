using MealPlanAPI.Models.DTOs.FoodDto;

public interface IFoodService
{
    Task<FoodDto> CreateFoodAsync(CreateFoodDto foodDto);
    Task<FoodDto?> GetFoodByIdAsync(int id);
    Task<List<FoodDto>> GetAllFoodsAsync(string? nameFilter, int page, int pageSize);
    Task DeleteFoodAsync(int id);
    Task ReactivateFoodAsync(int id);
    Task<FoodDto> UpdateFoodAsync(int id, UpdateFoodDto foodDto);
    Task<List<FoodDto>> GetFoodsByNutritionAsync(
        double? minCalories,
        double? maxCalories,
        double? minProtein,
        double? maxProtein,
        int page,
        int pageSize);
}