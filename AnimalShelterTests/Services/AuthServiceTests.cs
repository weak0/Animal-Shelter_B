using Animal_Shelter.Entities;
using Animal_Shelter.Models;
using Animal_Shelter.Models.Validators;
using Animal_Shelter.Serivces;
using AnimalShelterTests.Fixtures;
using AnimalShelterTests.Mocks;
using FluentValidation;

using Microsoft.AspNetCore.Identity;


namespace AnimalShelterTests.Services;

public class AuthServiceTests : IClassFixture<AnimalShelterDbContextFixture>
{
    private readonly AnimalShelterDbContextFixture _fixture;
    private readonly IAuthSerivce authService;

    public AuthServiceTests(AnimalShelterDbContextFixture fixture)
    {
        _fixture = fixture;
        authService = new AuthService(_fixture.Db, new CreateShelterValidator(), new PasswordHasher<Shelter>(),
            new AuthenticationSettingsMock());
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
            Name = "test",
            Email = "test@gmail.com",
            Password = "test123"
        };
        //Act
        async Task Act() => await authService.CreateUser(dto);
        //Assert
        await Assert.ThrowsAsync<ValidationException>(Act);
    }
    
    [Theory]
    [InlineData("", "test123")]
    [InlineData( "test@gmail.com", "")]
    [InlineData( "test5@gmail.com", "test123")]
    public async Task Login_ShouldThrowException_WhenDataIsInvalid(string email, string password)
    {
        //Arrange
        var login = new AuthShelterDto()
        {
            Email = email,
            Password = password
        }; 
        
        //Act
        async Task Act() => await authService.Login(login);
        //Assert
        await Assert.ThrowsAsync<BadHttpRequestException>(Act);
    }

    [Fact]
    public async Task Login_ShouldThrowException_WhenPasswordIsInvalid()
    {
        //Arrange
        var login = new AuthShelterDto()
        {
            Email = "test@gmail.com",
            Password = "wrongpassword"
        };
        //Act
        async Task Act() => await authService.Login(login);
        //Assert
        await Assert.ThrowsAsync<BadHttpRequestException>(Act);
    }
}
    
