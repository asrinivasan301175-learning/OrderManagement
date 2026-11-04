using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Features.Orders.Commands.ConfirmOrder;
using OrderManagement.Application.Features.Orders.Commands.CreateOrder;
using OrderManagement.Application.Features.Orders.Queries.GetAllOrders;
using OrderManagement.Application.Features.Orders.Queries.GetOrderById;

namespace OrderManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // POST: api/orders
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateOrderCommand command)
    {
        var orderId = await _mediator.Send(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id = orderId },
            new { id = orderId });
    }

    // GET: api/orders/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(
            new GetOrderByIdQuery(id));

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    // GET: api/orders
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(
            new GetAllOrdersQuery());

        return Ok(result);
    }

    [HttpPut("{id:guid}/confirm")]
    public async Task<IActionResult> Confirm(Guid id)
    {
        var result = await _mediator.Send(
            new ConfirmOrderCommand(id));

        if (!result)
            return NotFound();

        return NoContent();
    }
}