using Microsoft.EntityFrameworkCore;

namespace Animal_Shelter.Entities;

public class AnimalShelterContext : DbContext
{
    public AnimalShelterContext(DbContextOptions<AnimalShelterContext> options) : base(options)
    {
    }
    
    
}