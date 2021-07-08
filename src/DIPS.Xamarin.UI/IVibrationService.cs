namespace DIPS.Xamarin.UI
{
    public interface IVibrationService
    {
        void Vibrate(int duration);

        void Click();

        void HeavyClick();

        void DoubleClick();

        void SelectionChanged();
        
        void Error();
    }
}