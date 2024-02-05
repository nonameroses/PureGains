using Application.Features.User.Command;
using Application.Features.User.Queries;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Identity;
[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    private readonly IMediator _mediator;

    // Injecting dependency in the constructor
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    public Task<User> GetUserById(string id)
    {
        var user = _mediator.Send(new GetUserById.Query(id));

        return user;
    }
    [HttpPut]
    [Authorize]
    public Task<User> CreateUser(User user)
    {
        var result = _mediator.Send(new AddUser.Command(user));

        return result;
    }
}
