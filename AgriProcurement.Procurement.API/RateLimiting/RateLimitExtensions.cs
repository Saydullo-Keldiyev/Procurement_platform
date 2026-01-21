using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace AgriProcurement.Procurement.API.RateLimiting;

public static class RateLimitExtensions
{
    public static IServiceCollection AddProcurementRateLimiting(
        this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            options.AddFixedWindowLimiter("create-order", limiter =>
            {
                limiter.Window = TimeSpan.FromMinutes(1);
                limiter.PermitLimit = 20;
                limiter.QueueLimit = 0;
            });
        });

        return services;
    }
}
