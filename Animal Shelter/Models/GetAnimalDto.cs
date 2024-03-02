using Animal_Shelter.Entities;

namespace Animal_Shelter.Models;

public class GetAnimalDto
{
    public int AnimalId { get; set; }
    public string AnimalName { get; set; }
    public AnimalType Type { get; set; }
    public AnimalSize Size { get; set; }
    public DateTime DateAdded { get; private set; }
    public int ShelterId { get; set; }

}