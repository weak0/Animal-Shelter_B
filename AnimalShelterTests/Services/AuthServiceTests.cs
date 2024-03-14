using Animal_Shelter;
using Animal_Shelter.Entities;
using Animal_Shelter.Models;
using Animal_Shelter.Models.Validators;
using Animal_Shelter.Serivces;
using AnimalShelterTests.Fixtures;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AnimalShelterTests.Services;

public class AuthServiceTests : IClassFixture<AnimalShelterDbContextFixture>
{
    private readonly AnimalShelterDbContextFixture _fixture;
    private readonly IAuthSerivce authService;

    public AuthServiceTests(AnimalShelterDbContextFixture fixture)
    {
        _fixture = fixture;
        authService = new AuthService(_fixture.Db, new CreateShelterValidator(), new PasswordHasher<Shelter>(),
            new AuthenticationSettings());
    }

    [Fact]
    public async Task CreateUser_ShouldCreateNewShelter_WhenDataIsValid()
    {
        //Arrange
        var dto = new AuthShelterRegisterDto()
        {
            Name = "test",
            Email = "test@gmail.com",
            Password = "test123"
        };

        //Act
        await authService.CreateUser(dto);
        //Assert
        var shelter = await _fixture.Db.Shelters.FirstOrDefaultAsync(x => x.Email == dto.Email);
        Assert.NotNull(shelter);
        Assert.Equal(dto.Name, shelter.Name);
        Assert.Equal(dto.Email, shelter.Email);
        Assert.Equal(84, shelter.HashedPassword.Length);
    }

    [Theory]
    [InlineData("test", "", "test123")]
    [InlineData("", "test1@gmail.com", "test123")]
    [InlineData("test", "test2@gmail.com", "")]
    public async Task CreateUser_ShouldThrowException_WhenDataIsInvalid(string name, string email, string password)
    {
        //Arrange
        var dto = new AuthShelterRegisterDto()
        {
            Name = name,
            Email = email,
            Password = password
        };

        //Act
        async Task Act() => await authService.CreateUser(dto);
        //Assert
        await Assert.ThrowsAsync<ValidationException>(Act);
    }

    [Fact]
    public async Task CreateUser_ShouldThrowException_WhenUserAlreadyExist()
    {
        //Arrange
        var dto = new AuthShelterRegisterDto()
        {
            Name = "test3",
            Email = "test3@gmail.com",
            Password = "test123"
        };
        await authService.CreateUser(dto);

        //Act
        async Task Act() => await authService.CreateUser(dto);
        //Assert
        await Assert.ThrowsAsync<ValidationException>(Act);
    }
}
