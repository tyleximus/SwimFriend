
namespace ConferenceScorePad.Services
{
    public class TotalsVisibilityService
    {
        public bool ShowTotals { get; private set; } = true;
        public event Action? OnChange;
        public void Toggle()
        {
            ShowTotals = !ShowTotals;
            OnChange?.Invoke();
        }
    }
}
