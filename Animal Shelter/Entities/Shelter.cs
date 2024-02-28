using System.ComponentModel.DataAnnotations;

namespace Animal_Shelter.Entities;

public class Shelter
{
    public int ShelterId { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string HashedPassword { get; set; }
}