using ConferenceScorePad;
using ConferenceScorePad.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient for static files (CSV/JSON) served by the Blazor app
builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new(builder.HostEnvironment.BaseAddress) });

// Separate client for backend API calls
builder.Services.AddHttpClient("backend", client =>
    client.BaseAddress = new("https://localhost:7263/"));

// DI registrations
builder.Services.AddScoped<EventLookupService>();
builder.Services.AddScoped<RosterService>();
builder.Services.AddSingleton<ScoringService>();
builder.Services.AddSingleton<ResultService>();
builder.Services.AddSingleton<TotalsVisibilityService>();
builder.Services.AddScoped<StorageService>();

var host = builder.Build();

// PRELOAD lookup tables before the UI shows
var http = host.Services.GetRequiredService<HttpClient>();
await host.Services.GetRequiredService<EventLookupService>().InitializeAsync();
await host.Services.GetRequiredService<RosterService>().InitializeAsync(http);

// (Any stored results, etc.)
var storage = host.Services.GetRequiredService<StorageService>();
var resultSvc = host.Services.GetRequiredService<ResultService>();
foreach (var r in await storage.LoadAsync())
    resultSvc.AddOrUpdate(r);

await host.RunAsync();
