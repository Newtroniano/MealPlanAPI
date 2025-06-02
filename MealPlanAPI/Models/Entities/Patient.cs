using MealPlanAPI.Models.Entities;

public class Patient : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool IsDeleted { get; set; } = false;
    public ICollection<MealPlan> MealPlans { get; set; }
}

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}