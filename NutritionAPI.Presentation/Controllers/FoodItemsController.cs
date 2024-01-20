using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace NutritionAPI.Presentation.Controllers;

[Route("api/foodItems")]
[ApiController]
public class FoodItemsController : ControllerBase
{
    private readonly IServiceManager _service;

    public FoodItemsController(IServiceManager service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetFoodItems()
    {
        IEnumerable<FoodItemsDto> foodItems = await _service.FoodItemsService.GetAllFoodItemsAsync(trackChanges: false);
        return Ok(foodItems);
    }

    [HttpGet("{foodCode}")]
    public async Task<IActionResult> GetFoodItem(string foodCode)
    {
        FoodItemsDto foodItem = await _service.FoodItemsService.GetFoodItemAsync(foodCode, trackChanges: false);
        return Ok(foodItem);
    }
}