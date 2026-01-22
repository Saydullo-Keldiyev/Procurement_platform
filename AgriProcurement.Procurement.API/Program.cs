using AgriProcurement.Procurement.API.Extensions;
using AgriProcurement.Procurement.API.Middlewares;
using AgriProcurement.Procurement.API.RateLimiting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Prometheus;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

const string serviceName = "AgriProcurement.Procurement.API";

// Swagger/OpenAPI konfiguratsiyasi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AgriProcurement API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Idempotency-Key", new OpenApiSecurityScheme
    {
        Name = "Idempotency-Key",
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Description = "Idempotency key for POST requests"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Idempotency-Key"
                }
            },
            Array.Empty<string>()
        }
    });
});

// 🔹 Rate limiter
builder.Services.AddProcurementRateLimiting();

// Serilog
builder.Host.UseSerilog((context, config) =>
{
    config
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console();
});

//Opentelementry
builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource =>
        resource.AddService(serviceName))
    .WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddEntityFrameworkCoreInstrumentation()
            .AddConsoleExporter(); // hozircha
    })
    .WithMetrics(metrics =>
    {
        metrics
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddConsoleExporter();
    });

// 🔹 DI (Application + Infrastructure)
builder.Services.AddProcurementModule(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    // Swagger middleware
    app.UseSwagger(); // JSON endpoint
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AgriProcurement API v1");
        c.DocExpansion(DocExpansion.List);
        c.RoutePrefix = string.Empty; // Swagger UI ni root (localhost:port/) ga qo'yadi
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHttpMetrics();
app.UseMetricServer();

app.MapControllers();

app.Run();
