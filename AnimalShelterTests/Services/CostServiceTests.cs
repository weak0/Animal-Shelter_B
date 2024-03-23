using System.ComponentModel.DataAnnotations;
using Animal_Shelter.Entities;
using Animal_Shelter.Models;
using Animal_Shelter.Models.Validators;
using Animal_Shelter.Serivces;
using Animal_Shelter.Services;
using AnimalShelterTests.Fixtures;
using AnimalShelterTests.Mocks;

namespace AnimalShelterTests.Services;

public class CostServiceTests: IClassFixture<AnimalShelterDbContextFixture>
{
    private readonly AnimalShelterDbContextFixture _fixture;
    private readonly IConfigurationService _configurationService;
    
    public CostServiceTests(AnimalShelterDbContextFixture fixture)
    {
        _fixture = fixture;
    }   
    
    [Fact]
    public async Task AddCost_ShouldAddCostAndOk_WhenDataIsValid()
    {
        //Arrange
        var costService = new CostService(_fixture.Db);
        var dto = new AddCostDto()
        {
            CostName = "test1",
            Category = "Maintenance",
            ShelterConfigId = 1,
            Cost = 100,
            PaymentPeriod = "Monthly"
        };
        //Act
        var serviceResponse = await costService.AddCost(dto);
        //Assert
        Assert.NotNull(serviceResponse);
        Assert.Equal(dto.CostName, serviceResponse.CostName);
        Assert.Equal(dto.Cost, serviceResponse.Cost);
    }
    [Fact]
    public async Task UpdateCost_ShouldUpdateCostAndOk_WhenDataIsValid()
    {
        //Arrange
        var costService = new CostService(_fixture.Db);
        var dto = new UpdateCostDto()
        {
            CostName = "test1",
            Category = "Maintenance",
            ShelterConfigId = 1,
            Cost = 100,
            PaymentPeriod = "Monthly"
        };
        //Act
        var serviceResponse = await costService.UpdateCost(dto);
        //Assert
        Assert.NotNull(serviceResponse);
        Assert.Equal(dto.CostName, serviceResponse.CostName);
        Assert.Equal(dto.Cost, serviceResponse.Cost);
    }
    [Fact]
    public async Task DeleteCost_ShouldDeleteCostAndOk_WhenDataIsValid()
    {
        //Arrange
        var costService = new CostService(_fixture.Db);
        var cost = new Cost()
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
        await costService.DeleteCost(cost.CostId);
        //Assert
        var deletedCost = await _fixture.Db.Costs.FindAsync(cost.CostId);
        Assert.Null(deletedCost);
    }
    
    [Theory]
    [InlineData("", "Maintenance", 1, 100, "Monthly")]
    [InlineData("test1", "", 1, 100, "Monthly")]
    [InlineData("test1", "Maintenance", 0, 100, "Monthly")]
    [InlineData("test1", "Maintenance", 1, 0, "Monthly")]
    
public async Task AddCost_ShouldThrowException_WhenDataIsInvalid(string costName, string category, int shelterConfigId, double cost, string paymentPeriod)
    {
        //Arrange
        var costService = new CostService(_fixture.Db);
        var dto = new AddCostDto()
        {
            CostName = costName,
            Category = category,
            ShelterConfigId = shelterConfigId,
            Cost = cost,
            PaymentPeriod = paymentPeriod
        };
        //Act
        async Task Act() => await costService.AddCost(dto);
        //Assert
        await Assert.ThrowsAsync<ValidationException>(Act);
    }
}