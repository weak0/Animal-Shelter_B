using System.ComponentModel.DataAnnotations;

namespace Animal_Shelter.Entities;

public class AnimalShelter
{
    [Key]
    public int AnimalShelterId { get; set; }
    [Required]
    public string AnimalShelterName { get; set; }
    
}