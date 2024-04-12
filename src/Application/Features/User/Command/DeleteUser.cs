using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.User.Command;

public static class DeleteUser
{
    public sealed record Command : IRequest<Unit>
    {
        public string Auth0UserId { get; }

        public Command(string _auth0UserId)
        {
            Auth0UserId = _auth0UserId;
        }
    }

    public sealed class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Auth0UserId == request.Auth0UserId, cancellationToken);
            //if (entity == null)
            //{
            //    throw new NotFoundException(nameof(User), request.Id);
            //}

            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
