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
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator)); ;
    }

    [HttpGet("getWorkoutExercises")]
    public async Task<IEnumerable<WorkoutExercise>> GetWorkoutExercises()
    {
        var workouts = await _mediator.Send(new GetWorkoutExercises.Query());

        return workouts;
    }

    [HttpPut("addWorkoutExercise")]
    public async Task<IEnumerable<int>> AddWorkoutExercise(int workoutId, List<int> exerciseIds)
    {
        var exercises = await _mediator.Send(new AddWorkoutExercise.Command(workoutId, exerciseIds));

        return exercises;
    }
}