using System.ComponentModel.DataAnnotations;

namespace Animal_Shelter.Entities;

public class PaymentPeriod
{
    [Key]
    public int PaymentPeriodId { get; set; }
    public string Value { get; set;}
}