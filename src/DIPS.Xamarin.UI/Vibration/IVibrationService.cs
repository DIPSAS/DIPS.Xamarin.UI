namespace DIPS.Xamarin.UI.Vibration
{
    internal interface IVibrationService
    {
        void Vibrate(int duration);

        void Click();

        void HeavyClick();

        void DoubleClick();

        void Error();
        
        void Success();
        
        IGenerator Generate();
    }

    internal interface IGenerator
    {
        void SelectionChanged();

        void Prepare();

        void Release();
    }
}