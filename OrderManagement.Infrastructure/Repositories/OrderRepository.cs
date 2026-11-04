using Microsoft.EntityFrameworkCore;
using OrderManagement.Application.Common.Interfaces;
using OrderManagement.Domain.Entities;
using OrderManagement.Infrastructure.Persistence;

namespace OrderManagement.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext _context;

    public OrderRepository(OrderDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await _context.Orders
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await _context.Orders
            .Include(x => x.Items)
            .OrderByDescending(x => x.OrderDate)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}