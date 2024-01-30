using Application.Features.MuscleGroups.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[ApiController]
[Route("[controller]")]
public class MuscleGroupsController : Controller
{
    private readonly IMediator _mediator;

    public MuscleGroupsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("getMuscleGroups")]
    public async Task<IEnumerable<MuscleGroup>> GetBooks()
    {
        var muscleGroups = await _mediator.Send(new GetMuscleGroups.Query());

        return muscleGroups;
    }
}

