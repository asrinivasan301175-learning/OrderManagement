using OrderManagement.Domain.Enums;
using OrderManagement.Domain.ValueObjects;

namespace OrderManagement.Domain.Entities;

public class Order
{
    public Guid Id { get; private set; }

    public string CustomerName { get; private set; } = string.Empty;

    public DateTime OrderDate { get; private set; }

    public OrderStatus Status { get; private set; }

    private readonly List<OrderItem> _items = new();

    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public decimal TotalAmount =>
        _items.Sum(x => x.GetTotal());

    private Order() { } // EF Core

    private Order(string customerName)
    {
        if (string.IsNullOrWhiteSpace(customerName))
            throw new ArgumentException("Customer name is required");

        Id = Guid.NewGuid();
        CustomerName = customerName;
        OrderDate = DateTime.UtcNow;
        Status = OrderStatus.Pending;
    }

    public static Order Create(string customerName)
    {
        return new Order(customerName);
    }

    public void AddItem(
        Guid productId,
        string productName,
        int quantity,
        decimal price)
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException(
                "Items can only be added to pending orders");

        var item = new OrderItem(
            productId,
            productName,
            quantity,
            new Money(price, "INR"));

        _items.Add(item);
    }

    public void Confirm()
    {
        if (!_items.Any())
            throw new InvalidOperationException(
                "Cannot confirm an empty order");

        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException(
                "Only pending orders can be confirmed");

        Status = OrderStatus.Confirmed;
    }

    public void Cancel()
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException(
                "Only pending orders can be cancelled");

        Status = OrderStatus.Cancelled;
    }
}