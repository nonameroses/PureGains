using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Workout.Commands;

public static class AddWorkout
{
    public sealed record Command : IRequest<Domain.Entities.Workout>
    {
        public int UserId;
        public List<WorkoutExercise> WorkoutExercises;

        public Command(int userId, List<WorkoutExercise> workoutExercises)
        {
            UserId = userId;
            WorkoutExercises = workoutExercises;
        }
    }

    public sealed class Handler : IRequestHandler<Command, Domain.Entities.Workout>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Workout> Handle(Command request, CancellationToken cancellationToken)
        {
            var workout = new Domain.Entities.Workout
            {
                UserId = request.UserId,
                Date = DateTime.Now,
                WorkoutExercises = request.WorkoutExercises

            };

            _context.Workouts.Add(workout);

            await _context.SaveChangesAsync(cancellationToken);

            return workout;

        }
    }
}
