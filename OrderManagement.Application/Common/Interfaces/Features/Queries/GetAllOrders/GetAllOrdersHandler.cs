using MediatR;
using OrderManagement.Application.Common.Interfaces;
using OrderManagement.Application.DTOs;

namespace OrderManagement.Application.Features.Orders.Queries.GetAllOrders;

public class GetAllOrdersHandler
    : IRequestHandler<GetAllOrdersQuery, List<OrderDto>>
{
    private readonly IOrderRepository _repository;

    public GetAllOrdersHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<OrderDto>> Handle(
        GetAllOrdersQuery request,
        CancellationToken cancellationToken)
    {
        var orders = await _repository.GetAllAsync();

        return orders.Select(order => new OrderDto
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
        }).ToList();
    }
}