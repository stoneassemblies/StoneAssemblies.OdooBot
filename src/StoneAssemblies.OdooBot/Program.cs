using Coravel;
using Serilog;
using Serilog.Events;
using StoneAssemblies.EntityFrameworkCore.Services.Interfaces;
using StoneAssemblies.EntityFrameworkCore.Services;
using StoneAssemblies.OdooBot.Extensions;
using StoneAssemblies.OdooBot.Services;
using Microsoft.EntityFrameworkCore;
using PortaCapena.OdooJsonRpcClient;
using PortaCapena.OdooJsonRpcClient.Models;
using Microsoft.FluentUI.AspNetCore.Components;
using StoneAssemblies.OdooBot.Components;
using StoneAssemblies.OdooBot.Wasm.Pages;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using StoneAssemblies.OdooBot.Client;
using System.Reflection;
using AngleSharp.Io.Dom;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using QuestPDF.Infrastructure;
using Blorc.Services;
using System.Net;

using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;

QuestPDF.Settings.License = LicenseType.Community;
QuestPDF.Settings.EnableDebugging = true;
QuestPDF.Settings.CheckIfAllTextGlyphsAreAvailable = false;

var logger = Log.Logger = new LoggerConfiguration()
    .MinimumLevel
    .Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddEnvironmentVariables()
    .AddEnvironmentVariablesWithPrefixAndSectionSeparator("BOT", "_")
    .AddJsonFiles(builder.Environment.EnvironmentName);

builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<Program>());

// TODO: Improve this later.
var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntityType<global::StoneAssemblies.OdooBot.Entities.Category>();
modelBuilder.EntityType<global::StoneAssemblies.OdooBot.Entities.Product>();
modelBuilder.EntitySet<global::StoneAssemblies.OdooBot.Entities.Category>("Categories");
modelBuilder.EntitySet<global::StoneAssemblies.OdooBot.Entities.Product>("Products");

builder.Services
    .AddControllersWithViews()
    .AddOData(options => options
        .Select().Filter().OrderBy().Expand().Count().SetMaxTop(null).AddRouteComponents(
            "odata",
            modelBuilder.GetEdmModel()))
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        //options.SerializerSettings.Converters.Add(new StringEnumConverter());
        //options.SerializerSettings.Converters.Add(new DateOnlyListJsonConverter());
    });

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(options =>
    {
        options.CustomOperationIds(description => $"{description.ActionDescriptor.RouteValues["controller"]}_{description.ActionDescriptor.RouteValues["action"]}");

        options.SwaggerDoc(
            "v1",
            new OpenApiInfo
            {
                Version = "v1",
                Title = "OdooBot API",
                Description = "A Web API for OdooBot",
                TermsOfService = new Uri("https://stoneassemblies.github.io/terms"),
                Contact = new OpenApiContact { Name = "OdooBot Contact", Url = new Uri("https://stoneassemblies.github.io/contact") },
                License = new OpenApiLicense { Name = "OdooBot License", Url = new Uri("https://stoneassemblies.github.io/license") },
            });

        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

        options.MapType<TimeSpan>(() => new OpenApiSchema { Type = "string", Format = "time-span" });

        // TODO: Review this?
        options.UseAllOfToExtendReferenceSchemas();
    });

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddFluentUIComponents();

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
    var apiUrl = configurationSection["ApiUrl"];
    var database = configurationSection["Database"];
    var username = configurationSection["Username"];
    var password = configurationSection["Password"];

    return new OdooConfig(apiUrl, database, username, password);
});

builder.Services.AddScoped(typeof(IOdooRepository<>), typeof(OdooRepository<>));

builder.Services.AddHttpClient<ICatalogServiceClient, CatalogServiceClient>((serviceProvider, client) =>
{
    var server = serviceProvider.GetRequiredService<IServer>();
    var baseAddress = server.Features.Get<IServerAddressesFeature>()!.Addresses.FirstOrDefault();
    if (baseAddress!.Contains("0.0.0.0"))
    {
        baseAddress = baseAddress.Replace("0.0.0.0", "127.0.0.1");
    }

    client.BaseAddress = new Uri(baseAddress!);
});

var app = builder.Build();

logger.Information("Starting host");

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger().UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    // app.UseHsts();
}

// app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();
app.MapControllers();
// app.UseEndpoints(endpoints => endpoints.MapControllers());

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly); // Load this in other way?


try
{
    logger.Information("Migrating database");

    using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
    var applicationDbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await applicationDbContext.Database.MigrateAsync();

    logger.Information("Configuring schedule");

    app.Services.UseScheduler(
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

    await app.RunAsync();
}
catch (Exception ex)
{
    logger.Fatal(ex, "Host terminated unexpectedly");

    Log.CloseAndFlush();
}
