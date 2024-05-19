using Animal_Shelter.Entities;

namespace Animal_Shelter.Models;

public class GetCostsDto
{
    public int CostId { get; set; }
    public string CostName { get; set; }
    public string? CostDescription { get; set; }
    public int CategoryId { get; set; }
    public int ShelterId { get; set; }
    public int Cost { get; set; }
    public int PaymentPeriodId { get; set; }
}