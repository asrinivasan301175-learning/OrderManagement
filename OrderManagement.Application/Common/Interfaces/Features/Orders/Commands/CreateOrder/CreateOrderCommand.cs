using MediatR;

namespace OrderManagement.Application.Features.Orders.Commands.CreateOrder;

public record CreateOrderCommand(
    string CustomerName,
    List<CreateOrderItemRequest> Items
) : IRequest<Guid>;

public record CreateOrderItemRequest(
    Guid ProductId,
    string ProductName,
    int Quantity,
    decimal Price
);