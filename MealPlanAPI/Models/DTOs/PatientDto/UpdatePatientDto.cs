using System.ComponentModel.DataAnnotations;

namespace MealPlanAPI.Models.DTOs.PatientDto
{
    public class UpdatePatientDto
    {

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        public DateTime DateOfBirth { get; set; }
    }
}
