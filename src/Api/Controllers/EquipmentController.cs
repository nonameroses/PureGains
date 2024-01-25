using Application.Features.Equipment.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[ApiController]
[Route("[controller]")]
public class EquipmentController : Controller
{
    private readonly IMediator _mediator;

    // Injecting dependency in the constructor
    public EquipmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("getEquipment")]
    public async Task<IEnumerable<Equipment>> GetBooks()
    {
        var equipment = await _mediator.Send(new GetEquipment.Query());

        return equipment;
    }
}
