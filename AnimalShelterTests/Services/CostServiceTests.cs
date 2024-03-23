using System.ComponentModel.DataAnnotations;
using Animal_Shelter;
using Animal_Shelter.Entities;
using Animal_Shelter.Mappers;
using Animal_Shelter.Models;
using Animal_Shelter.Models.Validators;
using Animal_Shelter.Serivces;
using Animal_Shelter.Services;
using AnimalShelterTests.Fixtures;
using AnimalShelterTests.Mocks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

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
            CostName = "test1",
            Category = CostsCategory.Maintenance,
            ShelterConfigId = 1,
            Cost = 100,
            PaymentPeriod = PaymentPeriod.Monthly
        };
        //Act
         await _costService.UpdateCost(dto.CostId, dto);
        //Assert
        // Assert.NotNull(serviceResponse);
        // Assert.Equal(dto.CostName, serviceResponse.CostName);
        // Assert.Equal(dto.Cost, serviceResponse.Cost);
    }
    [Fact]
    public async Task DeleteCost_ShouldDeleteCostAndOk_WhenDataIsValid()
    {
        //Arrange
        var cost = new Costs()
        {
            CostName = "test1",
            Category = CostsCategory.Maintenance,
            ShelterConfigId = 1,
            Cost = 100,
            PaymentPeriod = PaymentPeriod.Monthly
        };
        _fixture.Db.Costs.Add(cost);
        await _fixture.Db.SaveChangesAsync();
        //Act
        await _costService.DeleteCost(cost.CostId);
        //Assert
        var deletedCost = await _fixture.Db.Costs.FindAsync(cost.CostId);
        Assert.Null(deletedCost);
    }
    
    [Theory]
    [InlineData("", CostsCategory.Maintenance, 1, 100, PaymentPeriod.Monthly)]
    [InlineData("test1", "", 1, 100, PaymentPeriod.Monthly)]
    [InlineData("test1", CostsCategory.Maintenance, 0, 100, PaymentPeriod.Monthly)]
    [InlineData("test1", CostsCategory.Maintenance, 1, 0, PaymentPeriod.Monthly)]
    
public async Task AddCost_ShouldThrowException_WhenDataIsInvalid(string costName, CostsCategory category, int shelterConfigId, double cost, PaymentPeriod paymentPeriod)
    {
        //Arrange
        var dto = new AddCostDto()
        {
            CostName = costName,
            Category = category,
            ShelterConfigId = shelterConfigId,
            Cost = (decimal)cost,
            PaymentPeriod = paymentPeriod
        };
        //Act
        async Task Act() => await _costService.AddCost(dto);
        //Assert
        await Assert.ThrowsAsync<ValidationException>(Act);
    }
}