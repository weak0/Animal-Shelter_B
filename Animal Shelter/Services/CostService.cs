using Animal_Shelter.Data;
using Animal_Shelter.Entities;
using Animal_Shelter.Exceptions;
using Animal_Shelter.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Animal_Shelter.Services;

public interface ICostService
{
    Task<AddCostDto> AddCost(AddCostDto newCost);
    Task UpdateCost(int costId, UpdateCostDto updatedCost);
    Task DeleteCost(int costId);
    Task<List<GetCostsDto>> GetAllCosts();
}

public class CostService : ICostService
{
    private readonly AnimalShelterDbContext _context;
    private readonly IMapper _mapper;
    private readonly IConfigurationService _configurationService;
    
    public CostService(AnimalShelterDbContext context, IMapper mapper, IConfigurationService configurationService)
    {
        _context = context;
        _mapper = mapper;
        _configurationService = configurationService;
    }

    public async Task<AddCostDto> AddCost(AddCostDto dto)
    {
        var cost = _mapper.Map<Costs>(dto);
        var configuration = await _configurationService.GetConfigurationName(cost.ShelterConfigId);

        if (cost.ShelterConfigId != dto.ShelterConfigId)
            throw new Exception($"Is the chosen configuration correct? {configuration} is not the same as {dto.ShelterConfigId}.");
        
        await _context.Costs.AddAsync(cost);
        await _context.SaveChangesAsync();
        return _mapper.Map<AddCostDto>(cost);
    }

    public async Task UpdateCost(int costId, UpdateCostDto dto)
    {
        // missing mapping, you didn't use dto
        var cost = await _context.Costs.FindAsync(costId) ?? throw new NotFoundException("The cost ID not found.");
        _context.Costs.Update(cost); // ?
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCost(int costId)
    {
        var cost = await _context.Costs.FindAsync(costId) ?? throw new NotFoundException("The cost ID not found.");
        _context.Costs.Remove(cost);
        await _context.SaveChangesAsync();
    }
    
    public async Task<List<GetCostsDto>> GetAllCosts()
    {
        var costs = await _context.Costs.ToListAsync();
        return _mapper.Map<List<GetCostsDto>>(costs);
    }
}