using ConferenceScorePad.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Register Swagger/OpenAPI services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Program.cs -- configure server JSON options
builder.Services.Configure<JsonOptions>(opts =>
{
    opts.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    opts.JsonSerializerOptions.PropertyNameCaseInsensitive = true;  // optional safety net
});

// Allow CORS from any origin so the Blazor client can access the API
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Enable CORS
app.UseCors();

// Enable Swagger in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

string dataPath = Path.Combine(app.Environment.ContentRootPath, "Data", "results.json");

app.MapGet("/api/results", async () =>
{
    if (!File.Exists(dataPath))
    {
        return Results.Ok(new List<Result>());
    }

    var json = await File.ReadAllTextAsync(dataPath);
    var results = JsonSerializer.Deserialize<List<Result>>(json) ?? new List<Result>();
    return Results.Ok(results);
});

app.MapPost("/api/results", async ([FromBody] List<Result> results) =>
{
    Directory.CreateDirectory(Path.GetDirectoryName(dataPath)!);
    var json = JsonSerializer.Serialize(results);
    await File.WriteAllTextAsync(dataPath, json);
    return Results.Ok();
});

app.Run();
