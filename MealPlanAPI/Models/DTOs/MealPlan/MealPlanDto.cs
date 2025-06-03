using MealPlanAPI.Models.DTOs.MealPlan;

public class MealPlanDto
{

        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public decimal TotalCalories { get; set; }
        public double TotalProtein { get; set; }  
        public double TotalCarbs { get; set; }    
        public double TotalFat { get; set; }     
        public List<MealPlanFoodDto> Food { get; set; }
    
}