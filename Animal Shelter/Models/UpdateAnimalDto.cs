namespace Animal_Shelter.Models;

public class UpdateAnimalDto
{
    
    public int AnimalId { get; set; }
    public string AnimalName { get; set; }
    public int TypeId { get; set; }
    public int SizeId { get; set; }
}