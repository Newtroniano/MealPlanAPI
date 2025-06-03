using Microsoft.AspNetCore.Mvc;

namespace MealPlanAPI.Services
{
    public interface IPatientService
    {
        Task<IActionResult> ReactivatePatientAsync(int id);
    }
}
