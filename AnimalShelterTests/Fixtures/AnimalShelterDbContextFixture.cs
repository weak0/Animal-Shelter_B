using Animal_Shelter.Data;
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
    }

    public void Dispose()
    {
        Db.Database.EnsureDeleted();
        Db.Dispose();
    }
}