using Animal_Shelter.Entities;
using Microsoft.EntityFrameworkCore;
namespace Animal_Shelter.Data;

public class AnimalShelterDbContext : DbContext
{
    public AnimalShelterDbContext(DbContextOptions<AnimalShelterDbContext> options) : base (options)
    {
        
    }

    public DbSet<Animals> Animals { get; set; }
    public DbSet<AnimalShelter> AnimalShelters { get; set; }
}