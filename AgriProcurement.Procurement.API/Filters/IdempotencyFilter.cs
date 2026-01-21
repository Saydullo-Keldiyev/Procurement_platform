using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AgriProcurement.Procurement.API.Filters;

public sealed class IdempotencyFilter : IActionFilter
{
    private const string HeaderName = "Idempotency-Key";

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(
                HeaderName, out var value) ||
            string.IsNullOrWhiteSpace(value))
        {
            context.Result = new BadRequestObjectResult(
                $"{HeaderName} header is required");
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
