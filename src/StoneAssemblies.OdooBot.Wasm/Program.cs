using Blorc.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;
using StoneAssemblies.OdooBot.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp =>
{
    var httpClient = new HttpClient
    {
        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress),
    };

    return httpClient;
});

builder.Services.AddHttpClient<ICatalogServiceClient, CatalogServiceClient>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddBlorcCore();

builder.Services.AddFluentUIComponents();

var webAssemblyHost = builder.Build();
await webAssemblyHost.ConfigureDocumentAsync(
    async documentService =>
    {
        await documentService.InjectBlorcCoreJsAsync();
    });

await webAssemblyHost.RunAsync();
