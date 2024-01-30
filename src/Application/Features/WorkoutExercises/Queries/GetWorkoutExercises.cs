using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.WorkoutExercises.Queries;

public class GetWorkoutExercises
{
    public sealed class Query : IRequest<IEnumerable<WorkoutExercise>>
    {
        public List<WorkoutExercise> WorkoutExercises { get; set; } = null!;

        public Query()
        {

        }
    }

    public class Handler : IRequestHandler<Query, IEnumerable<WorkoutExercise>>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WorkoutExercise>> Handle(Query request, CancellationToken cancellationToken)
        {
            var workoutExercises = await _context.WorkoutExercises.ToListAsync(cancellationToken);

            return workoutExercises;
        }
    }
}
