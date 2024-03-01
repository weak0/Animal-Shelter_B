using Animal_Shelter.Models;
using Animal_Shelter.Services;
using Microsoft.AspNetCore.Mvc;

namespace Animal_Shelter.Controllers;

[ApiController]
[Route("[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly IAnimalsService _animalsService;

    public AnimalsController(IAnimalsService animalsService)
    {
        _animalsService = animalsService;
    }

    [HttpGet("Get Animal by ID")]
    public async Task<ActionResult<GetAnimalDto>> GetAnimalById(int animalId)
    {
        var serviceRespone = await _animalsService.GetAnimalById(animalId);
        return Ok(serviceRespone);
    }

    [HttpGet("Get All Animals")]
    public async Task<ActionResult<List<GetAnimalDto>>> GetAllAnimals()
    {
        var serviceResponse = await _animalsService.GetAllAnimals();
        return Ok(serviceResponse);
    }

    [HttpGet("Get All Animals by Shelter ID")]
    public async Task<ActionResult<List<GetAnimalDto>>> GetAllAnimalsByShelterId(int shelterId)
    {
        var serviceResponse = await _animalsService.GetAllAnimalsByShelterId(shelterId);
        return Ok(serviceResponse);
    }

    [HttpDelete("Delete Animal")]
    public async Task<ActionResult> DeleteAnimal(int animalId)
    {
        await _animalsService.DeleteAnimal(animalId);
        return NoContent();
    }
}