using System.Dynamic;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace NutritionAPI.Presentation.Controllers;

[Route("api/foodGroups")]
[ApiController]
public class FoodGroupsController : ControllerBase
{
    private readonly IServiceManager _service;

    public FoodGroupsController(IServiceManager service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetFoodGroups([FromQuery] FoodGroupParameters foodGroupParameters)
    {
        IEnumerable<FoodGroupsDto> foodGroups = await _service.FoodGroupsService.GetAllFoodGroups(foodGroupParameters, 
            trackChanges: false);
        return Ok(foodGroups);
    }

    [HttpGet("{foodGroupCode}")]
    public async Task<IActionResult> GetFoodGroup(string foodGroupCode)
    {
        FoodGroupsDto foodItem = await _service.FoodGroupsService.GetFoodGroup(foodGroupCode, trackChanges: false);
        return Ok(foodItem);
    }
    
    [HttpGet("{foodGroupCode}/foodItems")]
    public async Task<IActionResult> GetFoodItemsForFoodGroup([FromQuery] FoodItemParameters foodItemParameters,
        string foodGroupCode)
    {
        (IEnumerable<ExpandoObject> foodItems, MetaData metaData) pagedResult = await _service.FoodItemsService.GetFoodItemsForFoodGroupAsync(foodItemParameters,
            foodGroupCode, trackChanges: false);
        
        Response.Headers.Add("X-pagination", JsonSerializer.Serialize(pagedResult.metaData));
        
        return Ok(pagedResult.foodItems);
    }
    
    [HttpOptions]
    public IActionResult GetFoodGroupsOptions()
    {
        Response.Headers.Add("Allow", "GET, OPTIONS");

        return Ok();
    }
}