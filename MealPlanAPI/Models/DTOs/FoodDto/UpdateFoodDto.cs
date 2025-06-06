﻿using System.ComponentModel.DataAnnotations;

namespace MealPlanAPI.Models.DTOs.FoodDto
{
    public class UpdateFoodDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double CaloriesPer100g { get; set; }
        public double ProteinPer100g { get; set; }
        public double CarbsPer100g { get; set; }
        public double FatPer100g { get; set; }
    }
}
