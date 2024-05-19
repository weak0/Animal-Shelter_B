using System.ComponentModel.DataAnnotations;

namespace Animal_Shelter.Entities;

public class AnimalSize
{
    [Key]
    public int AnimalSizeId { get; set; }
    public string Value { get; set; }
}