using AutoMapper;
using MealPlanAPI.Models.DTOs.MealPlan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/mealplans")] // Rota explícita para evitar conflitos
public class MealPlansController : ControllerBase
{
    private readonly IMealPlanService _mealPlanService;
    private readonly IMapper _mapper;

    public MealPlansController(IMealPlanService mealPlanService, IMapper mapper)
    {
        _mealPlanService = mealPlanService;
        _mapper = mapper;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<MealPlanDto>> Create([FromBody] CreateMealPlanDto createDto)
    {
        try
        {
            var mealPlan = await _mealPlanService.CreateMealPlanAsync(createDto);
            return Created($"/api/mealplans/{mealPlan.Id}", mealPlan);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateMealPlanDto updateDto)
    {
        try
        {
            await _mealPlanService.UpdateMealPlanAsync(id, updateDto);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _mealPlanService.DeleteMealPlanAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<MealPlanDto>> GetById(int id)
    {
        try
        {
            var mealPlan = await _mealPlanService.GetMealPlanByIdAsync(id);
            return Ok(mealPlan);
        }
        catch (Exception ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }
}