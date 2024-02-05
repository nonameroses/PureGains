using Application.Common.Interfaces;
using MediatR;

namespace Application.Features.User.Command;

public static class AddUser
{
    public sealed record Command : IRequest<Domain.Entities.Identity.User>
    {
        //public string? Auth0UserId { get; set; }
        //public string? Email { get; set; }
        //public string? GivenName { get; set; }
        //public string? FamilyName { get; set; }
        //public string? Nickname { get; set; }

        //public Command(string authId, string email, string givenName, string familyName, string nickName)
        //{
        //    Auth0UserId = authId;
        //    Email = email;
        //    GivenName = givenName;
        //    FamilyName = familyName;
        //    Nickname = nickName;

        //}
        public Domain.Entities.Identity.User User { get; set; }

        public Command(Domain.Entities.Identity.User user)
        {
            User = user;
        }
    }

    public sealed class Handler : IRequestHandler<Command, Domain.Entities.Identity.User>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Identity.User> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = new Domain.Entities.Identity.User
            {
                Auth0UserId = request.User.Auth0UserId,
                Email = request.User.Email,
                GivenName = request.User.GivenName,
                FamilyName = request.User.FamilyName,
                Nickname = request.User.Nickname,
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync(cancellationToken);

            return user;

        }
    }
}
