using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;
using NutritionAPI;
using NutritionAPI.Presentation.Controllers;
using Repository.Extensions;
using Service;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using Xunit;
using Xunit.Abstractions;
using IConfiguration = AutoMapper.Configuration.IConfiguration;

namespace NutritionAPIApp.Tests;

public class FoodItemsControllerTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly FoodItemsController _controller;
    private readonly Mock<IRepositoryManager> _repositoryManagerMock;
    private readonly IMapper _mapper;
    private readonly Mock<IServiceManager> _service;

    public FoodItemsControllerTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _repositoryManagerMock = new Mock<IRepositoryManager>();
        
        // Create the configuration for AutoMapper
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>(); // Add your mapping profile
            // Add any additional mappings or configurations here
        });

        // Create an instance of IMapper using the configured mapper configuration
        _mapper = mapperConfig.CreateMapper();
        
        _service = new Mock<IServiceManager>();

        _controller = new FoodItemsController(_service.Object);
    }

    [Fact]
    public async Task GetFoodItems_WhenCalled_ReturnsOkResult()
    {
        // Arrange
        var mockHttpContext = MockHttpContext();
        var foodItemParameters = new FoodItemParameters();
        
        var mockedDatabase = GenerateMockDatabase();
        SetupRepositoryMock(mockedDatabase, foodItemParameters);
        SetupServiceMock(foodItemParameters);
        
        _controller.ControllerContext.HttpContext = mockHttpContext.Object;

        // Act
        var result = await _controller.GetFoodItems(foodItemParameters);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
    }

    [Fact]
    public async Task GetFoodItems_WhenCalled_ReturnsAllItems()
    {
        // Arrange
        var mockHttpContext = MockHttpContext();
        var foodItemParameters = new FoodItemParameters();
        var expectedFoodItems = GenerateMockDatabase();
    
        SetupRepositoryMock(expectedFoodItems, foodItemParameters);
        SetupServiceMock(foodItemParameters);
        
        _controller.ControllerContext.HttpContext = mockHttpContext.Object;

        // Act
        var result = await _controller.GetFoodItems(foodItemParameters);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedFoodItems = Assert.IsAssignableFrom<IEnumerable<FoodItemsDto>>(okResult.Value);
        Assert.Equal(expectedFoodItems.Count, returnedFoodItems.Count());
    }

    [Fact]
    public async Task GetFoodItems_WhenCalled_SearchFoodByName_ReturnsMatchingItems()
    {
        // Arrange
        var mockHttpContext = MockHttpContext();
        var foodItemParameters = new FoodItemParameters { SearchFoodByName = "Banana" }; // Set the name to search for
        var expectedFoodItems = GenerateMockDatabase().Where(f => f.Name.Contains(foodItemParameters.SearchFoodByName)).ToList(); // Filter expected items based on the search criteria
    
        var mockedDatabase = GenerateMockDatabase();
        SetupRepositoryMock(mockedDatabase, foodItemParameters);
        SetupServiceMock(foodItemParameters);
        
        _controller.ControllerContext.HttpContext = mockHttpContext.Object;

        // Act
        var result = await _controller.GetFoodItems(foodItemParameters);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedFoodItems = Assert.IsAssignableFrom<IEnumerable<FoodItemsDto>>(okResult.Value);
        Assert.Equal(expectedFoodItems.Count, returnedFoodItems.Count()); // Ensure all expected items are returned
        Assert.All(returnedFoodItems, dto => Assert.Contains("Banana", dto.Name)); // Check if each item contains "Food 1" in its name
    }

    [Fact]
    public async Task GetFoodItemFromFoodCode_WhenCalled_ReturnsOkResult()
    {
        // Arrange
        var mockHttpContext = MockHttpContext();
        string testFoodCode = "14-999";

        var mockedDatabase = GenerateMockDatabase();
        SetupRepositoryMockForOneFoodItem(mockedDatabase, testFoodCode);
        SetupServiceMockForOneFoodItem(testFoodCode);
        _controller.ControllerContext.HttpContext = mockHttpContext.Object;

        // Act
        var result = await _controller.GetFoodItem(testFoodCode);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
    }
    
    [Fact]
    public async Task GetFoodItemFromFoodCode_WhenCalled_ReturnsMatchingFoodItem()
    {
        // Arrange
        var mockHttpContext = MockHttpContext();
        string testFoodCode = "14-999";
        var expectedFoodItem = GenerateMockDatabase().SingleOrDefault(f => f.FoodCode.Equals(testFoodCode));

        var mockedDatabase = GenerateMockDatabase();
        SetupRepositoryMockForOneFoodItem(mockedDatabase, testFoodCode);
        SetupServiceMockForOneFoodItem(testFoodCode);
        _controller.ControllerContext.HttpContext = mockHttpContext.Object;

        // Act
        var result = await _controller.GetFoodItem(testFoodCode);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedFoodItem = Assert.IsType<FoodItemsDto>(okResult.Value);
        Assert.Equal(expectedFoodItem.Name, returnedFoodItem.Name);
    }

    
    [Fact]
    public async Task GetFoodItems_WhenCalled_OrderBy_ReturnsCorrectOrder()
    {
        // Arrange
        var mockHttpContext = MockHttpContext();
        var foodItemParameters = new FoodItemParameters { OrderBy = "energy kcal desc" }; 
        var expectedFoodItems = GenerateMockDatabase()
            .OrderByDescending(f => f.Energy?.Kcal) // Order the expected items by kcal in descending order
            .ToList();

        var mockedDatabase = GenerateMockDatabase();
        SetupRepositoryMock(mockedDatabase, foodItemParameters);
        SetupServiceMock(foodItemParameters);
        
        _controller.ControllerContext.HttpContext = mockHttpContext.Object;

        // Act
        IActionResult result = await _controller.GetFoodItems(foodItemParameters);
        
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedFoodItems = Assert.IsAssignableFrom<IEnumerable<FoodItemsDto>>(okResult.Value);

        // Convert returnedFoodItems to a list for indexing
        var returnedFoodItemsList = returnedFoodItems.ToList();

        // Check if the returned items are in the correct order
        for (int i = 0; i < expectedFoodItems.Count; i++)
        {
            _testOutputHelper.WriteLine($"Expected food items: {expectedFoodItems[i].Name}");
            _testOutputHelper.WriteLine($"Returned food items: {returnedFoodItemsList[i].Name}");
            Assert.Equal(expectedFoodItems[i].FoodCode, returnedFoodItemsList[i].FoodCode);
        }
    }
    
    [Fact]
    public async Task GetFoodItems_WhenCalled_ReturnsCorrectResponseHeaders()
    {
        // Arrange
        var mockHttpContext = MockHttpContext();
        var foodItemParameters = new FoodItemParameters();
        
        var mockedDatabase = GenerateMockDatabase();
        SetupRepositoryMock(mockedDatabase, foodItemParameters);
        SetupServiceMock(foodItemParameters);
        
        _controller.ControllerContext.HttpContext = mockHttpContext.Object;

        // Act
        var result = await _controller.GetFoodItems(foodItemParameters);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);

        Assert.True(mockHttpContext.Object.Response.Headers.ContainsKey("x-pagination"));
        var paginationHeaderValue = mockHttpContext.Object.Response.Headers["x-pagination"];
        var paginationObject = JsonConvert.DeserializeObject<MetaData>(paginationHeaderValue);
        Assert.Equal(1, paginationObject.TotalPages);
        Assert.Equal(1, paginationObject.CurrentPage);
        Assert.Equal(10, paginationObject.PageSize);
        Assert.Equal(mockedDatabase.Count, paginationObject.TotalCount);
        Assert.False(paginationObject.HasPrevious);
        Assert.False(paginationObject.HasNext);
    }

    
    private Mock<HttpContext> MockHttpContext()
    {
        var mockHttpContext = new Mock<HttpContext>();
        var mockResponse = new Mock<HttpResponse>();
        var headers = new HeaderDictionary();
        mockResponse.SetupGet(r => r.Headers).Returns(headers);
        mockHttpContext.SetupGet(c => c.Response).Returns(mockResponse.Object);
        return mockHttpContext;
    }

    private List<FoodItems> GenerateMockDatabase()
    {
        return new List<FoodItems>
        {
            new FoodItems
            { 
                FoodCode = "13-000",
                Name = "Banana",
                FoodGroupCode = "Group 1",
                Energy = new Energy { Kcal = 89.0, Kj = 371.0 } 
            },
            new FoodItems
            { 
                FoodCode = "14-999",
                Name = "Apple",
                FoodGroupCode = "Group 1",
                Energy = new Energy { Kcal = 52.0, Kj = 218.0 }
            },
            new FoodItems
            { 
                FoodCode = "15-875",
                Name = "Cake",
                FoodGroupCode = "Group 2",
                Energy = new Energy { Kcal = 113.0, Kj = 472.8 }
            },
            new FoodItems
            { 
                FoodCode = "12-150",
                Name = "Carrot",
                FoodGroupCode = "Group 3",
                Energy = new Energy { Kcal = 10, Kj = 45.0 }
            }
        };
    }

    private void SetupRepositoryMock(List<FoodItems> database, FoodItemParameters foodItemParameters)
    {
        var filteredItems = database
            .AsQueryable()
            .Search(foodItemParameters.SearchFoodByName)
            .Sort(foodItemParameters.OrderBy)
            .ToList();

        // Create a paged list based on the filtered items and parameters
        var pagedList = PagedList<FoodItems>.ToPagedList(filteredItems, foodItemParameters.PageNumber, foodItemParameters.PageSize);

        // Setup the mock to return the paged list when GetAllFoodItemsAsync is called with the specified parameters
        _repositoryManagerMock
            .Setup(repo => repo.FoodItems.GetAllFoodItemsAsync(foodItemParameters, It.IsAny<bool>()))
            .ReturnsAsync(pagedList);
    }

    
    private void SetupServiceMock(FoodItemParameters foodItemParameters)
    {
        _service.Setup(s => s.FoodItemsService.GetAllFoodItemsAsync(foodItemParameters, false))
            .ReturnsAsync((FoodItemParameters parameters, bool trackChanges) =>
            {
                // Retrieve the items returned by the repository mock based on the parameters
                var pagedList = _repositoryManagerMock.Object.FoodItems.GetAllFoodItemsAsync(parameters, trackChanges).Result;
                var foodItemsDto = pagedList.Select(item => _mapper.Map<FoodItemsDto>(item));
                var metaData = pagedList.MetaData;

                return (foodItems: foodItemsDto, metaData: metaData);
            });
    }

   
    private void SetupRepositoryMockForOneFoodItem(List<FoodItems> database, string foodCode)
    {
        var filteredItem = database
            .AsQueryable()
            .FirstOrDefault(x => x.FoodCode.Equals(foodCode)); // Filter by foodCode
        
        _repositoryManagerMock
            .Setup(repo => repo.FoodItems.GetFoodItemAsync(foodCode, It.IsAny<bool>()))
            .ReturnsAsync(filteredItem); // Ensure filteredItem is not null
    }

    
    private void SetupServiceMockForOneFoodItem(string foodCode)
    {
        // Setup the service mock to return the mapped DTO when GetFoodItemAsync is called
        _service.Setup(s => s.FoodItemsService.GetFoodItemAsync(foodCode, It.IsAny<bool>()))
            .ReturnsAsync((string foodCode, bool trackChanges) =>
            {
                // Retrieve the food item from the repository mock based on the food code
                var result = _repositoryManagerMock.Object.FoodItems.GetFoodItemAsync(foodCode, trackChanges).Result;
                // Map the retrieved food item to a DTO
                var mappedDto = _mapper.Map<FoodItemsDto>(result);
                return mappedDto;
            });
    }


}
