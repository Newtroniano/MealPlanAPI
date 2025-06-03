using Microsoft.AspNetCore.Mvc;
using MealPlanAPI.Models.DTOs.FoodDto;
using MealPlanAPI.Services;

namespace MealPlanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodsController : ControllerBase
    {
        private readonly IFoodService _foodService;

        public FoodsController(IFoodService foodService)
        {
            _foodService = foodService;
        }

        [HttpPost]
        public async Task<ActionResult<FoodDto>> CreateFood([FromBody] CreateFoodDto foodDto)
        {
            var result = await _foodService.CreateFoodAsync(foodDto);
            return CreatedAtAction(nameof(GetFoodById), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FoodDto>> GetFoodById(int id)
        {
            var food = await _foodService.GetFoodByIdAsync(id);
            return food == null ? NotFound() : Ok(food);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodDto>>> GetAllFoods(
            [FromQuery] string? nameFilter,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var foods = await _foodService.GetAllFoodsAsync(nameFilter, page, pageSize);
            return Ok(foods);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFood(int id)
        {
            try
            {
                await _foodService.DeleteFoodAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPatch("{id}/reactivate")]
        public async Task<IActionResult> ReactivateFood(int id)
        {
            try
            {
                await _foodService.ReactivateFoodAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Food is already active");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FoodDto>> UpdateFood(int id, [FromBody] UpdateFoodDto foodDto)
        {
            try
            {
                var updatedFood = await _foodService.UpdateFoodAsync(id, foodDto);
                return Ok(updatedFood);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("nutrition")]
        public async Task<ActionResult<IEnumerable<FoodDto>>> GetFoodsByNutrition(
            [FromQuery] double? minCalories,
            [FromQuery] double? maxCalories,
            [FromQuery] double? minProtein,
            [FromQuery] double? maxProtein,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var foods = await _foodService.GetFoodsByNutritionAsync(
                minCalories,
                maxCalories,
                minProtein,
                maxProtein,
                page,
                pageSize);

            return Ok(foods);
        }
    }
}