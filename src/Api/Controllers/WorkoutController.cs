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
        _mediator = mediator;
    }

    [HttpGet("getWorkouts")]
    public async Task<IEnumerable<Workout>> GetWorkouts()
    {
        var workouts = await _mediator.Send(new GetWorkouts.Query());

        return workouts;
    }
    [HttpPost("addWorkouts")]
    public async Task<Workout> AddWorkotus(int userId, List<WorkoutExercise> workoutExercises)
    {
        var workouts = await _mediator.Send(new AddWorkout.Command(userId, workoutExercises));

        return workouts;
    }
}