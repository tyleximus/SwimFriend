
namespace ConferenceScorePad.Models
{
    public record Result(int EventNumber, int Place, string SwimmerKey, string TeamAbbr, bool IsRelay, double TimeSeconds);
}
