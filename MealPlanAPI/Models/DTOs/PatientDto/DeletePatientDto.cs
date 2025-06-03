using System.ComponentModel.DataAnnotations;
namespace MealPlanAPI.Models.DTOs.PatientDto
{
    public class DeletePatientDto
    {
        [Required]
        public int Id { get; set; }
    }
}