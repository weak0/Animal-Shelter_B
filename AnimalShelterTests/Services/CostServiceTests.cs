// tip - on the end always clear unused usings
using System.ComponentModel.DataAnnotations;
using Animal_Shelter;
using Animal_Shelter.Entities;
using Animal_Shelter.Exceptions;
using Animal_Shelter.Mappers;
using Animal_Shelter.Models;
using Animal_Shelter.Services;
using AnimalShelterTests.Fixtures;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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
        // try to pass for test only data which is needed, this dto is redundant
        var dto = new UpdateCostDto()
        {
            CostName = "test1",
            Category = CostsCategory.Maintenance,
            ShelterConfigId = 1,
            Cost = 100,
            PaymentPeriod = PaymentPeriod.Monthly
        };
        //Act
        dto.CostName = "UpdatedCostName";
        dto.Cost = 200;
        // this 1 -  how u know that it will be 1? , magic number
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
    [InlineData("test2", CostsCategory.Maintenance, 0, 100, PaymentPeriod.Monthly)]
    //u have validation only on id, in first case test will fail because u propably has cost with id == 1 
    
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
        // why are u creating variable if u dont need it?
        var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _costService.AddCost(dto));
    }
}