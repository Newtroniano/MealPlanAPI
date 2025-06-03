using AutoMapper;
using MealPlanAPI.Models.DTOs.PatientDto;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapeamentos para Patient
        CreateMap<CreatePatientDto, Patient>();
        CreateMap<Patient, PatientDto>();
        CreateMap<UpdatePatientDto, Patient>();


        // Mapeamentos para Food
        //CreateMap<CreateFoodDto, Food>();
        //CreateMap<Food, FoodDto>();
    }
}