using Animal_Shelter.Data;
using Animal_Shelter.Entities;
using Animal_Shelter.Exceptions;
using Animal_Shelter.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ValidationException = Animal_Shelter.Exceptions.ValidationException;

namespace Animal_Shelter.Services;

public interface ICostService
{
    Task<AddCostDto> AddCost(AddCostDto newCost);
    Task UpdateCost(UpdateCostDto updatedCost);
    Task DeleteCost(int costId);
    Task<List<GetCostsDto>> ShelterCosts(int shelterId);
    Task<List<CostsCategory>> GetCostCategories();
    Task<List<PaymentPeriod>> GetPaymentPeriods();
}

public class CostService : ICostService
{
    private readonly AnimalShelterDbContext _context;
    private readonly IMapper  _mapper;

    public CostService(AnimalShelterDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AddCostDto> AddCost(AddCostDto dto)
    {
        IsCostModelValid(dto);
        var cost = _mapper.Map<Costs>(dto);
        await _context.Costs.AddAsync(cost);
        await _context.SaveChangesAsync();
        return _mapper.Map<AddCostDto>(cost);
    }

    public async Task UpdateCost( UpdateCostDto dto)
    {
        var cost = await _context.Costs.FindAsync(dto.CostId) ?? throw new NotFoundException("The cost ID not found.");
        _mapper.Map(dto, cost);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCost(int costId)
    {
        var cost = await _context.Costs.FindAsync(costId) ?? throw new NotFoundException("The cost ID not found.");
        _context.Costs.Remove(cost);
        await _context.SaveChangesAsync();
    }

    public async Task<List<GetCostsDto>> ShelterCosts(int shelterId)
    {
        var costs = await _context.Costs.Where(cost => cost.ShelterId == shelterId).ToListAsync();
        return _mapper.Map<List<GetCostsDto>>(costs);
    }

    public Task<List<CostsCategory>> GetCostCategories()
    {
        var result = _context.CostsCategories.ToListAsync();
        return result;
    }

    public Task<List<PaymentPeriod>> GetPaymentPeriods()
    {
        var result = _context.PaymentPeriods.ToListAsync();
        return result;
    }

    public async Task<List<GetCostsDto>> AllCosts()
    {
        var costs = await _context.Costs.ToListAsync();
        return _mapper.Map<List<GetCostsDto>>(costs);
    }

    private void IsCostModelValid(AddCostDto dto)
    {
        if (string.IsNullOrEmpty(dto.CostName))
            throw new ValidationException("Cost name is required.");
        
        if (!Enum.IsDefined(typeof(CostsCategoryEnum), dto.CategoryId))
            throw new ValidationException($"Invalid dto category: {dto.CategoryId}");

        if (dto.Cost <= 0 || dto.Cost > 1000000)
            throw new ValidationException("Cost is incorrect.");

        if (!Enum.IsDefined(typeof(PaymentPeriodEnum), dto.PaymentPeriodId))
            throw new ValidationException($"Invalid payment period: {dto.PaymentPeriodId}");
    }
    
    
}
