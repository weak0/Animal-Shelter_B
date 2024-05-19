using System.ComponentModel.DataAnnotations;

namespace Animal_Shelter.Entities;

public class CostsCategory
{
    [Key]
    public int CostCategoryId { get; set; }
    public string Value { get; set; } 
}