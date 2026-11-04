using MediatR;
using OrderManagement.Application.Common.Interfaces;
using OrderManagement.Application.DTOs;

namespace OrderManagement.Application.Features.Orders.Queries.GetOrderById;

public class GetOrderByIdHandler
    : IRequestHandler<GetOrderByIdQuery, OrderDto?>
{
    private readonly IOrderRepository _repository;

    public GetOrderByIdHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<OrderDto?> Handle(
        GetOrderByIdQuery request,
        CancellationToken cancellationToken)
    {
        var order = await _repository.GetByIdAsync(request.Id);

        if (order == null)
            return null;

        return new OrderDto
        {
            Id = order.Id,
            CustomerName = order.CustomerName,
            Status = order.Status.ToString(),
            TotalAmount = order.TotalAmount,

            Items = order.Items.Select(item => new OrderItemDto
            {
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice.Amount,
                Total = item.GetTotal()
            }).ToList()
        };
    }
}