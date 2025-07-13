
using ConferenceScorePad.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace ConferenceScorePad.Services
{
    public class StorageService
    {
        private readonly HttpClient _http;

        public StorageService(IHttpClientFactory httpFactory)
        {
            _http = httpFactory.CreateClient("backend");
        }

        public async Task SaveAsync(IEnumerable<Result> results)
        {
            await _http.PostAsJsonAsync("api/results", results);
        }

        public async Task<IEnumerable<Result>> LoadAsync()
        {
            try
            {
                var data = await _http.GetFromJsonAsync<List<Result>>("api/results");
                return data ?? Enumerable.Empty<Result>();
            }
            catch
            {
                return Enumerable.Empty<Result>();
            }
        }
    }
}
