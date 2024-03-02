using Animal_Shelter.Entities;

namespace Animal_Shelter.Models;

public class AddAnimalDto
{
    public string AnimalName { get; set; }
    public AnimalType Type { get; set; }
    public AnimalSize Size { get; set; }
    public int ShelterId { get; set; }
}