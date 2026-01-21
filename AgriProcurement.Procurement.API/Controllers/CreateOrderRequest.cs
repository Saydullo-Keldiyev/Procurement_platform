using AgriProcurement.Procurement.Application.DTOs;

namespace AgriProcurement.Procurement.API.Controllers;

public sealed record CreateOrderRequest(
    IReadOnlyCollection<OrderLineDto> Lines
);
