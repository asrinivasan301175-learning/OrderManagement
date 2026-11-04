using MediatR;
using OrderManagement.Application.Common.Interfaces;

namespace OrderManagement.Application.Features.Orders.Commands.ConfirmOrder;

public class ConfirmOrderHandler
    : IRequestHandler<ConfirmOrderCommand, bool>
{
    private readonly IOrderRepository _repository;

    public ConfirmOrderHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(
        ConfirmOrderCommand request,
        CancellationToken cancellationToken)
    {
        var order = await _repository.GetByIdAsync(request.OrderId);

        if (order == null)
            return false;

        order.Confirm();

        await _repository.SaveChangesAsync();

        return true;
    }
}