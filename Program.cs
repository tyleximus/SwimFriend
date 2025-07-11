
using ConferenceScorePad;
using ConferenceScorePad.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register services
builder.Services.AddSingleton<EventLookupService>();
builder.Services.AddSingleton<ScoringService>();
builder.Services.AddSingleton<ResultService>();
builder.Services.AddSingleton<RosterService>();
builder.Services.AddScoped<StorageService>();

await builder.Build().RunAsync();

builder.Services.AddSingleton<TotalsVisibilityService>();
