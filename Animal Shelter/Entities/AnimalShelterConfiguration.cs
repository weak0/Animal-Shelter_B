using System.ComponentModel.DataAnnotations;

namespace Animal_Shelter.Entities;

public class AnimalShelterConfiguration
{
    [Key]
    public int ShelterConfigId { get; set; }
    public string ShelterConfigName { get; set; }
    public int ShelterId { get; set; }
}