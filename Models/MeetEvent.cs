
namespace ConferenceScorePad.Models
{
    public record MeetEvent(int Number, string Gender, string AgeGroupText, string Distance, string Stroke, bool IsRelay)
    {
        public string Label => $"Event #{Number} {Gender} {AgeGroupText} {Distance} {Stroke}";
    }
}
