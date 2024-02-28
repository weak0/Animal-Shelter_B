using Animal_Shelter.Data;
using Animal_Shelter.Entities;
using Animal_Shelter.Models;
using Microsoft.AspNetCore.Mvc;

namespace Animal_Shelter.Services;

public interface ICostService
{
    Task<AddCostDto> AddCost(AddCostDto newCost);
    Task UpdateCost(int id, AddCostDto updatedCost);
    Task DeleteCost(int id);
}

public class CostService : ICostService
{
    private readonly AnimalShelterDbContext _context;

    public CostService(AnimalShelterDbContext context)
    {
        _context = context;
    }

    public Task<AddCostDto> AddCost(AddCostDto newCost)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCost(int id, AddCostDto updatedCost)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCost(int id)
    {
        throw new NotImplementedException();
    }
}