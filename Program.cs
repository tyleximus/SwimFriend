
using ConferenceScorePad;
using ConferenceScorePad.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Http client for loading static data
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register services
builder.Services.AddSingleton<EventLookupService>();
builder.Services.AddSingleton<ScoringService>();
builder.Services.AddSingleton<ResultService>();
builder.Services.AddSingleton<RosterService>();
builder.Services.AddSingleton<TotalsVisibilityService>();
builder.Services.AddScoped<StorageService>();

var host = builder.Build();

// preload events and any stored results
var events = host.Services.GetRequiredService<EventLookupService>();
await events.InitializeAsync();
var storage = host.Services.GetRequiredService<StorageService>();
var resultService = host.Services.GetRequiredService<ResultService>();
foreach (var r in await storage.LoadAsync())
    resultService.AddOrUpdate(r);

await host.RunAsync();
