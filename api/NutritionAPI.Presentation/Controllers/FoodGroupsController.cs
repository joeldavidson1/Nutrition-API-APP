using System.Dynamic;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
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
    [HttpGet(Name = "GetFoodGroups"), Authorize]
    public async Task<IActionResult> GetAllFoodGroups([FromQuery] FoodGroupParameters foodGroupParameters)
    {
        IEnumerable<FoodGroupsDto> foodGroups = await _service.FoodGroupsService.GetAllFoodGroups(foodGroupParameters, 
            trackChanges: false);
        return Ok(foodGroups);
    }

    /// <summary>
    /// Gets a specific food group from a given food group code
    /// </summary>
    /// <param name="foodGroupCode" example="DG">Filter food items via a food group code</param>
    /// <returns>A food group</returns>
    [HttpGet("{foodGroupCode}"), Authorize]
    public async Task<IActionResult> GetFoodGroup(string foodGroupCode)
    {
        FoodGroupsDto foodItem = await _service.FoodGroupsService.GetFoodGroup(foodGroupCode, trackChanges: false);
        return Ok(foodItem);
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