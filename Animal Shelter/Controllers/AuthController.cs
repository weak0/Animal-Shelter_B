using Animal_Shelter.Entities;
using Animal_Shelter.Models;
using Animal_Shelter.Serivces;
using Microsoft.AspNetCore.Mvc;

namespace Animal_Shelter.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly AnimalShelterContext _dbContext;
    private readonly IAuthSerivce _authService;

    public AuthController(AnimalShelterContext dbContext, IAuthSerivce authService)
    {
        _dbContext = dbContext;
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> CreateShelter([FromBody] AuthShelterRegisterDto newShelterDto)
    {

        await _authService.CreateUser(newShelterDto);
        return Ok(newShelterDto.Email);
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<string>> Login([FromBody] AuthShelterDto shelter)
    {
        var token = await _authService.Login(shelter);
        return Ok(token);
    }

}