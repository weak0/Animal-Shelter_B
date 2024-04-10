using Animal_Shelter.Entities;
using Animal_Shelter.Exceptions;
using Animal_Shelter.Mappers;
using Animal_Shelter.Models;
using Animal_Shelter.Services;
using AnimalShelterTests.Fixtures;
using AutoMapper;


namespace AnimalShelterTests.Services;

public class AnimalsServiceTests: IClassFixture<AnimalShelterDbContextFixture>
{
    private readonly AnimalShelterDbContextFixture _fixture;
    private readonly IConfigurationService _configurationService;
    private readonly AnimalsService _animalsService;

    public AnimalsServiceTests(AnimalShelterDbContextFixture fixture)
    {
        _fixture = fixture;
        var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AnimalShelterMappingProfile>()));
        var shelterService = new ShelterService(fixture.Db);
        var configurationService = new ConfigurationService(fixture.Db, mapper, shelterService);
        _animalsService = new AnimalsService(fixture.Db, mapper, shelterService);
    }
    
    [Fact]
    public async Task AddAnimal_ShouldAddAnimalAndOk_WhenDataIsValid()
    {
        //Arrange
        var dto = new AddAnimalDto()
        {
            AnimalName = "test1",
            Type = AnimalType.Cat,
            Size = AnimalSize.Small,
            ShelterId = 1,
            
        };
        //Act
        var serviceResponse = await _animalsService.AddAnimal(dto);
        //Assert
        Assert.NotNull(serviceResponse);
        Assert.Equal(dto.AnimalName, serviceResponse.AnimalName);
        Assert.Equal(dto.Type, serviceResponse.Type);
        Assert.Equal(dto.Size, serviceResponse.Size);
        Assert.Equal(dto.ShelterId, serviceResponse.ShelterId);
    }
    
    [Fact]
    public async Task AddAnimal_ShouldThrowException_WhenShelterDoesNotExist()
    {
        //Arrange
        var dto = new AddAnimalDto()
        {
            AnimalName = "test2",
            Type = AnimalType.Cat,
            Size = AnimalSize.Small,
            ShelterId = 100,
            
        };
        //Act
        async Task AddAnimal() => await _animalsService.AddAnimal(dto);
        //Assert
        await Assert.ThrowsAsync<NotFoundException>(AddAnimal);
        
    }

    [Fact]
    public async Task DeleteAnimal_ShouldThrowException_WhenAnimalIdDoesNotExist()
    {
        //Arrange
        var animal = new Animals()
        {
            AnimalId = 200
        };
        //Act
       async Task DeleteAnimal() => await _animalsService.DeleteAnimal(animal.AnimalId);
        //Assert
        await Assert.ThrowsAsync<NotFoundException>(DeleteAnimal);
    }
    
}