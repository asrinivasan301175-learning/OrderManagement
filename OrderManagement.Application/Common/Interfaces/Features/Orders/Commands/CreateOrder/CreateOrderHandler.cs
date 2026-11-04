using MediatR;
using OrderManagement.Application.Common.Interfaces;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderHandler
    : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _repository;

    public CreateOrderHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(
        CreateOrderCommand request,
        CancellationToken cancellationToken)
    {

        if (request.Items == null || !request.Items.Any())
        {
            throw new ArgumentException(
                "An order must contain at least one item.");
        }
        var order = Order.Create(request.CustomerName);

        foreach (var item in request.Items)
        {
            order.AddItem(
                item.ProductId,
                item.ProductName,
                item.Quantity,
                item.Price);
        }

        await _repository.AddAsync(order);

        await _repository.SaveChangesAsync();

        return order.Id;
    }
}