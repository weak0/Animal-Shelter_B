using Animal_Shelter.Data;
using Animal_Shelter.Entities;
using Animal_Shelter.Exceptions;
using Animal_Shelter.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Animal_Shelter.Services;

public interface IAnimalsService
{
    Task<GetAnimalDto> GetAnimalById(int animalId);
    Task<List<GetAnimalDto>> GetAllAnimals();
    Task<List<GetAnimalDto>> GetAllAnimalsByShelterId(int shelterId);
    Task<AddAnimalDto> AddAnimal(AddAnimalDto dto);

    //Task UpdateAnimal(int animalId);
    Task DeleteAnimal(int animalId);
}
public class AnimalsService : IAnimalsService
{
    private readonly AnimalShelterDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IShelterService _shelterService;

    public AnimalsService(AnimalShelterDbContext dbContext, IMapper mapper, IShelterService shelterService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _shelterService = shelterService;
    }

    public async Task<GetAnimalDto> GetAnimalById(int animalId)
    {
        var animal = await _dbContext.Animals.FindAsync(animalId) ?? throw new NotFoundException("Animal id not found.");
        return _mapper.Map<GetAnimalDto>(animal);
    }

    public async Task<List<GetAnimalDto>> GetAllAnimals()
    {
        var animals = await _dbContext.Animals.ToListAsync();
        return _mapper.Map<List<GetAnimalDto>>(animals);
    }

    public async Task<List<GetAnimalDto>> GetAllAnimalsByShelterId(int shelterId)
    {
        var animals = await _dbContext.Animals.Where(a=>a.ShelterId == shelterId).ToListAsync() ?? throw new NotFoundException("Animal shelter id not found.");
        return _mapper.Map<List<GetAnimalDto>>(animals);
    }

    public async Task<AddAnimalDto> AddAnimal(AddAnimalDto dto)
    {
        var animal = _mapper.Map<Animals>(dto);
        var shelterName = await _shelterService.GetShelterName(dto.ShelterId);

        if (animal.ShelterId != dto.ShelterId)
            throw new NotFoundException($"Is the chosen shelter correct? {shelterName} is not the same as {dto.ShelterId}.");
        if (animal.AnimalName == "")
            throw new ValidationException("Animal name is required.");
        if (animal.Type > (AnimalType)3)
            throw new BadRequestException("Invalid animal type. Choose between Dog (1), Cat (2), or Other (3).");

        await _dbContext.Animals.AddAsync(animal);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<AddAnimalDto>(animal);
    }

    public async Task DeleteAnimal(int animalId)
    {
        var animal = await _dbContext.Animals.FindAsync(animalId) ?? throw new NotFoundException("Animal id not found.");
        _dbContext.Animals.Remove(animal);
        await _dbContext.SaveChangesAsync();

    }
}
