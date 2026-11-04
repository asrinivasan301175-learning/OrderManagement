using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Infrastructure.Persistence;

public static class SeedData
{
    public static async Task InitializeAsync(
        OrderDbContext context)
    {
        await context.Database.MigrateAsync();

        if (await context.Orders.AnyAsync())
            return;

        var order1 = Order.Create("Ashok");

        order1.AddItem(
            Guid.NewGuid(),
            "Laptop",
            1,
            65000);

        order1.AddItem(
            Guid.NewGuid(),
            "Wireless Mouse",
            2,
            1200);

        order1.Confirm();

        var order2 = Order.Create("Rahul");

        order2.AddItem(
            Guid.NewGuid(),
            "Keyboard",
            1,
            2500);

        var order3 = Order.Create("Priya");

        order3.AddItem(
            Guid.NewGuid(),
            "Monitor",
            2,
            15000);

        order3.Confirm();

        await context.Orders.AddRangeAsync(
            order1, order2, order3);

        await context.SaveChangesAsync();
    }
}