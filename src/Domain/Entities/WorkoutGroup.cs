using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public class WorkoutGroup
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }

    [Required]
    public ICollection<MuscleGroup> MuscleGroups { get; set; } = new List<MuscleGroup>();

}

// public ICollection<MuscleGroup?> MuscleGroup { get; set; } = new List<MuscleGroup?>();