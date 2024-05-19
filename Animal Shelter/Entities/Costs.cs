using System.ComponentModel.DataAnnotations;

namespace Animal_Shelter.Entities;

public class Costs
{
    [Key]
    public int CostId { get; set; }
    public Shelter Shelter { get; set; }
    public int ShelterId { get; set; }
    public string CostName { get; set; }
    public string? CostDescription { get; set; }
    public CostsCategory Category { get; set; }
    public int CategoryId { get; set; }
    public int Cost { get; set; }
    public PaymentPeriod PaymentPeriod  { get; set; }
    public int PaymentPeriodId  { get; set; }
}