using System.ComponentModel.DataAnnotations;

namespace Animal_Shelter.Entities;

public enum CostsCategory : int
{
    Maintenance = 1,
    Food = 2,
    Employees = 3
}

public enum PaymentPeriod : int
{
    Daily = 1,
    Monthly = 2,
    Yearly = 3
}
public class Costs
{
    [Key]
    public int CostId { get; set; }
    public string CostName { get; set; }
    public string? CostDescription { get; set; }
    public CostsCategory Category { get; set; }
    public int ShelterConfigId { get; set; }
    public decimal? Cost { get; set; }
    public PaymentPeriod PaymentPeriod { get; set; }
}