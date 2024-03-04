using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NutritionAPI;
using NutritionAPI.Presentation.Controllers;
using Repository.Extensions;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using Xunit;
using Xunit.Abstractions;

namespace NutritionAPIApp.Tests;

public class FoodGroupsControllerTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly FoodGroupsController _controller;
    private readonly Mock<IRepositoryManager> _repositoryManagerMock;
    private readonly Mock<IServiceManager> _service;
    private readonly IMapper _mapper;

    public FoodGroupsControllerTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _repositoryManagerMock = new Mock<IRepositoryManager>();
        _service = new Mock<IServiceManager>();
        _controller = new FoodGroupsController(_service.Object);
        
        // Create the configuration for AutoMapper
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>(); // Add your mapping profile
            // Add any additional mappings or configurations here
        });

        // Create an instance of IMapper using the configured mapper configuration
        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task GetFoodGroups_WhenCalled_ReturnsOkResult()
    {
        // Arrange
        var mockHttpContext = MockHttpContext();
        var foodGroupParameters = new FoodGroupParameters();
        
        var mockedFoodGroups = GenerateMockFoodGroups();
        SetupRepositoryMockForAllFoodGroups(mockedFoodGroups, foodGroupParameters);
        SetupServiceMockForAllFoodGroups(foodGroupParameters);
        
        _controller.ControllerContext.HttpContext = mockHttpContext.Object;

        // Act
        var result = await _controller.GetAllFoodGroups(foodGroupParameters);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
    }

    [Fact]
    public async Task GetFoodGroups_WhenCalled_ReturnsAllItems()
    {
        // Arrange
        var mockHttpContext = MockHttpContext();
        var foodGroupParameters = new FoodGroupParameters();
        
        var mockedFoodGroups = GenerateMockFoodGroups();
        SetupRepositoryMockForAllFoodGroups(mockedFoodGroups, foodGroupParameters);
        SetupServiceMockForAllFoodGroups(foodGroupParameters);
        
        _controller.ControllerContext.HttpContext = mockHttpContext.Object;

        // Act
        var result = await _controller.GetAllFoodGroups(foodGroupParameters);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedFoodItems = Assert.IsAssignableFrom<IEnumerable<FoodGroupsDto>>(okResult.Value);
        Assert.Equal(mockedFoodGroups.Count, returnedFoodItems.Count());
    }

    [Fact]
    public async Task GetFoodGroups_WhenCalled_SearchFoodGroupByDescription_ReturnsMatchingItems()
    {
        // Arrange
        var mockHttpContext = MockHttpContext();
        var foodGroupParameters = new FoodGroupParameters { SearchFoodGroupByDescription = "Fruit" };

        var expectedFoodGroups = GenerateMockFoodGroups().Where(f => f.Description.Contains(foodGroupParameters.SearchFoodGroupByDescription)).ToList();

        var mockedFoodGroups = GenerateMockFoodGroups();
        SetupRepositoryMockForAllFoodGroups(mockedFoodGroups, foodGroupParameters);
        SetupServiceMockForAllFoodGroups(foodGroupParameters);

        _controller.ControllerContext.HttpContext = mockHttpContext.Object;

        // Act
        var result = await _controller.GetAllFoodGroups(foodGroupParameters);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedFoodItems = Assert.IsAssignableFrom<IEnumerable<FoodGroupsDto>>(okResult.Value);
        Assert.Equal(expectedFoodGroups.Count, returnedFoodItems.Count()); // Checking counts first
        Assert.All(returnedFoodItems, dto => Assert.Contains(expectedFoodGroups, g => g.Description == dto.Description));
    }

    [Fact]
    public async Task GetFoodGroupFromFoodGroupCode_WhenCalled_ReturnsOkResult()
    {
        // Arrange
        var mockHttpContext = MockHttpContext();
        string testFoodGroupCode = "AN";
        
        var mockedFoodGroups = GenerateMockFoodGroups();
        SetupRepositoryMockForOneFoodGroup(mockedFoodGroups, testFoodGroupCode);
        SetupServiceMockForOneFoodGroup(testFoodGroupCode);
        
        _controller.ControllerContext.HttpContext = mockHttpContext.Object;

        // Act
        var result = await _controller.GetFoodGroup(testFoodGroupCode);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value); 
    }
    
    [Fact]
    public async Task GetFoodGroupFromFoodGroupCode_WhenCalled_ReturnsMatchingItem()
    {
        // Arrange
        var mockHttpContext = MockHttpContext();
        string testFoodGroupCode = "AN";
        var expectedFoodGroup =
            GenerateMockFoodGroups().SingleOrDefault(f => f.FoodGroupCode.Equals(testFoodGroupCode));
        
        var mockedFoodGroups = GenerateMockFoodGroups();
        SetupRepositoryMockForOneFoodGroup(mockedFoodGroups, testFoodGroupCode);
        SetupServiceMockForOneFoodGroup(testFoodGroupCode);
        
        _controller.ControllerContext.HttpContext = mockHttpContext.Object;

        // Act
        var result = await _controller.GetFoodGroup(testFoodGroupCode);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedFoodGroup = Assert.IsType<FoodGroupsDto>(okResult.Value);
        Assert.Equal(expectedFoodGroup.FoodGroupCode, returnedFoodGroup.FoodGroupCode);
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

    private List<FoodGroups> GenerateMockFoodGroups()
    {
        return new List<FoodGroups>
        {
            new FoodGroups
            {
                FoodGroupCode = "F",
                Description = "Fruit"
            },
            new FoodGroups
            {
                FoodGroupCode = "AN",
                Description = "Cakes"
            },
            new FoodGroups
            {
                FoodGroupCode = "D",
                Description = "Vegetables"
            },
        };
    }

    private void SetupRepositoryMockForAllFoodGroups(List<FoodGroups> database, FoodGroupParameters foodGroupParameters)
    {
        var filteredItems = database
            .AsQueryable()
            .Search(foodGroupParameters.SearchFoodGroupByDescription)
            .ToList();

        // Setup the mock to return the paged list when GetAllFoodItemsAsync is called with the specified parameters
        _repositoryManagerMock
            .Setup(repo => repo.FoodGroups.GetAllFoodGroupsAsync(foodGroupParameters, false))
            .ReturnsAsync(filteredItems);
    }

    private void SetupServiceMockForAllFoodGroups(FoodGroupParameters foodGroupParameters)
    {
        _service.Setup(s => s.FoodGroupsService.GetAllFoodGroups(foodGroupParameters, false))
            .ReturnsAsync((FoodGroupParameters parameters, bool trackChanges) =>
            {
                var foodGroups = _repositoryManagerMock.Object.FoodGroups.GetAllFoodGroupsAsync(parameters, trackChanges).Result;
                var foodGroupsDto = foodGroups.Select(fg => _mapper.Map<FoodGroupsDto>(fg));

                return foodGroupsDto;
            });
    }

    private void SetupRepositoryMockForOneFoodGroup(List<FoodGroups> database, string foodGroupCode)
    {
        var filteredItem = database
            .AsQueryable()
            .SingleOrDefault(x => x.FoodGroupCode.Equals(foodGroupCode));

        _repositoryManagerMock.Setup(repo => repo.FoodGroups.GetFoodGroupAsync(foodGroupCode, It.IsAny<bool>()))
            .ReturnsAsync(filteredItem);
    }

    private void SetupServiceMockForOneFoodGroup(string foodGroupCode)
    {
        _service.Setup(s => s.FoodGroupsService.GetFoodGroup(foodGroupCode, It.IsAny<bool>()))
            .ReturnsAsync((string foodGroupCode, bool trackChanges) =>
            {
                var result = _repositoryManagerMock.Object.FoodGroups.GetFoodGroupAsync(foodGroupCode, trackChanges)
                    .Result;
                var mappedResult = _mapper.Map<FoodGroupsDto>(result);

                return mappedResult;
            });
    }
}