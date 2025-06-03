using System.ComponentModel.DataAnnotations;

namespace MealPlanAPI.Models.DTOs.MealPlan
{
    public class DeleteMealPlan
    {
        [Required]
        public int Id { get; set; }
    }
}
