using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public class MuscleGroup
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }

    [Required]
    public ICollection<WorkoutGroup> WorkoutGroups { get; set; } = new List<WorkoutGroup>();
}
