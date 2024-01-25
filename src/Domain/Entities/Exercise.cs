using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Exercise
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Priority { get; set; }
    [Required]
    public Equipment Equipment { get; set; } = null!;
    [Required]
    public Muscle PrimaryMuscle { get; set; } = null!;
    //public Muscle SecondaryMuscle { get; set; } = null!;
    //[Required]
    //public string? ImagePath { get; set; }
}
