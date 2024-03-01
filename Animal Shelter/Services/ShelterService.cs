using Animal_Shelter.Data;
using Animal_Shelter.Exceptions;

namespace Animal_Shelter.Services;

public interface IShelterService
{
    Task<string> GetShelterName(int shelterId);
}
public class ShelterService : IShelterService
{
    private readonly AnimalShelterDbContext _dbContext;

    public ShelterService(AnimalShelterDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<string> GetShelterName(int shelterId)
    {
        var shelter = await _dbContext.Shelters.FindAsync(shelterId) ?? throw new NotFoundException("The Animal Shelter ID not found.");
        
        return shelter.Name;
      
    }
}