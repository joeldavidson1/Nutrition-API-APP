namespace NutritionAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using NutritionAPI.Dto;
using NutritionAPI.Interfaces;
using NutritionAPI.Models;

public class AllFoodItemInformation : Controller
{

    [Route("api/[controller]")]
    [ApiController]
    public class FoodItemController : Controller
    {
        private readonly IFoodItemRepository _foodItemRepository;
        private readonly IMappingService _mappingService;

        public FoodItemController(IFoodItemRepository foodItemRepository, IMappingService mappingService)
        {
            _foodItemRepository = foodItemRepository;
            _mappingService = mappingService;
        }


    }
}