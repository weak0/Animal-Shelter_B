using Animal_Shelter.Data;
using Animal_Shelter.Entities;
using Animal_Shelter.Exceptions;
using Animal_Shelter.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Animal_Shelter.Services;

public interface IConfigurationService
{
    Task<AddConfigurationDto> AddConfiguration(AddConfigurationDto newConfiguration);
    Task DeleteConfiguration(int configurationId);
    Task<string> GetConfigurationName(int configurationId);
}
public class ConfigurationService : IConfigurationService
{
    private readonly AnimalShelterDbContext _context;
    private readonly IMapper _mapper;
    private readonly IShelterService _shelterService;

    public ConfigurationService(AnimalShelterDbContext context, IMapper mapper, IShelterService shelterService)
    {
        _context = context;
        _mapper = mapper;
        _shelterService = shelterService;
    }

    public async Task<AddConfigurationDto> AddConfiguration(AddConfigurationDto dto)
    {
        var configuration = _mapper.Map<AnimalShelterConfiguration>(dto);
        var shelterName = await _shelterService.GetShelterName(dto.ShelterId);
        
        if (configuration.ShelterId != dto.ShelterId)
            throw new Exception($"Is the chosen shelter correct? {shelterName} is not the same as {dto.ShelterId}.");
        
        await _context.AnimalShelterConfiguration.AddAsync(configuration);
        await _context.SaveChangesAsync();
        return _mapper.Map<AddConfigurationDto>(configuration);
    }

    public async Task DeleteConfiguration(int configurationId)
    {
        var configuration = await _context.AnimalShelterConfiguration.FindAsync(configurationId) ?? throw new NotFoundException("The configuration ID not found.");
        _context.AnimalShelterConfiguration.Remove(configuration);
        await _context.SaveChangesAsync();
    }
    
    public async Task<string> GetConfigurationName(int configurationId)
    {
        var configuration = await _context.AnimalShelterConfiguration.FindAsync(configurationId) ?? throw new NotFoundException("The configuration ID not found.");
        return configuration.ShelterConfigName;
    }
}