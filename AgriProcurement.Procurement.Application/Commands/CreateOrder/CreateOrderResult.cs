namespace AgriProcurement.Procurement.Application.Commands.CreateOrder;

public sealed record CreateOrderResult(
    Guid OrderId,
    string Status
);
