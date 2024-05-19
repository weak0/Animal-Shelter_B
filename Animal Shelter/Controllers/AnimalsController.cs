using Animal_Shelter.Entities;
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

    [HttpGet("/animals/{animalId:int}")]
    public async Task<ActionResult<Animal>> GetAnimalById([FromRoute]int animalId)
    {
        var serviceRespone = await _animalsService.GetAnimalById(animalId);
        return Ok(serviceRespone);
    }

    [HttpGet("/animals")]
    public async Task<ActionResult<List<Animal>>> GetAllAnimals()
    {
        var serviceResponse = await _animalsService.GetAllAnimals();
        return Ok(serviceResponse);
    }

    [HttpGet("/animals/shelter/{shelterId:int}")]
    public async Task<ActionResult<List<Animal>>> GetAllAnimalsByShelterId([FromRoute]int shelterId)
    {
        var serviceResponse = await _animalsService.GetAllAnimalsByShelterId(shelterId);
        return Ok(serviceResponse);
    }

    [HttpPost("/animals")]
    public async Task<ActionResult<AddAnimalDto>> AddAnimal([FromBody]AddAnimalDto dto)

    {
        var serviceResponse = await _animalsService.AddAnimal(dto);
        return Ok(serviceResponse);
    }

    [HttpDelete("/animals/{animalId:int}")]
    public async Task<ActionResult> DeleteAnimal([FromRoute]int animalId)
    {
        await _animalsService.DeleteAnimal(animalId);
        return NoContent();
    }
    
    [HttpPut("/animals/update")]
    public async Task<ActionResult<Animals>> UpdateAnimal([FromBody]UpdateAnimalDto dto)
    {
        var serviceResponse = await _animalsService.UpdateAnimal(dto);
        return Ok(serviceResponse);
    }
    
    [HttpGet("/animals/types")]
    public async Task<ActionResult<List<AnimalType>>> GetAnimalTypes()
    {
        var result = await _animalsService.GetAnimalTypes();
        return Ok(result);
    }
    
    [HttpGet("/animals/sizes")]
    public async Task<ActionResult<List<AnimalSize>>> GetAnimalSizes()
    {
        var result = await _animalsService.GetAnimalSizes();
        return Ok(result);
    }
    
}