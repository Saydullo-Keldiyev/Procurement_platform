namespace AgriProcurement.Procurement.Application.Sagas;

public interface ISagaEventPublisher
{
    Task PublishAsync<T>(
        T @event,
        CancellationToken cancellationToken);
}
