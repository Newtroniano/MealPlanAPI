using MealPlanAPI.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

public class MealPlan : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public int PatientId { get; set; }
    public Patient Patient { get; set; }
    public ICollection<MealPlanFood> MealPlanFoods { get; set; }

    [NotMapped]
    public double TotalCalories => MealPlanFoods?.Sum(mpf => mpf.CalculateCalories()) ?? 0;
}