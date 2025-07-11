
using ConferenceScorePad.Models;
using System.Collections.ObjectModel;

namespace ConferenceScorePad.Services
{
    public class ResultService
    {
        private readonly ScoringService _scoring;
        private readonly Dictionary<int, List<Result>> _byEvent = new();
        public ObservableCollection<Result> Results { get; } = new();

        public ResultService(ScoringService scoring)
        {
            _scoring = scoring;
        }

        public void AddOrUpdate(Result r)
        {
            if (!_byEvent.TryGetValue(r.EventNumber, out var list))
            {
                list = new();
                _byEvent[r.EventNumber] = list;
            }

            // remove existing by swimmer key if exists
            list.RemoveAll(x => x.SwimmerKey == r.SwimmerKey);
            list.Add(r);

            // reorder by time
            list.Sort((a,b) => a.TimeSeconds.CompareTo(b.TimeSeconds));

            // assign places accounting for ties
            double? prevTime = null;
            int prevPlace = 0;
            for(int i=0;i<list.Count;i++)
            {
                var current = list[i];
                int place;
                if (prevTime.HasValue && Math.Abs(current.TimeSeconds - prevTime.Value) < 0.0001)
                {
                    place = prevPlace; // same place
                }
                else
                {
                    place = i + 1;
                    prevPlace = place;
                    prevTime = current.TimeSeconds;
                }
                var updated = current with { Place = place };
                list[i] = updated;
            }

            // flatten back to Results collection (TODO optimize)
            Results.Clear();
            foreach(var evList in _byEvent.Values.SelectMany(x=>x))
                Results.Add(evList);
        }
    }
}
