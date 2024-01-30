using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Exercises.Queries;

//Query to get all the Equipment 
public class GetExercises
{
    public sealed class Query : IRequest<IEnumerable<Exercise>>
    {
        public List<Exercise> Exercises { get; set; } = null!;

        public Query()
        {

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
            var exercises = await _context.Exercises.ToListAsync(cancellationToken);

            return exercises;
        }
    }
}