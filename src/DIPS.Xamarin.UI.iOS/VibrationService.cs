using DIPS.Xamarin.UI.Vibration;
using UIKit;

namespace DIPS.Xamarin.UI.iOS
{
    internal class VibrationService : IVibrationService
    {
        private readonly UINotificationFeedbackGenerator m_uiNotificationFeedbackGenerator =
            new UINotificationFeedbackGenerator();

        private readonly UISelectionFeedbackGenerator m_uiSelectionFeedbackGenerator =
            new UISelectionFeedbackGenerator();

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
            new UIImpactFeedbackGenerator(UIImpactFeedbackStyle.Rigid).ImpactOccurred();
        }

        public void DoubleClick()
        {
        }

        public void SelectionChanged()
        {
            new UISelectionFeedbackGenerator().SelectionChanged();
        }

        public void Error()
        {
            new UINotificationFeedbackGenerator().NotificationOccurred(UINotificationFeedbackType.Error);
        }

        public IGenerator Generate()
        {
            return new PlatformGenerator();
        }

        internal static void Initialize()
        {
        }

        private class PlatformGenerator : IGenerator
        {
            private UISelectionFeedbackGenerator m_generator;

            public PlatformGenerator()
            {
                m_generator = new UISelectionFeedbackGenerator();
            }

            public void Prepare()
            {
                m_generator?.Prepare();
            }

            public void Release()
            {
                m_generator = null;
            }

            public void SelectionChanged()
            {
                m_generator?.SelectionChanged();
            }
        }
    }
}