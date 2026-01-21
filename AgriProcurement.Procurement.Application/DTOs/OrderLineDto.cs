namespace AgriProcurement.Procurement.Application.DTOs;

public sealed record OrderLineDto(
    Guid ProductId,
    int Quantity,
    decimal Price,
    string Currency
);
