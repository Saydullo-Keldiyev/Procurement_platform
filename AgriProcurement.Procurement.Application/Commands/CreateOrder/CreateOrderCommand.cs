using AgriProcurement.Procurement.Application.DTOs;

namespace AgriProcurement.Procurement.Application.Commands.CreateOrder;

public sealed record CreateOrderCommand(
    Guid OrderId,
    IReadOnlyCollection<OrderLineDto> Lines,
    string IdempotencyKey
);
