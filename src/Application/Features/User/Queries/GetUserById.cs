using Application.Common.Interfaces;
using MediatR;

namespace Application.Features.User.Queries;

public class GetUserById
{
    public sealed class Query : IRequest<Domain.Entities.Identity.User>
    {
        public string? Auth0UserId { get; set; }

        public Query(string id)
        {
            Auth0UserId = id;
        }
    }

    public class Handler : IRequestHandler<Query, Domain.Entities.Identity.User>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Identity.User> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request, cancellationToken);
            if (user != null)
            {

            }

            return user;
        }
    }
}
