using OrderManagement.Domain.ValueObjects;

namespace OrderManagement.Domain.Entities;

public class OrderItem
{
    public Guid Id { get; private set; }

    public Guid ProductId { get; private set; }

    public string ProductName { get; private set; } = string.Empty;

    public int Quantity { get; private set; }

    public Money UnitPrice { get; private set; } = new(0, "INR");

    private OrderItem() { } // EF Core

    internal OrderItem(
        Guid productId,
        string productName,
        int quantity,
        Money unitPrice)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero");

        if (string.IsNullOrWhiteSpace(productName))
            throw new ArgumentException("Product name is required");

        Id = Guid.NewGuid();
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public decimal GetTotal()
    {
        return UnitPrice.Amount * Quantity;
    }
}