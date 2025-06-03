using System.ComponentModel.DataAnnotations;

namespace MealPlanAPI.Models.DTOs.FoodDto
{
    public class DeleteFood
    {
        [Required]
        public int Id { get; set; }
    }
}
