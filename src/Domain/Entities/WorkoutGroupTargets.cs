namespace Domain.Entities;
public class WorkoutGroupTargets
{
    public int WorkoutGroupId { get; set; }
    public WorkoutGroup WorkoutGroup { get; set; }

    public int MuscleGroupId { get; set; }
    public MuscleGroup MuscleGroup { get; set; }
}
