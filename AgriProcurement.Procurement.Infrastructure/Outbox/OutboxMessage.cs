namespace AgriProcurement.Procurement.Infrastructure.Outbox;

public sealed class OutboxMessage
{
    public Guid Id { get; private set; }
    public string Type { get; private set; } = default!;
    public string Payload { get; private set; } = default!;
    public DateTime OccurredOn { get; private set; }
    public DateTime? ProcessedOn { get; private set; }

    private OutboxMessage() { }

    public OutboxMessage(string type, string payload)
    {
        Id = Guid.NewGuid();
        Type = type;
        Payload = payload;
        OccurredOn = DateTime.UtcNow;
    }

    public void MarkProcessed()
    {
        ProcessedOn = DateTime.UtcNow;
    }
}
