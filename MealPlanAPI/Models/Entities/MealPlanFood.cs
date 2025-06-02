public class MealPlanFood
{
    public int MealPlanId { get; set; }
    public MealPlan MealPlan { get; set; }
    public int FoodId { get; set; }
    public Food Food { get; set; }
    public double PortionSizeInGrams { get; set; }

    public double CalculateCalories()
    {
        return (Food.CaloriesPer100g / 100) * PortionSizeInGrams;
    }
}