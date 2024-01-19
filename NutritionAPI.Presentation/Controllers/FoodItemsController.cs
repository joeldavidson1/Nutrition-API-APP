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
    public IActionResult GetFoodItems()
    {
        IEnumerable<FoodItemsDto> foodItems = _service.FoodItemsService.GetAllFoodItems(trackChanges: false);
        return Ok(foodItems);
    }

    [HttpGet("{foodCode}")]
    public IActionResult GetFoodItem(string foodCode)
    {
        FoodItemsDto foodItem = _service.FoodItemsService.GetFoodItem(foodCode, trackChanges: false);
        return Ok(foodItem);
    }
}