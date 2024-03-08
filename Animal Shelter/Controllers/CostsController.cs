using Animal_Shelter.Models;
using Animal_Shelter.Services;
using Microsoft.AspNetCore.Mvc;

namespace Animal_Shelter.Controllers;

[ApiController]
[Route("[controller]")]

public class CostsController : ControllerBase
{
    private readonly ICostService _costService;
    
    public CostsController(ICostService costService)
    {
        _costService = costService;
    }
    
    [HttpPost("/configuration/costs/add")]
    public async Task<ActionResult<AddCostDto>> AddCost([FromBody] AddCostDto dto)
    {
        var serviceResponse = await _costService.AddCost(dto);
        return Ok(serviceResponse);
    }
    
    [HttpPut("/configuration/costs/update/{costId}")]
    public async Task<ActionResult> UpdateCost([FromRoute] int costId, [FromBody] UpdateCostDto dto)
    {
        await _costService.UpdateCost(costId, dto);
        return NoContent();
    }
    
    [HttpDelete("/configuration/costs/delete/{costId}")]
    public async Task<ActionResult> DeleteCost([FromRoute]int costId)
    {
        await _costService.DeleteCost(costId);
        return NoContent();
    }
}