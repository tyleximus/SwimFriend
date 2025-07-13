# SwimFriend

This repository contains a Blazor WebAssembly application along with a small
ASP.NET Core backend. The backend stores meet results in a JSON file so that all
visitors share the same data.

## Running

1. Start the backend API (which now also serves the Blazor app):
   ```bash
   dotnet run --project Backend
   ```
   The service listens at `https://localhost:7263` and manages `Data/results.json`.

   To override the API URL used by the Blazor client, set the `BackendUrl` environment
   variable when running the app.

Both projects are part of `ConferenceScorePad.sln` and can be built together with
`dotnet build`. The backend hosts the compiled WebAssembly files from `wwwroot`.
