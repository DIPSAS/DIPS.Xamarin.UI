using UIKit;

namespace DIPS.Xamarin.UI.iOS
{
    public class VibrationService : IVibrationService
    {
        private readonly UISelectionFeedbackGenerator m_uiSelectionFeedbackGenerator = new UISelectionFeedbackGenerator();
        private readonly UINotificationFeedbackGenerator m_uiNotificationFeedbackGenerator = new UINotificationFeedbackGenerator();

        internal static void Initialize()
        {
        }
        
        public void Vibrate(int duration)
        {
            new UIImpactFeedbackGenerator(UIImpactFeedbackStyle.Medium).ImpactOccurred();
        }

        public void Click()
        {
            new UIImpactFeedbackGenerator(UIImpactFeedbackStyle.Medium).ImpactOccurred();
        }

        public void HeavyClick()
        {
            new UIImpactFeedbackGenerator(UIImpactFeedbackStyle.Heavy).ImpactOccurred();
        }

        public void DoubleClick()
        {
            
        }

        public void SelectionChanged()
        {
            m_uiSelectionFeedbackGenerator.SelectionChanged();
        }

        public void Error()
        {
            m_uiNotificationFeedbackGenerator.NotificationOccurred(UINotificationFeedbackType.Error);
        }
    }
}