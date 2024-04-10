using Animal_Shelter.Data;
using Animal_Shelter.Models;
using Animal_Shelter.Serivces;
using Microsoft.AspNetCore.Mvc;

namespace Animal_Shelter.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly AnimalShelterDbContext _dbContext;
    private readonly IAuthSerivce _authService;

    public AuthController(AnimalShelterDbContext dbContext, IAuthSerivce authService)
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

    [HttpGet("hello")]
    public ActionResult<string> SayHello()
    {
        var test = "xd";
        return Ok("Hello Word!");
    }

}