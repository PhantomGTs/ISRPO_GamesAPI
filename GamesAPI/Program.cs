using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using GamesAPI.Data;
using GamesAPI.Repository.Interfaces;
using GamesAPI.Repository.Services;
using LokiLoggingProvider.Options;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GamesDbContext>(x => x.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));
builder.Services.AddScoped<IGameService, GameService>();


//подключение метрик
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});
builder.Services.AddMetrics();
builder.Services.AddMetricsTrackingMiddleware();
builder.Host
    .UseMetricsWebTracking()
    .UseMetrics(options => { options.EndpointOptions = endpointsOptions =>
        {
            endpointsOptions.MetricsTextEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
            endpointsOptions.MetricsEndpointOutputFormatter = new MetricsPrometheusProtobufOutputFormatter();
            endpointsOptions.EnvironmentInfoEndpointEnabled = false;
        };
    });

//подключение логов
builder.Logging.AddLoki(loggerOptions =>
{
    loggerOptions.Client = PushClient.Grpc;
    loggerOptions.StaticLabels.JobName = "GamesAPI";
});

//подключение трейсов
const string serviceName = "GamesService";
builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService(serviceName))
    .WithTracing(traceProviderBuilder =>
        traceProviderBuilder
            .AddSource(serviceName)
            .AddAspNetCoreInstrumentation(options => options.RecordException = true)
            .AddOtlpExporter()
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();