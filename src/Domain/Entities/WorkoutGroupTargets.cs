namespace Domain.Entities;

// Class to create a junction between WorkoutGroup and MuscleGroup tables
public class WorkoutGroupTargets
{
    public int Id { get; set; }
    public int WorkoutGroupId { get; set; }
    public WorkoutGroup WorkoutGroup { get; set; } = null!;

    public int MuscleGroupId { get; set; }
    public MuscleGroup MuscleGroup { get; set; } = null!;
}
