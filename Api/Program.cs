
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net;
using System.Reflection;
using System.Text.Json;
using System.Text;
using Api.ExtensionService;
using Api.MiddleWare;
using Domain.Environments;
using Infrastruceture.Environments;
using Domain.Repository;
using Infrastruceture.Repository;
using Domain.Transport;
using Infrastruceture.Transport;
using Newtonsoft.Json.Converters;
using Serilog;
using MediatR;

var builder = WebApplication.CreateBuilder(args);



// Config Serilog
builder.Host.UseSerilog((ctx, cfg) =>
{
    cfg.ReadFrom.Configuration(ctx.Configuration).WriteTo.ColoredConsole();
});

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ssZ";
    options.SerializerSettings.Converters.Add(new StringEnumConverter());
});

builder.Services.AddTransient<ServiceFactory>(p => p.GetService);
builder.Services.AddSingleton<IEnvironmentsConfig, EnvironmentsConfig>();
builder.Services.AddMemoryCache();
builder.Services.AddLazyCache();
builder.Services.AddMongoDb(builder.Configuration);
builder.Services.AddHealthChecks();
builder.Services.AddSingleton<IMemoryStore, MemoryStore>();
builder.Services.AddSingleton<IRestAPI, RestAPI>();
builder.Services.AddRedis(builder.Configuration);
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddCommandHandler();
builder.Services.AddHttpContextAccessor();
//builder.Services.AddHostedService<ServiceStart>();

builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<GzipCompressionProvider>();
});

ServicePointManager.DefaultConnectionLimit = Int32.MaxValue;

var app = builder.Build();

app.UseResponseCompression();

app.MapControllers();
app.UseSerilogRequestLogging();

app.UseMyMiddleware();

app.MapHealthChecks("/health", new HealthCheckOptions { ResponseWriter = WriteResponse });

await app.RunAsync("http://*:8080");

static Task WriteResponse(HttpContext context, HealthReport result)
{
    context.Response.ContentType = "application/json; charset=utf-8";

    var options = new JsonWriterOptions
    {
        Indented = true
    };

    using var stream = new MemoryStream();
    using (var writer = new Utf8JsonWriter(stream, options))
    {
        writer.WriteStartObject();
        writer.WriteString("status", "success");
        writer.WriteString("message", "OK");
        writer.WriteNull("data");
        writer.WriteEndObject();
    }

    var json = Encoding.UTF8.GetString(stream.ToArray());

    return context.Response.WriteAsync(json);
}