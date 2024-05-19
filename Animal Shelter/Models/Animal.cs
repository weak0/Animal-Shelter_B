using Animal_Shelter.Entities;

namespace Animal_Shelter.Models;

public class Animal
{
    public int AnimalId { get; set; }
    public string AnimalName { get; set; }
    public int TypeId { get; set; }
    public int SizeId { get; set; }
    public DateTime DateAdded { get; private set; }
    public int ShelterId { get; set; }

}