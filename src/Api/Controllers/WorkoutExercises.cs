using Application.Features.WorkoutExercises.Commands;
using Application.Features.WorkoutExercises.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[ApiController]
[Route("[controller]")]
public class WorkoutExercisesController : Controller
{
    private readonly IMediator _mediator;

    public WorkoutExercisesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("getWorkoutExercises")]
    public async Task<IEnumerable<WorkoutExercise>> GetWorkoutExercises()
    {
        var workouts = await _mediator.Send(new GetWorkoutExercises.Query());

        return workouts;
    }

    [HttpPost("addWorkoutExercise")]
    public async Task<WorkoutExercise> AddWorkoutExercise(int workoutId, int exerciseId)
    {
        var workouts = await _mediator.Send(new AddWorkoutExercise.Command(workoutId, exerciseId));

        return workouts;
    }
}