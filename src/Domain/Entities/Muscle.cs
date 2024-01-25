using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Muscle
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public string? Description { get; set; }
    [Required]
    public MuscleGroup MuscleGroup { get; set; } = null!;
}



//[Required]
//public string? ImagePath { get; set; }