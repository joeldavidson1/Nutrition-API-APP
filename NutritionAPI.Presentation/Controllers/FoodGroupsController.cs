using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace NutritionAPI.Presentation.Controllers;

[Route("api/foodGroups")]
[ApiController]
public class FoodGroupsController : ControllerBase
{
    private readonly IServiceManager _service;

    public FoodGroupsController(IServiceManager service) => _service = service;

    [HttpGet]
    public IActionResult GetFoodGroups()
    {
        IEnumerable<FoodGroupsDto> foodGroups = _service.FoodGroupsService.GetAllFoodGroups(trackChanges: false);
        return Ok(foodGroups);
    }

    [HttpGet("{foodGroupCode}")]
    public IActionResult GetFoodGroup(string foodGroupCode)
    {
        FoodGroupsDto foodItem = _service.FoodGroupsService.GetFoodGroup(foodGroupCode, trackChanges: false);
        return Ok(foodItem);
    }
    
    [HttpGet("{foodGroupCode}/foodItems")]
    public IActionResult GetFoodItemsForFoodGroup(string foodGroupCode)
    {
        IEnumerable<FoodItemsDto> foodItems = _service.FoodItemsService.GetFoodItemsForFoodGroup(foodGroupCode, trackChanges: false);
        return Ok(foodItems);
    }
}