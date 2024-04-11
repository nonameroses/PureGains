using Application.Features.Equipment.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[ApiController]
[Route("[controller]")]
public class EquipmentController(IMediator mediator) : Controller
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet("getEquipment")]
    public async Task<IEnumerable<Equipment>> GetEquipment()
    {
        var equipment = await _mediator.Send(new GetEquipment.Query());

        return equipment;
    }
}
