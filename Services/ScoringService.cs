
namespace ConferenceScorePad.Services
{
    public class ScoringService
    {
        private readonly int[] individual = {24,21,20,19,18,17,15,13,12,11,10,9,7,5,4,3,2,1};
        private readonly int[] relay =     {48,42,40,38,36,34,30,26,24,22,20,18,14,10,8,6,4,2};

        public int PointsForPlace(int place, bool isRelay)
        {
            if(place < 1 || place > 18) return 0;
            return isRelay ? relay[place - 1] : individual[place - 1];
        }
    }
}
