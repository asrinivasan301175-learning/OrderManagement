using MediatR;

namespace OrderManagement.Application.Features.Orders.Commands.ConfirmOrder;

public record ConfirmOrderCommand(Guid OrderId) : IRequest<bool>;