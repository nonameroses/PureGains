using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public class MuscleGroup
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    // Navigation property
    public ICollection<WorkoutGroupTargets> WorkoutGroupTargets { get; set; } = new List<WorkoutGroupTargets>();
}
