namespace Animal_Shelter.Models;

public class UpdateCostDto
{
    public int CostId { get; set; }
    public string CostName { get; set; }
    public string? CostDescription { get; set; }
    public int CategoryId { get; set; }
    public int ShelterConfigId { get; set; }
    public decimal? Cost { get; set; }
    public string PaymentPeriod { get; set; }
}