using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Workout.Queries;

public class GetWorkouts
{
    public sealed class Query : IRequest<IEnumerable<Domain.Entities.Workout>>
    {
        public List<Domain.Entities.Workout> Workouts { get; set; } = null!;

        public Query()
        {

        }
    }

    public class Handler : IRequestHandler<Query, IEnumerable<Domain.Entities.Workout>>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Domain.Entities.Workout>> Handle(Query request, CancellationToken cancellationToken)
        {
            var workouts = await _context.Workouts.ToListAsync(cancellationToken);

            return workouts;
        }
    }
}
