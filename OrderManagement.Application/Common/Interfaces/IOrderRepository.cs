using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Common.Interfaces;

public interface IOrderRepository
{
    Task AddAsync(Order order);

    Task<Order?> GetByIdAsync(Guid id);

    Task<List<Order>> GetAllAsync();

    Task SaveChangesAsync();
}