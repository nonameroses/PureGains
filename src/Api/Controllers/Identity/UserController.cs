using Application.Features.User.Command;
using Application.Features.User.Queries;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Identity;
[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public Task<User> GetUserById(string id)
    {
        var user = _mediator.Send(new GetUserById.Query(id));

        return user;
    }

    [HttpPost]
    public Task<bool> GetUserExists(string id)
    {
        var result = _mediator.Send(new GetUserExists.Query(id));


        return result;
    }
    [HttpPut]
    public Task<User> CreateUser(User user)
    {
        var result = _mediator.Send(new AddUser.Command(user));

        return result;
    }

    [HttpPost("deleteUser")]
    public Task<Unit> DeleteUser(string id)
    {
        var result = _mediator.Send(new DeleteUser.Command(id));

        return result;
    }
}
