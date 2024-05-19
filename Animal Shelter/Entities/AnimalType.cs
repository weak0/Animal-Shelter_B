using System.ComponentModel.DataAnnotations;

namespace Animal_Shelter.Entities;

public class AnimalType
{
    [Key]
    public int AnimalTypeId { get; set; }
    public string Value { get; set; }
}