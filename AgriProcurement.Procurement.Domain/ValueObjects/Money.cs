namespace AgriProcurement.Procurement.Domain.ValueObjects;
public sealed class Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive");

        Currency = currency ?? throw new ArgumentNullException(nameof(currency));
        Amount = amount;
    }
}
