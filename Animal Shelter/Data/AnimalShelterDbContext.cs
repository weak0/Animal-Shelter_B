using Animal_Shelter.Entities;
using Microsoft.EntityFrameworkCore;
namespace Animal_Shelter.Data;

public class AnimalShelterDbContext : DbContext
{
    public AnimalShelterDbContext(DbContextOptions<AnimalShelterDbContext> options) : base (options)
    {
        
    }
    public DbSet<Shelter> Shelters { get; set; }
    public DbSet<Animals> Animals { get; set; }
    public DbSet<AnimalShelterConfiguration> AnimalShelterConfiguration { get; set; }
    public DbSet<Costs> Costs { get; set; }
    
    //data seed
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Shelter>().HasData(
        new Shelter{ShelterId = 1, Email = "happyanimals@email.com", Name = "Happy Animals", HashedPassword = "happyanimals"},
        new Shelter{ShelterId = 2, Email = "pipsqueakery@email.com", Name = "Pipsqueakery: A Shelter For Small Animals", HashedPassword = "pipsqueakery"}
        );
        
        modelBuilder.Entity<Animals>().HasData(
        new Animals {AnimalId = 1, AnimalName = "Bobby", ShelterId = 1, Size = AnimalSize.Medium, Type = AnimalType.Dog },
        new Animals{AnimalId = 2, AnimalName = "Celestine", ShelterId = 2, Type = AnimalType.Other, Size = AnimalSize.Small},
        new Animals{AnimalId = 3, AnimalName = "Monet", ShelterId = 1, Type = AnimalType.Dog, Size = AnimalSize.Medium}
        );

        modelBuilder.Entity<AnimalShelterConfiguration>().HasData(
            new AnimalShelterConfiguration
                { ShelterConfigId = 1, ShelterConfigName = "Happy Animals: Standard", ShelterId = 1 },
            new AnimalShelterConfiguration
                { ShelterConfigId = 2, ShelterConfigName = "Pipsqueakery: Standard", ShelterId = 2 }
        );
    }
}