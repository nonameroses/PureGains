using Application.Features.WorkoutGroups.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[ApiController]
[Route("[controller]")]
public class WorkoutGroupsController : Controller
{
    private readonly IMediator _mediator;

    public WorkoutGroupsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("getWorkoutGroups")]
    public async Task<IEnumerable<WorkoutGroup>> GetBooks()
    {
        var workoutGroups = await _mediator.Send(new GetWorkoutGroups.Query());

        return workoutGroups;
    }
}
