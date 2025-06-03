using AutoMapper;
using MealPlanAPI.Models;
using MealPlanAPI.Models.DTOs.FoodDto;
using MealPlanAPI.Models.DTOs.MealPlan;
using MealPlanAPI.Models.DTOs.PatientDto;


public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapeamentos para Patient (mantido igual)
        CreateMap<CreatePatientDto, Patient>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<Patient, PatientDto>();

        CreateMap<UpdatePatientDto, Patient>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

        // Mapeamentos para Food (mantido igual)
        CreateMap<CreateFoodDto, Food>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

        CreateMap<Food, FoodDto>();

        CreateMap<UpdateFoodDto, Food>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

        // Mapeamentos para MealPlan (mesmo padrão)
        CreateMap<CreateMealPlanDto, MealPlan>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.MealPlanFoods, opt => opt.Ignore()); // Será preenchido manualmente

        CreateMap<MealPlan, MealPlanDto>();

        CreateMap<UpdateMealPlanDto, MealPlan>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

        // Mapeamento para MealPlanFood
        CreateMap<MealPlanFood, MealPlanFoodDto>()
            .ForMember(dest => dest.FoodName, opt => opt.MapFrom(src => src.Food.Name));
    }
}