using MealPlanAPI.Models.DTOs.PatientDto;
using Microsoft.AspNetCore.Mvc;

namespace MealPlanAPI.Services.Patient
{
    public interface IPatientService
    {
        Task<PatientDto> CreatePatientAsync(CreatePatientDto patientDto);
        Task<PatientDto?> GetPatientByIdAsync(int id);
        Task<List<PatientDto>> GetAllPatientsAsync(string? nameFilter, int page, int pageSize);
        Task DeletePatientAsync(int id);
        Task ReactivatePatientAsync(int id);
        Task<PatientDto> UpdatePatientAsync(int id, UpdatePatientDto patientDto);
    }
}
