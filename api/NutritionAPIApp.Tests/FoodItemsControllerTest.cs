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
    private readonly Mock<IDataShaper<FoodItemsDto>> _dataShaperMock;
    private readonly Mock<IServiceManager> _service;

    public FoodItemsControllerTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _repositoryManagerMock = new Mock<IRepositoryManager>();
        _mapperMock = new Mock<IMapper>();
        _dataShaperMock = new Mock<IDataShaper<FoodItemsDto>>();
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
        SetupServiceMock(expectedFoodItems);
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
        SetupServiceMock(expectedFoodItems);
        _controller.ControllerContext.HttpContext = mockHttpContext.Object;

        // Act
        var result = await _controller.GetFoodItems(new FoodItemParameters());
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedFoodItems = Assert.IsAssignableFrom<IEnumerable<ExpandoObject>>(okResult.Value);

        // Assert
        Assert.Equal(expectedFoodItems.Count, returnedFoodItems.Count());
    }
    

    [Fact]
    public async Task GetFoodItems_WhenCalled_SearchFoodByName_ReturnsMatchingItems()
    {
        // Arrange
        var mockHttpContext = MockHttpContext();
        var foodItemParameters = new FoodItemParameters { SearchFoodByName = "Food 1" }; // Set the name to search for
        var expectedFoodItems = GenerateExpectedFoodItems().Where(f => f.Name.Contains(foodItemParameters.SearchFoodByName)).ToList(); // Filter expected items based on the search criteria
        var pagedList = PagedList<FoodItems>.ToPagedList(expectedFoodItems, pageNumber: 1, pageSize: 10);
    
        SetupRepositoryMock(pagedList, foodItemParameters);
        SetupServiceMock(expectedFoodItems);
        _controller.ControllerContext.HttpContext = mockHttpContext.Object;

        // Act
        var result = await _controller.GetFoodItems(foodItemParameters);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedFoodItems = Assert.IsAssignableFrom<IEnumerable<ExpandoObject>>(okResult.Value);
        Assert.Equal(expectedFoodItems.Count, returnedFoodItems.Count()); // Ensure all expected items are returned
        // Add additional assertions if needed
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
            new FoodItems { FoodCode = "1", Name = "Food 1", FoodGroupCode = "Group 1" },
            new FoodItems { FoodCode = "2", Name = "Food 2", FoodGroupCode = "Group 2" }
        };
    }

    private void SetupRepositoryMock(PagedList<FoodItems> pagedList, FoodItemParameters foodItemParameters)
    {
        _repositoryManagerMock.Setup(repo => repo.FoodItems.GetAllFoodItemsAsync(foodItemParameters, It.IsAny<bool>()))
            .ReturnsAsync(pagedList);
    }

    private void SetupServiceMock(List<FoodItems> expectedFoodItems)
    {
        _service.Setup(s => s.FoodItemsService.GetAllFoodItemsAsync(It.IsAny<FoodItemParameters>(), false))
            .ReturnsAsync((expectedFoodItems.Select(f => new ExpandoObject()), new MetaData()));
    }
}
