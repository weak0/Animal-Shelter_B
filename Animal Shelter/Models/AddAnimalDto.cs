﻿namespace Animal_Shelter.Models;

public class AddAnimalDto
{
    public string AnimalName { get; set; }
    public int TypeId { get; set; }
    public int SizeId { get; set; }
    public int ShelterId { get; set; }
}