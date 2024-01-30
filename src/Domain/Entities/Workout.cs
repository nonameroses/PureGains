namespace Domain.Entities;
public class Workout
{
    public int Id { get; set; }
    public int UserId { get; set; } // Foreign Key to User, if applicable
                                    // public DateTime Date { get; set; }
                                    // public TimeSpan? TotalDuration { get; set; }

    // Navigation property to WorkoutExercise
    public virtual List<WorkoutExercise> WorkoutExercises { get; set; } = new List<WorkoutExercise>();
}