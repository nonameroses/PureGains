using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.User.Queries;

public class GetUserExists
{
    public sealed class Query : IRequest<bool>
    {
        public string? Auth0UserId { get; set; }

        public Query(string id)
        {
            Auth0UserId = id;
        }
    }

    public class Handler : IRequestHandler<Query, bool>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _context.Users.FirstOrDefaultAsync(x => x.Auth0UserId == request.Auth0UserId, cancellationToken);


            return result != null;
        }
    }
}
