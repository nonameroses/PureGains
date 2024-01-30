using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.WorkoutGroups.Queries;

public class GetWorkoutGroups
{
    public sealed class Query : IRequest<IEnumerable<Domain.Entities.WorkoutGroup>>
    {
        public List<Domain.Entities.WorkoutGroup> WorkoutGroups { get; set; } = null!;

        public Query()
        {

        }
    }

    public class Handler : IRequestHandler<Query, IEnumerable<Domain.Entities.WorkoutGroup>>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Domain.Entities.WorkoutGroup>> Handle(Query request, CancellationToken cancellationToken)
        {
            var workoutGroups = await _context.WorkoutGroups.ToListAsync(cancellationToken);

            return workoutGroups;
        }
    }
}