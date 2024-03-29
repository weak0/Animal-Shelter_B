﻿using System.ComponentModel.DataAnnotations;
using Animal_Shelter.Entities;

namespace Animal_Shelter.Models;

public class AddCostDto
{
    [Required]
    public string CostName { get; set; }
    public string? CostDescription { get; set; }
    [Required]
    public CostsCategory Category{ get; set; }
    [Required]
    public int ShelterConfigId { get; set; }
    [Required]
    public decimal? Cost { get; set; }
    [Required]
    public PaymentPeriod PaymentPeriod { get; set; }
}