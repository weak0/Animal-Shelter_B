using Animal_Shelter.Data;
using Animal_Shelter.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AnimalShelterTests.Fixtures;

public class AnimalShelterDbContextFixture : IDisposable
{
    public AnimalShelterDbContext Db { get;}
    
    public AnimalShelterDbContextFixture()
    {
        var options = new DbContextOptionsBuilder<AnimalShelterDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        Db = new AnimalShelterDbContext(options);
        Db.Database.EnsureCreated();
        AddTestUser(Db);
        AddTestCost(Db);
    }

    public void Dispose()
    {
        Db.Database.EnsureDeleted();
        Db.Dispose();
    }
    private void AddTestUser(AnimalShelterDbContext db)
    {
        var user = new Shelter();
        user.Name = "test";
        user.Email = "test@gmail.com";
        user.HashedPassword = new PasswordHasher<Shelter>().HashPassword(user, "test123");
        db.Shelters.Add(user);
        db.SaveChanges();
    }
    
    private void AddTestCost(AnimalShelterDbContext db)
    {
        var cost = new Costs();
        cost.CostName = "test1";
        cost.Category = "Maintenance";
        cost.ShelterConfigId = 1;
        cost.Cost = 100;
        cost.PaymentPeriod = "Monthly";
        db.Costs.Add(cost);
        db.SaveChanges();
    }
}