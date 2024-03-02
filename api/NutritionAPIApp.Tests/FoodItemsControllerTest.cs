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
using NutritionAPI.Presentation.Controllers;
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
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IServiceManager> _service;

    public FoodItemsControllerTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _repositoryManagerMock = new Mock<IRepositoryManager>();
        _mapperMock = new Mock<IMapper>();
        _service = new Mock<IServiceManager>();

        _controller = new FoodItemsController(_service.Object);
    }

    [Fact]
    public async Task GetFoodItems_WhenCalled_ReturnsOkResult()
    {
        // Arrange
        var mockHttpContext = MockHttpContext();
        var foodItemParameters = new FoodItemParameters();
        var expectedFoodItems = GenerateExpectedFoodItems();
        var pagedList = PagedList<FoodItems>.ToPagedList(expectedFoodItems, pageNumber: 1, pageSize: 10);
        
        SetupRepositoryMock(pagedList, foodItemParameters);
        SetupServiceMockForAllFoodItems(expectedFoodItems);
        _controller.ControllerContext.HttpContext = mockHttpContext.Object;

        // Act
        var result = await _controller.GetFoodItems(new FoodItemParameters());

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
        var expectedFoodItems = GenerateExpectedFoodItems();
        var pagedList = PagedList<FoodItems>.ToPagedList(expectedFoodItems, pageNumber: 1, pageSize: 10);
    
        SetupRepositoryMock(pagedList, foodItemParameters);
        SetupServiceMockForAllFoodItems(expectedFoodItems);
        _controller.ControllerContext.HttpContext = mockHttpContext.Object;

        // Act
        var result = await _controller.GetFoodItems(new FoodItemParameters());

        // _testOutputHelper.WriteLine(expectedFoodItems[0].Name);
    
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
        var expectedFoodItems = GenerateExpectedFoodItems().Where(f => f.Name.Contains(foodItemParameters.SearchFoodByName)).ToList(); // Filter expected items based on the search criteria
        var pagedList = PagedList<FoodItems>.ToPagedList(expectedFoodItems, pageNumber: 1, pageSize: 10);
    
        SetupRepositoryMock(pagedList, foodItemParameters);
        SetupServiceMockForAllFoodItems(expectedFoodItems);
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
        var foodItemParameters = new FoodItemParameters();
        var expectedFoodItems = GenerateExpectedFoodItems();
        var pagedList = PagedList<FoodItems>.ToPagedList(expectedFoodItems, pageNumber: 1, pageSize: 10);
        
        SetupRepositoryMock(pagedList, foodItemParameters);
        SetupServiceMockForOneFoodItem(expectedFoodItems);
        _controller.ControllerContext.HttpContext = mockHttpContext.Object;

        // Act
        var result = await _controller.GetFoodItem("14-999");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
    }
    
    [Fact]
    public async Task GetFoodItemFromFoodCode_WhenCalled_ReturnsMatchingFoodItem()
    {
        // Arrange
        var mockHttpContext = MockHttpContext();
        var foodItemParameters = new FoodItemParameters();
        var expectedFoodItems = GenerateExpectedFoodItems();
        var pagedList = PagedList<FoodItems>.ToPagedList(expectedFoodItems, pageNumber: 1, pageSize: 10);
        
        SetupRepositoryMock(pagedList, foodItemParameters);
        SetupServiceMockForOneFoodItem(expectedFoodItems);
        _controller.ControllerContext.HttpContext = mockHttpContext.Object;

        // Act
        var result = await _controller.GetFoodItem("14-999");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedFoodItem = Assert.IsType<FoodItemsDto>(okResult.Value);
        Assert.Equal("Apple", returnedFoodItem.Name);
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

    private List<FoodItems> GenerateExpectedFoodItems()
    {
        return new List<FoodItems>
        {
            new FoodItems { FoodCode = "13-000", Name = "Banana", FoodGroupCode = "Group 1" },
            new FoodItems { FoodCode = "14-999", Name = "Apple", FoodGroupCode = "Group 2" }
        };
    }

    private void SetupRepositoryMock(PagedList<FoodItems> pagedList, FoodItemParameters foodItemParameters)
    {
        _repositoryManagerMock.Setup(repo => repo.FoodItems.GetAllFoodItemsAsync(foodItemParameters, It.IsAny<bool>()))
            .ReturnsAsync(pagedList);
    }

    private void SetupServiceMockForAllFoodItems(List<FoodItems> expectedFoodItems)
    {

        List<FoodItemsDto> foodItemsDto = new List<FoodItemsDto>();

        foreach (var foodItem in expectedFoodItems)
        {
            foodItemsDto.Add(new FoodItemsDto(
                FoodCode: foodItem.FoodCode,
                Name: foodItem.Name,
                FoodGroupCode: foodItem.FoodGroupCode,
                Description: null,
                DataReferences: null,
                Energy: null,
                Macronutrients: null,
                Minerals: null,
                Proximates: null,
                Vitamins: null
            ));
        }
        
        MetaData metaData = new MetaData(); 

        _service.Setup(s => s.FoodItemsService.GetAllFoodItemsAsync(It.IsAny<FoodItemParameters>(), false))
            .ReturnsAsync((foodItemsDto, metaData));
    }
    
    private void SetupServiceMockForOneFoodItem(List<FoodItems> expectedFoodItems)
    {
        _service.Setup(s => s.FoodItemsService.GetFoodItemAsync(It.IsAny<string>(), false))
            .ReturnsAsync((string foodCode, bool trackChanges) =>
            {
                FoodItems foodItem = expectedFoodItems.FirstOrDefault(fi => fi.FoodCode == foodCode);
            
                // If food item is found, map it to FoodItemsDto otherwise return null
                var foodItemDto = foodItem != null ? new FoodItemsDto(
                    FoodCode: foodItem.FoodCode,
                    Name: foodItem.Name,
                    FoodGroupCode: foodItem.FoodGroupCode,
                    Description: null,
                    DataReferences: null,
                    Energy: null,
                    Macronutrients: null,
                    Minerals: null,
                    Proximates: null,
                    Vitamins: null
                    ) 
                    : null;
            
                return foodItemDto;
            });
    }
}
