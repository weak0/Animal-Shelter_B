using Animal_Shelter.Entities;
using Animal_Shelter.Models;
using Animal_Shelter.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Animal_Shelter.Controllers;

[ApiController]
[Route("costs")]
[Authorize]
public class CostsController : ControllerBase
{
    private readonly ICostService _costService;
    
    public CostsController(ICostService costService)
    {
        _costService = costService;                  
    }
    
    [HttpPost]
    public async Task<ActionResult<AddCostDto>> AddCost([FromBody] AddCostDto dto)
    {
        var serviceResponse = await _costService.AddCost(dto);
        return Ok(serviceResponse);
    }
    
    [HttpPut]
    public async Task<ActionResult> UpdateCost([FromBody] UpdateCostDto dto)
    {
        await _costService.UpdateCost(dto);
        return NoContent();
    }
    
    [HttpDelete("{costId}")]
    public async Task<ActionResult> DeleteCost([FromRoute]int costId)
    {
        await _costService.DeleteCost(costId);
        return NoContent();
    }

    [HttpGet("shelter/{shelterId}")]
    public async Task<ActionResult> GetShelterCosts([FromRoute] int shelterId)
    {
        var result  = await _costService.ShelterCosts(shelterId);
        return this.Ok(result);
    }
    
    [HttpGet("categories")]
    public async Task<ActionResult<List<CostsCategory>>> GetCostCategories()
    {
        var result = await _costService.GetCostCategories();
        return this.Ok(result);
    }
    
    [HttpGet("paymentPeriods")]
    public async Task<ActionResult<List<PaymentPeriod>>> GetPaymentPeriods()
    {
        var result = await _costService.GetPaymentPeriods();
        return this.Ok(result);
    }
}