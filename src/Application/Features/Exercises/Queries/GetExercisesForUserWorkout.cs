using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Exercises.Queries;
public class GetExercisesForUserWorkout
{
    public sealed class Query : IRequest<IEnumerable<Exercise>>
    {
        public List<int> ExerciseIds { get; set; }

        public Query(List<int> exerciseIds)
        {
            ExerciseIds = exerciseIds;
        }
    }

    public class Handler : IRequestHandler<Query, IEnumerable<Exercise>>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Exercise>> Handle(Query request, CancellationToken cancellationToken)
        {
            var exercises = await _context.Exercises.Where(e => request.ExerciseIds.Contains(e.Id))
                .ToListAsync(cancellationToken);

            return exercises;
        }
    }
}
