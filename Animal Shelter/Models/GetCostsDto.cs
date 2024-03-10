using Animal_Shelter.Entities;

namespace Animal_Shelter.Models;

public class GetCostsDto
{
    public int CostId { get; set; }
    public string CostName { get; set; }
    public string? CostDescription { get; set; }
    public CostsCategory Category { get; set; }
    public int ShelterConfigId { get; set; }
    public decimal? Cost { get; set; }
    public PaymentPeriod PaymentPeriod { get; set; }
}