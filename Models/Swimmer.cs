
namespace ConferenceScorePad.Models
{
    public record Swimmer(string Key, string FirstName, string LastName, string Gender, int Age, string TeamAbbr)
    {
        public string AgeGroup =>
            Age <= 6 ? "6 & U" :
            Age <= 8 ? "7-8" :
            Age <= 10 ? "9-10" :
            Age <= 12 ? "11-12" :
            Age <= 14 ? "13-14" : "15-18";

        public string Display => $"{FirstName} {LastName}";
    }
}
