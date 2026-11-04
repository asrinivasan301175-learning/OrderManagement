using MediatR;
using OrderManagement.Application.DTOs;

namespace OrderManagement.Application.Features.Orders.Queries.GetAllOrders;

public record GetAllOrdersQuery : IRequest<List<OrderDto>>;