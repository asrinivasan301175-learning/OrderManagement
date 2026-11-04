namespace OrderManagement.Application.DTOs;

public class OrderDto
{
    public Guid Id { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public decimal TotalAmount { get; set; }

    public List<OrderItemDto> Items { get; set; } = new();
}

public class OrderItemDto
{
    public string ProductName { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal Total { get; set; }
}