namespace Animal_Shelter.Entities;

public enum CostsCategory
{
    Maintenance,
    Food,
    Employees
}

public enum PaymentPeriod
{
    Daily,
    Monthly,
    Yearly
}
public class Costs
{
    public int CostId { get; set; }
    public string CostName { get; set; }
    public string? CostDescription { get; set; }
    public CostsCategory Category { get; set; }
    public int ShelterConfigId { get; set; }
    public decimal? Cost { get; set; }
    public PaymentPeriod PaymentPeriod { get; set; }
}