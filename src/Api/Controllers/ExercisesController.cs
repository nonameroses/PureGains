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
        _mediator = mediator;
    }

    [HttpPost("getExercisesForUser")]
    public async Task<IEnumerable<Exercise>> GetExercisesForUser(ExerciseRequestDto request)
    {
        var exercises = await _mediator.Send(new GetExercises.Query(request));

        return exercises;
    }
}
