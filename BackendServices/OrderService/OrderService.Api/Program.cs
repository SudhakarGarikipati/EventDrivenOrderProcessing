using InventoryService.Grpc;
using OrderService.Infrastructure;
using OrderService.Infrastructure.Observability.Correlation;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog configuration
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers();

// Required for gRPC client infrastructure
builder.Services.AddGrpc();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

ServiceRegistration.RegisterServices(builder.Services, builder.Configuration);

builder.Services.AddGrpcClient<InventoryGrpc.InventoryGrpcClient>(o =>
{
    o.Address = new Uri("https://localhost:7195");
});


var app = builder.Build();

// Add CorrelationIdMiddleware to the pipeline
app.UseMiddleware<CorrelationIdMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
