using Application.Features.Exercises.Dtos;
using Application.Features.Exercises.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[ApiController]
[Route("[controller]")]
public class ExercisesController : Controller
{
    private readonly IMediator _mediator;

    public ExercisesController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost("getInitialExercisesForUser")]
    public async Task<IEnumerable<Exercise>> GetInitialExercisesForUser(ExerciseRequestDto request)
    {
        var exercises = await _mediator.Send(new GetExercises.Query(request));

        return exercises;
    }

    [HttpPost("getExercisesForUserWorkout")]
    public async Task<IEnumerable<Exercise>> GetExercisesForUserWorkout(List<int> exerciseIds)
    {
        var exercises = await _mediator.Send(new GetExercisesForUserWorkout.Query(exerciseIds));

        return exercises;
    }

}
