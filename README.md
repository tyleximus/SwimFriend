# SwimFriend

This repository contains a Blazor WebAssembly application along with a small
ASP.NET Core backend. The backend stores meet results in a JSON file so that all
visitors share the same data.

## Running

1. Start the backend API:
   ```bash
   dotnet run --project Backend
   ```
   This will listen on the default ports and manage `Data/results.json`.

2. In another terminal run the WebAssembly app:
   ```bash
   dotnet run --project ConferenceScorePad.csproj
   ```

Both projects are part of `ConferenceScorePad.sln` and can be built together
with `dotnet build`.
