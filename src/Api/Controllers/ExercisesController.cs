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

    [HttpGet("getExercises")]
    public async Task<IEnumerable<Exercise>> GetBooks()
    {
        var exercises = await _mediator.Send(new GetExercises.Query());

        return exercises;
    }
}
