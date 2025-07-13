using System.Text.Json;
using ConferenceScorePad.Models;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

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

app.MapPost("/api/results", async (IEnumerable<Result> results) =>
{
    Directory.CreateDirectory(Path.GetDirectoryName(dataPath)!);
    var json = JsonSerializer.Serialize(results);
    await File.WriteAllTextAsync(dataPath, json);
    return Results.Ok();
});

app.Run();
