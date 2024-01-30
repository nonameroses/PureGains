namespace Domain.Entities;

public class WorkoutExercise
{
    public int Id { get; set; }
    public int WorkoutId { get; set; } // Foreign Key to Workout
    public int ExerciseId { get; set; } // Foreign Key to Exercise
    public int Sets { get; set; } = 4; // Default value
    public int Reps { get; set; } = 8; // Default value
    public int Sequence { get; set; } // Order of exercises in the workout

    // Navigation properties to Workout and Exercise
    public virtual Workout Workout { get; set; } = null!;
    public virtual Exercise Exercise { get; set; } = null!;

    // Other properties and methods
}