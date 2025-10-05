using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Commands;

namespace SagaDesignPattern_Choreography.Controllers;

[Route("api/orders")]
[ApiController]
public class OrderApiController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderApiController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok(command);
    }
}
