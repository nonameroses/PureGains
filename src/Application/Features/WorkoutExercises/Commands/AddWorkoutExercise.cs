using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.WorkoutExercises.Commands;

public static class AddWorkoutExercise
{
    public sealed record Command : IRequest<WorkoutExercise>
    {
        public int WorkoutId { get; set; } // Foreign Key to Workout
        public int ExerciseId { get; set; } // Foreign Key to Exercise
        //public int Sets { get; set; } = 4; // Default value
        //public int Reps { get; set; } = 8; // Default value

        public Command(int workoutId, int exerciseId)
        {
            WorkoutId = workoutId;
            ExerciseId = exerciseId;
            //Sets = sets;
            //Reps = reps;
        }
    }

    public sealed class Handler : IRequestHandler<Command, WorkoutExercise>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<WorkoutExercise> Handle(Command request, CancellationToken cancellationToken)
        {
            var workoutExercise = new WorkoutExercise
            {
                ExerciseId = request.ExerciseId,
                WorkoutId = request.WorkoutId,
            };

            _context.WorkoutExercises.Add(workoutExercise);

            await _context.SaveChangesAsync(cancellationToken);

            return workoutExercise;

        }
    }
}
