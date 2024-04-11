using Application.Features.Workout.Commands;
using Application.Features.Workout.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[ApiController]
[Route("[controller]")]
public class WorkoutController : Controller
{
    private readonly IMediator _mediator;

    public WorkoutController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator)); ;
    }

    [HttpGet("getWorkouts")]
    public async Task<IEnumerable<Workout>> GetWorkouts(int userId)
    {
        var workouts = await _mediator.Send(new GetWorkoutsForUser.Query(userId));

        return workouts;
    }
    [HttpPut("addWorkout")]
    public async Task<Workout> AddWorkout(int id)
    {
        var workouts = await _mediator.Send(new AddWorkout.Command(id));

        return workouts;
    }
}