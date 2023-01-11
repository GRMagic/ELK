using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.RabbitMQ.Sinks.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Configuração do Log

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithProperty("_applicationName", "My Application")
    .WriteTo.RabbitMQ((clientConfiguration, sinkConfiguration) => {
        var rabbitMQClientConfiguration = new RabbitMQClientConfiguration();
        builder.Configuration.Bind("Logging:RabbitMq", rabbitMQClientConfiguration);
        clientConfiguration.From(rabbitMQClientConfiguration);
        sinkConfiguration.TextFormatter = new JsonFormatter(renderMessage: true);
    }).CreateLogger();
var loggerFactory = new LoggerFactory();
loggerFactory.AddSerilog();

builder.Services.AddSingleton<ILoggerFactory>(loggerFactory);

#endregion

var app = builder.Build();

app.UseSwagger(o => o.RouteTemplate = "{documentName}/swagger.{json|yaml}");
app.UseSwaggerUI(o => o.RoutePrefix = "");

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
