namespace MealPlanAPI.Models.DTOs.PatientDto
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}