using Animal_Shelter.Data.ExteranalData;
using Animal_Shelter.Entities;
using Animal_Shelter.Exceptions;
using Animal_Shelter.Mappers;
using Animal_Shelter.Models;
using Animal_Shelter.Services;
using AnimalShelterTests.Fixtures;
using AnimalShelterTests.Mocks;
using AutoMapper;


namespace AnimalShelterTests.Services
{
    
    public class AnimalsServiceTests : IClassFixture<AnimalShelterDbContextFixture>
    {
        private readonly AnimalShelterDbContextFixture _fixture;
        private readonly AnimalsService _animalsService;

        public AnimalsServiceTests(AnimalShelterDbContextFixture fixture)
        {
            _fixture = fixture;
            var mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AnimalMapper>();
                cfg.AddProfile<CostMapper>();
            }));
            var shelterService = new ShelterService(fixture.Db);
            var costService = new CostService(fixture.Db, mapper);
            var priceJsonApiClient = new PriceJsonApiClientMock();
            _animalsService = new AnimalsService(fixture.Db, mapper, shelterService, costService, priceJsonApiClient);
        }

        [Fact]
        public async Task AddAnimal_ShouldAddAnimalAndOk_WhenDataIsValid()
        {
            //Arrange
            var dto = new AddAnimalDto()
            {
                AnimalName = "test1",
                TypeId = (int)AnimalTypeEnum.Cat,
                SizeId = (int)AnimalSizeEnum.Small,
                ShelterId = 1,

            };
            //Act
            var serviceResponse = await _animalsService.AddAnimal(dto);
            //Assert
            Assert.NotNull(serviceResponse);
            Assert.Equal(dto.AnimalName, serviceResponse.AnimalName);
            Assert.Equal(dto.TypeId, serviceResponse.TypeId);
            Assert.Equal(dto.SizeId, serviceResponse.SizeId);
            Assert.Equal(dto.ShelterId, serviceResponse.ShelterId);
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

        [Fact]
        public async Task AddAnimal_ShouldThrowException_WhenShelterDoesNotExist()
        {
            //Arrange
            var dto = new AddAnimalDto()
            {
                AnimalName = "test2",
                TypeId = (int)AnimalTypeEnum.Cat,
                SizeId = (int)AnimalSizeEnum.Small,
                ShelterId = 100,

            };

            //Act
            async Task AddAnimal() => await _animalsService.AddAnimal(dto);
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(AddAnimal);

        }

        [Fact]
        public async Task AddAnimal_ShouldThrowException_WhenAnimalNameIsEmpty()
        {
            //Arrange
            var dto = new AddAnimalDto()
            {
                AnimalName = "",
                TypeId = (int)AnimalTypeEnum.Cat,
                SizeId = (int)AnimalSizeEnum.Small,
                ShelterId = 1,

            };

            //Act
            async Task AddAnimal() => await _animalsService.AddAnimal(dto);
            //Assert
            await Assert.ThrowsAsync<ValidationException>(AddAnimal);
        }

        [Fact]
        public async Task AddAnimal_ShouldThrowException_WhenAnimalTypeIsInvalid()
        {
            //Arrange
            var dto = new AddAnimalDto()
            {
                AnimalName = "test1",
                TypeId = 100,
                SizeId = (int)AnimalSizeEnum.Small,
                ShelterId = 1,

            };

            //Act
            async Task AddAnimal() => await _animalsService.AddAnimal(dto);
            //Assert
            await Assert.ThrowsAsync<BadRequestException>(AddAnimal);
        }

        [Fact]
        public async Task GetAnimalById_ShouldReturnAnimal_WhenDataIsValid()
        {
            //Arrange
            var animal = new Animals()
            {
                AnimalName = "test1",
                TypeId = (int)AnimalTypeEnum.Cat,
                SizeId = (int)AnimalSizeEnum.Small,
                ShelterId = 1,
            };
            _fixture.Db.Animals.Add(animal);
            await _fixture.Db.SaveChangesAsync();
            //Act
            var serviceResponse = await _animalsService.GetAnimalById(animal.AnimalId);
            //Assert
            Assert.NotNull(serviceResponse);
            Assert.Equal(animal.AnimalName, serviceResponse.AnimalName);
            Assert.Equal(animal.TypeId, serviceResponse.TypeId);
            Assert.Equal(animal.SizeId, serviceResponse.SizeId);
            Assert.Equal(animal.ShelterId, serviceResponse.ShelterId);
        }

    }
}
