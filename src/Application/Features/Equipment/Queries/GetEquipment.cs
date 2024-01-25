using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Equipment.Queries;

//Query to get all the Equipment 
public class GetEquipment
{
    public sealed class Query : IRequest<IEnumerable<Domain.Entities.Equipment>>
    {
        public List<Domain.Entities.Equipment> Equipments { get; set; } = null!;

        public Query()
        {

        }
    }

    // Takes in Query and returns a List of Books
    public class Handler : IRequestHandler<Query, IEnumerable<Domain.Entities.Equipment>>
    {
        // Declare IApplicationDbContext class to use the methods
        private readonly IApplicationDbContext _context;

        // Injecting dependency into constructor
        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Domain.Entities.Equipment>> Handle(Query request, CancellationToken cancellationToken)
        {
            // Calling context to return all the equipment
            var equipment = await _context.Equipment.ToListAsync(cancellationToken);

            return equipment;
        }
    }
}