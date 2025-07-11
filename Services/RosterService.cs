
using ConferenceScorePad.Models;

namespace ConferenceScorePad.Services
{
    public class RosterService
    {
        private readonly Dictionary<string, Swimmer> _swimmers = new();

        public void ImportCsv(string csvContent)
        {
            var lines = csvContent.Split('\n', '\r');
            foreach(var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length < 5) continue;
                var first = parts[0].Trim();
                var last = parts[1].Trim();
                var gender = parts[2].Trim();
                if(!int.TryParse(parts[3], out var age)) continue;
                var team = parts[4].Trim();
                var key = $"{first.ToLower()}_{last.ToLower()}_{team}";
                var swimmer = new Swimmer(key, first, last, gender, age, team);
                _swimmers[key] = swimmer;
            }
        }
        public async Task InitializeAsync(HttpClient http)
        {
            if (_swimmers.Count > 0) return;

            var csv = await http.GetStringAsync("/data/roster.csv");
            ImportCsv(csv);               // re-use your existing parser
        }


        public IEnumerable<Swimmer> All => _swimmers.Values;
        public Swimmer? FindByKey(string key) => _swimmers.TryGetValue(key, out var s) ? s : null;
    }
}
