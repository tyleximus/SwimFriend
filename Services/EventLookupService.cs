
using ConferenceScorePad.Models;
using System.Globalization;
using System.Reflection;

namespace ConferenceScorePad.Services
{
    public class EventLookupService
    {
        private readonly Dictionary<int, MeetEvent> _events = new();
        public EventLookupService()
        {
            // load csv from embedded resource or wwwroot
            var path = Path.Combine(AppContext.BaseDirectory, "wwwroot", "data", "events.csv");
            if (File.Exists(path))
            {
                using var reader = new StreamReader(path);
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(',');
                    if (parts.Length < 6 || !int.TryParse(parts[0], out var num)) continue;
                    var ev = new MeetEvent(num, parts[1], parts[2], parts[3], parts[4], parts[5].Trim().ToLower() == "true");
                    _events[num] = ev;
                }
            }
        }

        public IReadOnlyDictionary<int, MeetEvent> All => _events;
        public MeetEvent? Get(int number) => _events.TryGetValue(number, out var ev) ? ev : null;
    }
}
