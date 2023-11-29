using Microsoft.AspNetCore.Mvc;
using NutritionAPI.Interfaces;
using NutritionAPI.Models;

namespace NutritionAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FoodItemController : Controller
{
    private readonly IFoodItemRepository _foodItemRepository;

    public FoodItemController(IFoodItemRepository foodItemRepository)
    {
        _foodItemRepository = foodItemRepository;
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<FoodItem>))]
    public async Task<IActionResult> GetFoodItems()
    {
        IEnumerable<FoodItem> foodItems = await _foodItemRepository.GetFoodItems();

        if (!ModelState.IsValid) return BadRequest(ModelState);

        return Ok(foodItems);
    }

    [HttpGet("{foodCode}")]
    [ProducesResponseType(200, Type = typeof(FoodItem))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetFoodItem(string foodCode)
    {
        if (!_foodItemRepository.FoodItemExists(foodCode))
        {
            return NotFound();
        }

        FoodItem foodItem = await _foodItemRepository.GetFoodItem(foodCode);

        if (!ModelState.IsValid) return BadRequest(ModelState);

        return Ok(foodItem);
    }
}