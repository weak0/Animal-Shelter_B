using System.ComponentModel.DataAnnotations;
using Animal_Shelter.Entities;

public class AddConfigurationDto
{
    [Required]
    public string ShelterConfigName { get; set; }
    [Required]
    public int ShelterId { get; set; }
}