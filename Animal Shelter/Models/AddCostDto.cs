﻿using System.ComponentModel.DataAnnotations;

namespace Animal_Shelter.Models;

public class AddCostDto
{
    [Required]
    public string CostName { get; set; }
    public string? CostDescription { get; set; }
    [Required]
    public int Category{ get; set; }
    [Required]
    public int ShelterConfigId { get; set; }
    [Required]
    public decimal? Cost { get; set; }
    [Required]
    public string PaymentPeriod { get; set; }
}