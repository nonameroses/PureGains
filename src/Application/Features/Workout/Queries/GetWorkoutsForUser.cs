using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Workout.Queries;

public class GetWorkoutsForUser
{
    public sealed class Query : IRequest<IEnumerable<Domain.Entities.Workout>>
    {
        public int UserId { get; set; }

        public Query(int userId)
        {
            UserId = userId;
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
            var workouts = await _context.Workouts.Where(w => w.UserId == request.UserId).ToListAsync(cancellationToken);

            return workouts;
        }
    }
}
