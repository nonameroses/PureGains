using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.WorkoutExercises.Commands;

public static class AddWorkoutExercise
{
    public sealed record Command : IRequest<IEnumerable<int>>
    {
        public int WorkoutId { get; set; } // Foreign Key to Workout
        public List<int> ExerciseIds { get; set; } // Foreign Key to Exercise
        //public int Sets { get; set; } = 4; // Default value
        //public int Reps { get; set; } = 8; // Default value

        public Command(int workoutId, List<int> _exerciseIds)
        {
            WorkoutId = workoutId;
            ExerciseIds = _exerciseIds;
            //Sets = sets;
            //Reps = reps;
        }
    }

    public sealed class Handler : IRequestHandler<Command, IEnumerable<int>>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<int>> Handle(Command request, CancellationToken cancellationToken)
        {
            var exerciseIds = new List<int>();

            foreach (var exerciseId in request.ExerciseIds)
            {
                var workoutExercise = new WorkoutExercise
                {
                    ExerciseId = exerciseId,
                    WorkoutId = request.WorkoutId,
                };

                exerciseIds.Add(exerciseId);
                _context.WorkoutExercises.Add(workoutExercise);
            }


            await _context.SaveChangesAsync(cancellationToken);

            return exerciseIds;

        }
    }
}
