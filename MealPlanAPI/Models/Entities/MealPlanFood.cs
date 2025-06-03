public class MealPlanFood
{
    public bool IsDeleted { get; set; } = false;

    public int Id { get; set; }
    public MealPlan MealPlan { get; set; }

    public int FoodId { get; set; }
    public Food Food { get; set; } // <- PROPRIEDADE DE NAVEGAÇÃO

    public decimal PortionSize { get; set; }
    public string MealTime { get; set; }
}