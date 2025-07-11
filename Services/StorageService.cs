
using ConferenceScorePad.Models;
using Microsoft.JSInterop;
using System.Text.Json;

namespace ConferenceScorePad.Services
{
    public class StorageService
    {
        private readonly IJSRuntime _js;
        private const string Key = "conference-score-pad-data";

        public StorageService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task SaveAsync(IEnumerable<Result> results)
        {
            var json = JsonSerializer.Serialize(results);
            await _js.InvokeVoidAsync("localforage.setItem", Key, json);
        }

        public async Task<IEnumerable<Result>> LoadAsync()
        {
            var json = await _js.InvokeAsync<string>("localforage.getItem", Key);
            if(string.IsNullOrWhiteSpace(json)) return Enumerable.Empty<Result>();
            return JsonSerializer.Deserialize<IEnumerable<Result>>(json) ?? Enumerable.Empty<Result>();
        }
    }
}
