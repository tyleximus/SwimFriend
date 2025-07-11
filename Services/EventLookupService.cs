
using ConferenceScorePad.Models;
using System.Net.Http;

namespace ConferenceScorePad.Services
{
    public class EventLookupService
    {
        private readonly HttpClient _http;
        private readonly Dictionary<int, MeetEvent> _events = new();

        public EventLookupService(HttpClient http)
        {
            _http = http;
        }

        public async Task InitializeAsync()
        {
            if (_events.Count > 0) return;

            var csv = await _http.GetStringAsync("data/events.csv");
            using var reader = new StringReader(csv);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                var parts = line.Split(',');
                if (parts.Length < 6 || !int.TryParse(parts[0], out var num)) continue;
                var ev = new MeetEvent(num, parts[1], parts[2], parts[3], parts[4], parts[5].Trim().ToLower() == "true");
                _events[num] = ev;
            }
        }

        public IReadOnlyDictionary<int, MeetEvent> All => _events;
        public MeetEvent? Get(int number) => _events.TryGetValue(number, out var ev) ? ev : null;
    }
}
