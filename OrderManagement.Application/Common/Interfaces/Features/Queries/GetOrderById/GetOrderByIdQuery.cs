using MediatR;
using OrderManagement.Application.DTOs;

namespace OrderManagement.Application.Features.Orders.Queries.GetOrderById;

public record GetOrderByIdQuery(Guid Id)
    : IRequest<OrderDto?>;