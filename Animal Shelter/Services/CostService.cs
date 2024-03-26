using Animal_Shelter.Data;
using Animal_Shelter.Entities;
using Animal_Shelter.Exceptions;
using Animal_Shelter.Models;
using AutoMapper;
using FluentValidation;
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
        
        if (cost.CostName == String.Empty || cost.CostName == null)
            throw new ValidationException("Cost name is required.");
        
        if (!Enum.IsDefined(typeof(CostsCategory), cost.Category))
            throw new ValidationException($"Invalid cost category: {cost.Category}");

        if (cost.Cost <= 0 || cost.Cost > 1000000)
            throw new ValidationException("Cost is incorrect.");
            
        if (!Enum.IsDefined(typeof(PaymentPeriod), cost.PaymentPeriod))
            throw new ValidationException($"Invalid payment period: {cost.PaymentPeriod}");
        
        await _context.Costs.AddAsync(cost);
        await _context.SaveChangesAsync();
        return _mapper.Map<AddCostDto>(cost);
    }

    public async Task UpdateCost(int costId, UpdateCostDto dto)
    {
        // missing mapping, you didn't use dto
        var cost = await _context.Costs.FindAsync(costId) ?? throw new NotFoundException("The cost ID not found.");
        _mapper.Map(dto, cost);
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