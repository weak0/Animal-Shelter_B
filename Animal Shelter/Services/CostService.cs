using Animal_Shelter.Data;
using Animal_Shelter.Entities;
using Animal_Shelter.Exceptions;
using Animal_Shelter.Models;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ValidationException = Animal_Shelter.Exceptions.ValidationException;

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
        IsCostModelValid(dto);
        var cost = _mapper.Map<Costs>(dto);
        await _configurationService.GetConfigurationName(cost.ShelterConfigId);
        await _context.Costs.AddAsync(cost);
        await _context.SaveChangesAsync();
        return _mapper.Map<AddCostDto>(cost);
    }

    public async Task UpdateCost(int costId, UpdateCostDto dto)
    {
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

    private void IsCostModelValid(AddCostDto dto)
    {
        if (dto.CostName == String.Empty || dto.CostName == null)
            throw new ValidationException("Cost name is required.");

        if (!Enum.IsDefined(typeof(CostsCategory), dto.Category))
            throw new ValidationException($"Invalid dto category: {dto.Category}");

        if (dto.Cost <= 0 || dto.Cost > 1000000)
            throw new ValidationException("Cost is incorrect.");

        if (!Enum.IsDefined(typeof(PaymentPeriod), dto.PaymentPeriod))
            throw new ValidationException($"Invalid payment period: {dto.PaymentPeriod}");
    }
}
