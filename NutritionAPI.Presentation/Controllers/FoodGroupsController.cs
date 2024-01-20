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
    public async Task<IActionResult> GetFoodGroups()
    {
        IEnumerable<FoodGroupsDto> foodGroups = await _service.FoodGroupsService.GetAllFoodGroups(trackChanges: false);
        return Ok(foodGroups);
    }

    [HttpGet("{foodGroupCode}")]
    public async Task<IActionResult> GetFoodGroup(string foodGroupCode)
    {
        FoodGroupsDto foodItem = await _service.FoodGroupsService.GetFoodGroup(foodGroupCode, trackChanges: false);
        return Ok(foodItem);
    }
    
    [HttpGet("{foodGroupCode}/foodItems")]
    public async Task<IActionResult> GetFoodItemsForFoodGroup(string foodGroupCode)
    {
        IEnumerable<FoodItemsDto> foodItems = await _service.FoodItemsService.GetFoodItemsForFoodGroupAsync(foodGroupCode, trackChanges: false);
        return Ok(foodItems);
    }
}