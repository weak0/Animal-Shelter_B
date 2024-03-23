using Animal_Shelter.Data;
using Animal_Shelter.Models;
using AnimalShelterTests.Fixtures;
using Microsoft.EntityFrameworkCore;

namespace AnimalShelterTests.Controllers;

public class CostsControllerTests
{
    private readonly HttpClient _client;
    private readonly AnimalShelterDbContext _db;
    
    public CostsControllerTests(AnimalShelterDbContextFixture fixture)
    {
        var factory = new CustomWebApplicationFactory<Program>(fixture.Db);
        _client = factory.CreateClient();
        _db = factory.Context;
    }
    
    [Fact]
    public async Task AddCost_ShouldAddCostAndOk_WhenDataIsValid()
    {
        //Arrange
        var dto = new AddCostDto()
        {
            CostName = "test1",
            Category = "Maintenance",
            ShelterConfigId = 1,
            Cost = 100,
            PaymentPeriod = "Monthly"
        };
        //Act
        var response = await _client.PostAsJsonAsync("configuration/costs", dto);
        //Assert
        response.EnsureSuccessStatusCode();
        var cost = await _db.Costs.FirstOrDefaultAsync(x => x.CostName == dto.CostName);
        Assert.NotNull(cost);
        Assert.Equal(dto.CostName, cost.CostName);
        Assert.Equal(dto.Cost, cost.Cost);
    }
    
    [Fact]
    public async Task UpdateCost_ShouldUpdateCostAndOk_WhenDataIsValid()
    {
        //Arrange
        var dto = new UpdateCostDto()
        {
            CostName = "test1",
            Category = "Maintenance",
            ShelterConfigId = 1,
            Cost = 100,
            PaymentPeriod = "Monthly"
        };
        //Act
        var response = await _client.PutAsJsonAsync($"configuration/costs/{dto.CostId}", dto);
        //Assert
        response.EnsureSuccessStatusCode();
        var updatedCost = await _db.Costs.FirstOrDefaultAsync(x => x.CostId == dto.CostId);
        Assert.NotNull(updatedCost);
        Assert.Equal(dto.CostName, updatedCost.CostName);
        Assert.Equal(dto.Cost, updatedCost.Cost);
    }

    [Fact]
    public async Task DeleteCost_ShouldDeleteCostAndOk_WhenDataIsValid()
    {
        //Arrange
        var dto = new UpdateCostDto()
        {
            CostName = "test1",
            Category = "Maintenance",
            ShelterConfigId = 1,
            Cost = 100,
            PaymentPeriod = "Monthly"
        };
        await _db.SaveChangesAsync();
        //Act
        var response = await _client.DeleteAsync($"configuration/costs/{dto.CostId}");
        //Assert
        response.EnsureSuccessStatusCode();
        var deletedCost = await _db.Costs.FirstOrDefaultAsync(x => x.CostId == dto.CostId);
        Assert.Null(deletedCost);
    }
}