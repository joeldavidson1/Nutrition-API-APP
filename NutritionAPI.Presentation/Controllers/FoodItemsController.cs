using System.Dynamic;
using System.Text.Json;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace NutritionAPI.Presentation.Controllers;

[Route("api/foodItems")]
[ApiController]
public class FoodItemsController : ControllerBase
{
    private readonly IServiceManager _service;

    public FoodItemsController(IServiceManager service) => _service = service;

    /// <summary>
    /// Gets the list of all the food items
    /// </summary>
    /// <returns>The food items list</returns>
    [HttpGet(Name = "GetFoodItems"), Authorize]
    public async Task<IActionResult> GetFoodItems([FromQuery] FoodItemParameters foodItemParameters)
    {
        (IEnumerable<ExpandoObject> foodItems, MetaData metaData) pagedResult = await
            _service.FoodItemsService.GetAllFoodItemsAsync(foodItemParameters, trackChanges: false);
        
        Response.Headers.Add("X-pagination", JsonSerializer.Serialize(pagedResult.metaData));

        return Ok(pagedResult.foodItems);
    }

    /// <summary>
    /// Gets a specific food item from a given food code
    /// </summary>
    /// <returns>A food item</returns>
    [HttpGet("{foodCode}")]
    public async Task<IActionResult> GetFoodItem(string foodCode)
    {
        FoodItemsDto foodItem = await _service.FoodItemsService.GetFoodItemAsync(foodCode, trackChanges: false);
        return Ok(foodItem);
    }

    /// <summary>
    /// Retrieves the available HTTP methods for the foodItems endpoint
    /// </summary>
    /// <returns>A list of allowed HTTP methods</returns>
    [HttpOptions]
    public IActionResult GetFoodItemsOptions()
    {
        Response.Headers.Add("Allow", "GET, OPTIONS");

        return Ok();
    }
}