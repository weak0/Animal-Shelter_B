﻿using Animal_Shelter.Models;
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
    public async Task<ActionResult<GetAnimalDto>> GetAnimalById([FromRoute]int animalId)
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
    public async Task<ActionResult<List<GetAnimalDto>>> GetAllAnimalsByShelterId([FromRoute]int shelterId)
    {
        var serviceResponse = await _animalsService.GetAllAnimalsByShelterId(shelterId);
        return Ok(serviceResponse);
    }

    [HttpPost("Add New Animal")]
    public async Task<ActionResult<AddAnimalDto>> AddAnimal([FromBody]AddAnimalDto dto)

    {
        var serviceResponse = await _animalsService.AddAnimal(dto);
        return Ok(serviceResponse);
    }

    [HttpDelete("Delete Animal")]
    public async Task<ActionResult> DeleteAnimal([FromRoute]int animalId)
    {
        await _animalsService.DeleteAnimal(animalId);
        return NoContent();
    }
}