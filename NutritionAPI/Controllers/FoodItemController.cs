using Microsoft.AspNetCore.Mvc;
using NutritionAPI.Dto;
using NutritionAPI.Interfaces;
using NutritionAPI.Models;

namespace NutritionAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FoodItemController : Controller
{
    public enum DetailsOption
    {
        Abridged,
        Full
    }
    
    private readonly IFoodItemRepository _foodItemRepository;
    private readonly IMappingService _mappingService;

    public FoodItemController(IFoodItemRepository foodItemRepository, IMappingService mappingService)
    {
        _foodItemRepository = foodItemRepository;
        _mappingService = mappingService;
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<FoodItem>))]
    public async Task<IActionResult> GetFoodItems([FromQuery] bool abridgedDetails = false)
    {
        IEnumerable<FoodItem> foodItems = await _foodItemRepository.GetFoodItems();
        IEnumerable<FoodItemDto> foodItemDtos = _mappingService.MapFoodItemsToDtos(foodItems);

        if (!ModelState.IsValid) return BadRequest(ModelState);

        return Ok(foodItemDtos);
    }

    [HttpGet("{foodCode}")]
    [ProducesResponseType(200, Type = typeof(FoodItem))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetFoodItem(string foodCode)
    {
        if (!_foodItemRepository.FoodItemExists(foodCode))
        {
            return NotFound();
        }

        FoodItem foodItem = await _foodItemRepository.GetFoodItem(foodCode);
        FoodItemDto foodItemDto = _mappingService.MapFoodItemToDto(foodItem);
        
        if (!ModelState.IsValid) return BadRequest(ModelState);

        return Ok(foodItemDto);
    }
}