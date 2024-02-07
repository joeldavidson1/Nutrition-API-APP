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

    /// <summary>
    /// Gets the list of all the food groups
    /// </summary>
    /// <returns>The food groups list</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllFoodGroups([FromQuery] FoodGroupParameters foodGroupParameters)
    {
        IEnumerable<FoodGroupsDto> foodGroups = await _service.FoodGroupsService.GetAllFoodGroups(foodGroupParameters, 
            trackChanges: false);
        return Ok(foodGroups);
    }

    /// <summary>
    /// Gets a specific food group from a given food group code
    /// </summary>
    /// <returns>A food group</returns>
    [HttpGet("{foodGroupCode}")]
    public async Task<IActionResult> GetFoodGroup(string foodGroupCode)
    {
        FoodGroupsDto foodItem = await _service.FoodGroupsService.GetFoodGroup(foodGroupCode, trackChanges: false);
        return Ok(foodItem);
    }
    
    /// <summary>
    /// Gets a list of food items from a specific food group
    /// </summary>
    /// <returns>A list of food items from the given food group</returns>
    [HttpGet("{foodGroupCode}/foodItems")]
    public async Task<IActionResult> GetFoodItemsForFoodGroup([FromQuery] FoodItemParameters foodItemParameters,
        string foodGroupCode)
    {
        (IEnumerable<ExpandoObject> foodItems, MetaData metaData) pagedResult = await _service.FoodItemsService.GetFoodItemsForFoodGroupAsync(foodItemParameters,
            foodGroupCode, trackChanges: false);
        
        Response.Headers.Add("X-pagination", JsonSerializer.Serialize(pagedResult.metaData));
        
        return Ok(pagedResult.foodItems);
    }
    
    /// <summary>
    /// Retrieves the available HTTP methods for the foodGroups endpoint
    /// </summary>
    /// <returns>A list of allowed HTTP methods</returns>
    [HttpOptions]
    public IActionResult GetFoodGroupsOptions()
    {
        Response.Headers.Add("Allow", "GET, OPTIONS");

        return Ok();
    }
}