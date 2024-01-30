using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.MuscleGroups.Queries;

//Query to get all the Equipment 
public class GetMuscleGroups
{
    public sealed class Query : IRequest<IEnumerable<Domain.Entities.MuscleGroup>>
    {
        public List<Domain.Entities.MuscleGroup> WorkoutGroups { get; set; } = null!;

        public Query()
        {

        }
    }

    public class Handler : IRequestHandler<Query, IEnumerable<Domain.Entities.MuscleGroup>>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Domain.Entities.MuscleGroup>> Handle(Query request, CancellationToken cancellationToken)
        {
            var workoutGroups = await _context.MuscleGroups.ToListAsync(cancellationToken);

            return workoutGroups;
        }
    }
}