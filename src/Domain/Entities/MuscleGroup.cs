﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public class MuscleGroup
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? ImagePath { get; set; }
}
