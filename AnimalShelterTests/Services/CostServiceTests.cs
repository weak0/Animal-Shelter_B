// tip - on the end always clear unused usings
using System.ComponentModel.DataAnnotations;
using Animal_Shelter.Entities;
using Animal_Shelter.Mappers;
using Animal_Shelter.Models;
using Animal_Shelter.Services;
using AnimalShelterTests.Fixtures;
using AutoMapper;
using ValidationException = FluentValidation.ValidationException;

namespace AnimalShelterTests.Services;

public class CostServiceTests: IClassFixture<AnimalShelterDbContextFixture>
{
    private readonly AnimalShelterDbContextFixture _fixture;
    private readonly IConfigurationService _configurationService;
    private readonly CostService _costService;

    public CostServiceTests(AnimalShelterDbContextFixture fixture)
    {
        _fixture = fixture;
        var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AnimalShelterMappingProfile>()));
        var shelterService = new ShelterService(fixture.Db);
        var configurationService = new ConfigurationService(fixture.Db, mapper, shelterService);
        _costService = new CostService(_fixture.Db, mapper, configurationService);
    }   
    
    [Fact]
    public async Task AddCost_ShouldAddCostAndOk_WhenDataIsValid()
    {
        //Arrange
        var dto = new AddCostDto()
        {
            CostName = "test1",
            Category = CostsCategory.Maintenance,
            ShelterConfigId = 1,
            Cost = 100,
            PaymentPeriod = PaymentPeriod.Monthly
        };
        //Act
        var serviceResponse = await _costService.AddCost(dto);
        //Assert
        Assert.NotNull(serviceResponse);
        Assert.Equal(dto.CostName, serviceResponse.CostName);
        Assert.Equal(dto.Cost, serviceResponse.Cost);
    }
    
    [Fact]
    public async Task UpdateCost_ShouldUpdateCostAndOk_WhenDataIsValid()
    {
        //Arrange
        var dto = new UpdateCostDto()
        {
            CostId = 1,        
            CostName = "UpdatedCostName",
            Cost = 200
        };
        //Act
        await _costService.UpdateCost(1, dto);
        //Assert
        var updatedCost = await _fixture.Db.Costs.FindAsync(1);
        Assert.NotNull(updatedCost);
        Assert.Equal(dto.CostName, updatedCost.CostName);
        Assert.Equal(dto.Cost, updatedCost.Cost);
    }
    [Fact]
    public async Task DeleteCost_ShouldDeleteCostAndOk_WhenDataIsValid()
    {
        //Arrange
        var cost = new Costs()
        {
            CostId = 1
        };
        //Act
        await _costService.DeleteCost(cost.CostId);
        //Assert
        var deletedCost = await _fixture.Db.Costs.FindAsync(cost.CostId);
        Assert.Null(deletedCost);
    }
    
    [Theory]
    [InlineData("", 1, 1, 100, 1)]
    [InlineData("test2", 99, 1, 100, 1)]
    [InlineData("test2", 1, 1, -2, 1)]
    [InlineData("test2", 1, 1, 70000000, 1)]
    
public async Task AddCost_ShouldThrowException_WhenDataIsInvalid(string costName, int category, int shelterConfigId, double cost, int paymentPeriod)
    {
        //Arrange
        var dto = new AddCostDto()
        {
            CostName = costName,
            Category = (CostsCategory)category,
            ShelterConfigId = shelterConfigId,
            Cost = (decimal)cost,
            PaymentPeriod = (PaymentPeriod)paymentPeriod
        };
        //Act & Assert
        await Assert.ThrowsAsync<ValidationException>(async () => await _costService.AddCost(dto));
    }
}