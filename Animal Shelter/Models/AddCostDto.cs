using System.ComponentModel.DataAnnotations;

namespace Animal_Shelter.Models;

public class AddCostDto
{
    [Required]
    public string CostName { get; set; }
    public string CostDescription { get; set; }
    [Required]
    public int CategoryId { get; set; }
    [Required]
    public int ShelterId { get; set; }
    [Required]
    public int Cost { get; set; }
    [Required]
    public int PaymentPeriodId { get; set; }
}