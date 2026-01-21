using System.Net;
using System.Text.Json;
using AgriProcurement.Procurement.Application.Exceptions;

namespace AgriProcurement.Procurement.API.Middlewares;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BusinessException ex)
        {
            _logger.LogWarning(ex, "Business exception");

            await WriteProblemDetails(
                context,
                HttpStatusCode.BadRequest,
                ex.Message);
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning(ex, "Not found");

            await WriteProblemDetails(
                context,
                HttpStatusCode.NotFound,
                ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");

            await WriteProblemDetails(
                context,
                HttpStatusCode.InternalServerError,
                "Unexpected error occurred");
        }
    }

    private static async Task WriteProblemDetails(
        HttpContext context,
        HttpStatusCode statusCode,
        string message)
    {
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/problem+json";

        var problem = new
        {
            type = "https://httpstatuses.com/" + (int)statusCode,
            title = message,
            status = (int)statusCode,
            traceId = context.TraceIdentifier
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(problem));
    }
}
