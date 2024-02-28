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
}