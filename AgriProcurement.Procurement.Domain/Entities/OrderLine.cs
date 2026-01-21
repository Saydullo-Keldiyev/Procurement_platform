using AgriProcurement.Procurement.Domain.ValueObjects;
namespace AgriProcurement.Procurement.Domain.Entities;

public class OrderLine
{
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public Money Price { get; private set; }

    private OrderLine() { } // EF uchun

    public OrderLine(Guid productId, int quantity, Money price)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero");

        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }
}
