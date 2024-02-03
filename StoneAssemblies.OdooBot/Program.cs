using StoneAssemblies.OdooBot;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<LocalCacheSyncWorker>();

var host = builder.Build();
host.Run();
