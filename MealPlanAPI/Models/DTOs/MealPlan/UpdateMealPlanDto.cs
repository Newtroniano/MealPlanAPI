namespace MealPlanAPI.Models.DTOs.MealPlan
{
    public class UpdateMealPlanDto
    {

        public DateTime? Date { get; set; }
        public string Notes { get; set; }
        public List<MealPlanFoodItemDto> FoodsToAdd { get; set; }
        public List<int> FoodIdsToRemove { get; set; }
    }
}
