using System.ComponentModel.DataAnnotations;

namespace MealPlanAPI.Models.DTOs.MealPlan
{
    // Para criação
    public class CreateMealPlanDto
    {
        [Required]
        public int PatientId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string Notes { get; set; }

        [Required]
        public List<MealPlanFoodItemDto> Foods { get; set; }
    }

    
}
