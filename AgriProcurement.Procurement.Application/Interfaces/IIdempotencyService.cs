namespace AgriProcurement.Procurement.Application.Interfaces;

public interface IIdempotencyService
{
    Task<bool> IsProcessedAsync(string key, CancellationToken cancellationToken);
    Task MarkAsProcessedAsync(string key, CancellationToken cancellationToken);
}
