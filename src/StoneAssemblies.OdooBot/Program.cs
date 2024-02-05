using Coravel;
using Serilog;
using Serilog.Events;
using StoneAssemblies.EntityFrameworkCore.Services.Interfaces;
using StoneAssemblies.EntityFrameworkCore.Services;
using StoneAssemblies.OdooBot;
using StoneAssemblies.OdooBot.Extensions;
using StoneAssemblies.OdooBot.Services;
using Microsoft.EntityFrameworkCore;
using PortaCapena.OdooJsonRpcClient;
using PortaCapena.OdooJsonRpcClient.Models;
using PortaCapena.OdooJsonRpcClient.Result;

var logger = Log.Logger = new LoggerConfiguration()
    .MinimumLevel
    .Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration
    .AddEnvironmentVariables()
    .AddEnvironmentVariablesWithPrefixAndSectionSeparator("BOT", "_")
    .AddJsonFiles(builder.Environment.EnvironmentName);


builder.Services.AddSerilog((provider, configuration) =>
{
    configuration.ReadFrom.Configuration(provider.GetRequiredService<IConfiguration>())
        .ReadFrom.Services(provider)
        .Enrich.FromLogContext()
        .WriteTo.Console();
});

logger.Information("Registering Coravel's services");
builder.Services.AddScheduler();
builder.Services.AddScoped<OddooSyncInvocable>();


logger.Information("Registering application's services");
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
builder.Services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));




builder.Services.AddScoped(typeof(OdooConfig), (provider) =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var configurationSection = configuration.GetSection("Odoo");
    var apiUrl  = configurationSection["ApiUrl"];
    var database = configurationSection["Database"];
    var username = configurationSection["Username"];
    var password = configurationSection["Password"];

    return new OdooConfig(apiUrl, database, username, password);
});

builder.Services.AddScoped(typeof(IOdooRepository<>), typeof(OdooRepository<>));

// builder.Services.AddHostedService<LocalCacheSyncWorker>();

var host = builder.Build();

logger.Information("Starting host");

try
{
    logger.Information("Migrating database");

    using var serviceScope = host.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
    var applicationDbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await applicationDbContext.Database.MigrateAsync();

    logger.Information("Configuring schedule");

    host.Services.UseScheduler(
            scheduler =>
            {
                scheduler
                    .Schedule<OddooSyncInvocable>()
                    .EveryTenSeconds()
                    .PreventOverlapping(nameof(OddooSyncInvocable))
                    .RunOnceAtStart();
            })
        .OnError(e =>
        {
            logger.Error(e, "Error scheduling task");
        });

    logger.Information("Built host, running now...");

    await host.RunAsync();
}
catch (Exception ex)
{
    logger.Fatal(ex, "Host terminated unexpectedly");

    Log.CloseAndFlush();
}