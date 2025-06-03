//using MealPlanAPI.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

public class MealPlan : BaseEntity
{
    public int PatientId { get; set; }
    public Patient Patient { get; set; }
    public DateTime Date { get; set; }
    public string Notes { get; set; }
    public ICollection<MealPlanFood> MealPlanFoods { get; set; } = new List<MealPlanFood>();
    public bool IsDeleted { get; set; }
}