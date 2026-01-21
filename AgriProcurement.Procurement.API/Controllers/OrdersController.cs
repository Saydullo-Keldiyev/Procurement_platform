using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AgriProcurement.Procurement.Application.Commands.CreateOrder;

namespace AgriProcurement.Procurement.API.Controllers;

[ApiController]
[Route("api/procurement/orders")]
public sealed class OrdersController : ControllerBase
{
    private readonly CreateOrderCommandHandler _handler;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(
        CreateOrderCommandHandler handler,
        ILogger<OrdersController> logger)
    {
        _handler = handler;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(
        [FromBody] CreateOrderRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Create order called");

        var idempotencyKey =
            Request.Headers["Idempotency-Key"].ToString();

        var command = new CreateOrderCommand(
            Guid.NewGuid(),
            request.Lines,
            idempotencyKey);

        var result = await _handler.Handle(command, cancellationToken);

        _logger.LogInformation(
            "Order created successfully. OrderId: {OrderId}",
            result.OrderId);

        return Ok(result);
    }
}
