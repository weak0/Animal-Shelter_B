using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Animal_Shelter.Data;
using Animal_Shelter.Entities;
using Animal_Shelter.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Animal_Shelter.Serivces;

public interface IAuthSerivce
{
    Task CreateUser(AuthShelterRegisterDto dto);
    Task<string> Login(AuthShelterDto dto);
}

public class AuthService : IAuthSerivce
{
    private readonly AnimalShelterDbContext _db;
    private readonly IValidator<AuthShelterDto> _createShelterValidator;
    private readonly IPasswordHasher<Shelter> _passwordHasher;
    private readonly AuthenticationSettings _authSettings;

    public AuthService(AnimalShelterDbContext db, IValidator<AuthShelterDto> createShelterValidator, IPasswordHasher<Shelter> passwordHasher, AuthenticationSettings authSettings)
    {
        _db = db;
        _createShelterValidator = createShelterValidator;
        _passwordHasher = passwordHasher;
        this._authSettings = authSettings;
    }
    public async Task CreateUser(AuthShelterRegisterDto dto)
    {
        var validationResult = await _createShelterValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        var newShelter = new Shelter()
        {
            Name = dto.Name,
            Email = dto.Email,
        };    
        newShelter.HashedPassword = _passwordHasher.HashPassword(newShelter, dto.Password);
        await _db.Shelters.AddAsync(newShelter);
        await _db.SaveChangesAsync();
    }
    
    public async Task<string> Login(AuthShelterDto dto)
    {
        var shelter = await _db.Shelters.FirstOrDefaultAsync(u => u.Email == dto.Email) ??
                   throw new BadHttpRequestException("Wrong email or password");
        var comparePassword = _passwordHasher.VerifyHashedPassword(shelter, shelter.HashedPassword, dto.Password);
        if(comparePassword != PasswordVerificationResult.Success)
        {
            throw new BadHttpRequestException("Wrong email or password");
        }
        var token = GenerateToken(shelter);
        return token;
    }
    
    private string GenerateToken(Shelter shelter)
    {
        var claims = new List<Claim>()
        {
            new (ClaimTypes.NameIdentifier,  shelter.ShelterId.ToString()),
            new (ClaimTypes.Email,  shelter.Email),
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.JwtKey));
        var creed = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(_authSettings.ExpiresDate);
        var token = new JwtSecurityToken(
            issuer: _authSettings.JwtIssuer,
            audience: _authSettings.JwtIssuer,
            claims: claims,
            expires: expires,
            signingCredentials: creed
        );
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
    
}

