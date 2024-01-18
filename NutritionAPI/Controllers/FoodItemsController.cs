using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NutritionAPI.Dto;
using NutritionAPI.Helper;
using NutritionAPI.Interfaces;
using NutritionAPI.Models;
using NutritionAPI.Services;

namespace NutritionAPI.Controllers;

[Route("api/foodItems")]
[ApiController]
public class FoodItemsController : ControllerBase
{
    private readonly IServiceManager _service;
    
    public FoodItemsController(IServiceManager service) => _service = service;

    // [HttpGet("items/{page}")]
    // [ProducesResponseType(200, Type = typeof(IEnumerable<FoodItems>))]
    // [ProducesResponseType(404)] 
    // public async Task<IActionResult> GetFoodItems(int page = 1)
    // {
    //     int pageResults = 10;
    //     int totalFoodItems = await _foodItemRepository.GetTotalFoodItemsCount();
    //     int totalPages = (int)Math.Ceiling(totalFoodItems / (double)pageResults);
    //     
    //     if (page < 1 || page > totalPages)
    //     {
    //         return NotFound("Requested page not found");
    //     }
    //     
    //     IQueryable<FoodItems> query = _foodItemRepository.GetFoodItems();
    //     IEnumerable<FoodItems> foodItems = await query
    //         .Skip((page - 1) * pageResults)
    //         .Take(pageResults)
    //         .ToListAsync();
    //     
    //     IEnumerable<FoodItemsDto> foodItemDtos = _mappingService.MapFoodItemsToDtos(foodItems);
    //
    //     if (!ModelState.IsValid) return BadRequest(ModelState);
    //
    //     ApiResponse<IEnumerable<FoodItemsDto>> response = new ApiResponse<IEnumerable<FoodItemsDto>>
    //     {
    //         Data = foodItemDtos,
    //         CurrentPage = page,
    //         Pages = totalPages
    //     };
    //
    //     return Ok(response);
    // }
    
    [HttpGet]
    public async Task<IActionResult> GetFoodItems([FromQuery] FoodItemsParameters foodItemsParameters)
    {
        IEnumerable<FoodItemsDto> foodItems = await _service.FoodItemsService.GetAllFoodItemsAsync(foodItemsParameters, trackChanges: false);

        return Ok(foodItems);
    }

    // [HttpGet("item/{foodCode}", Name = "FoodItemById")]
    // [ProducesResponseType(200, Type = typeof(FoodItems))]
    // [ProducesResponseType(400)]
    // public async Task<IActionResult> GetFoodItem(string foodCode)
    // {
    //     if (!_foodItemRepository.FoodItemExists(foodCode))
    //     {
    //         return NotFound();
    //     }
    //
    //     FoodItems foodItems = await _foodItemRepository.GetFoodItemByFoodCode(foodCode);
    //     FoodItemsDto foodItemsDto = _mappingService.MapFoodItemToDto(foodItems);
    //     
    //     if (!ModelState.IsValid) return BadRequest(ModelState);
    //
    //     return Ok(foodItemsDto);
    // }
    
    // [HttpGet("Search")]
    // [ProducesResponseType(200, Type = typeof(IEnumerable<FoodItemDto>))]
    // public async Task<IActionResult> SearchFoodItems([FromQuery] string searchTerm)
    // {
    //     if (string.IsNullOrWhiteSpace(searchTerm))
    //     {
    //         return BadRequest("Search term is required.");
    //     }
    //
    //     IEnumerable<FoodItem> matchedFoodItems = await _foodItemRepository.SearchFoodItemsByName(searchTerm);
    //
    //     IEnumerable<FoodItemDto> matchedFoodItemDtos = _mappingService.MapFoodItemsToDtos(matchedFoodItems);
    //
    //     return Ok(matchedFoodItemDtos);
    // }
}