using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Animal_Shelter.Entities;
public enum AnimalSize
{
    Small = 1,
    Medium = 2,
    Large = 3
}
public class Animals
{
    [Key]
    public int AnimalId { get; set; }
    [Required]
    public string AnimalName { get; set; }
    
    public Shelter Shelter { get; set; }
    [Required]
    public AnimalSize Size { get; set; }
    public DateTime DateAdded { get; private set; }
    [Required]
    public int ShelterId { get; set; }
    public Animals()
    {
        DateAdded = DateTime.Now;
    }

}