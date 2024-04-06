using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Animal_Shelter.Data;
using Animal_Shelter.Models;
using AnimalShelterTests.Fixtures;
using Microsoft.EntityFrameworkCore;

namespace AnimalShelterTests.Controllers;

public class AuthControllerTests : IClassFixture<AnimalShelterDbContextFixture>
{
    private readonly HttpClient _client;
    private readonly AnimalShelterDbContext _db;
    
    public AuthControllerTests(AnimalShelterDbContextFixture fixture)
    {
        var factory = new CustomWebApplicationFactory<Program>(fixture.Db);
        _client = factory.CreateClient();
        _db = factory.Context;
    }
    
    [Fact]
    public async Task CreateShelter_ShouldCreateNewShelterAndOk_WhenDataIsValid()
    {
        //Arrange
        var dto = new AuthShelterRegisterDto()
        {
            Name = "test1",
            Email = "test1@gmail.com",
            Password = "test123"
        };
        //Act
        var response = await _client.PostAsJsonAsync("auth/register", dto);
        //Assert
        response.EnsureSuccessStatusCode();
        var shelter = await _db.Shelters.FirstOrDefaultAsync(x => x.Email == dto.Email);
        Assert.NotNull(shelter);
        Assert.Equal(dto.Name, shelter.Name);
        Assert.Equal(dto.Email, shelter.Email);
        Assert.Equal(84, shelter.HashedPassword.Length);
    }
    [Fact]
    public async Task Login_ShouldReturnTokenAndOk_WhenDataIsValid()
    {
        //Arrange
        var login = new AuthShelterDto()
        {
            Email = "test@gmail.com",
            Password = "test123"
        };
        //Act
       var respone  = await _client.PostAsJsonAsync("auth/login", login);
        //Assert
        respone.EnsureSuccessStatusCode();
        var token = await respone.Content.ReadAsStringAsync();
        Assert.NotNull(token);
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenS = tokenHandler.ReadJwtToken(token);
        Assert.True(tokenS.Claims.Any(x => x.Type == ClaimTypes.Email && x.Value == login.Email));
    }
    
}