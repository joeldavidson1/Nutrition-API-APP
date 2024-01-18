using Microsoft.AspNetCore.Mvc;
using NutritionAPI.Dto;
using NutritionAPI.Interfaces;
using NutritionAPI.Models;

namespace NutritionAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FoodItemsController : Controller
{
    public enum DetailsOption
    {
        Abridged,
        Full
    }
    
    private readonly IFoodItemRepository _foodItemRepository;
    private readonly IMappingService _mappingService;

    public FoodItemsController(IFoodItemRepository foodItemRepository, IMappingService mappingService)
    {
        _foodItemRepository = foodItemRepository;
        _mappingService = mappingService;
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<FoodItems>))]
    // public async Task<IActionResult> GetFoodItems([FromQuery] bool abridgedDetails = false
    public async Task<IActionResult> GetFoodItems()
    {
        IEnumerable<FoodItems> foodItems = await _foodItemRepository.GetFoodItems();
        IEnumerable<FoodItemsDto> foodItemDtos = _mappingService.MapFoodItemsToDtos(foodItems);

        if (!ModelState.IsValid) return BadRequest(ModelState);

        return Ok(foodItemDtos);
    }

    [HttpGet("{foodCode}")]
    [ProducesResponseType(200, Type = typeof(FoodItems))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetFoodItem(string foodCode)
    {
        if (!_foodItemRepository.FoodItemExists(foodCode))
        {
            return NotFound();
        }

        FoodItems foodItems = await _foodItemRepository.GetFoodItem(foodCode);
        FoodItemsDto foodItemsDto = _mappingService.MapFoodItemToDto(foodItems);
        
        if (!ModelState.IsValid) return BadRequest(ModelState);

        return Ok(foodItemsDto);
    }
    
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