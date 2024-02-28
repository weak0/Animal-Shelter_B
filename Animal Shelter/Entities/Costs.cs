namespace Animal_Shelter.Entities;

public class Costs
{
    public int CostId { get; set; }
    public string CostName { get; set; }
    public string? CostDescription { get; set; }
    public int Category { get; set; }
    public int ShelterConfigId { get; set; }
    public decimal? Cost { get; set; }
    public string PaymentPeriod { get; set; }
}