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
            new UINotificationFeedbackGenerator().NotificationOccurred(UINotificationFeedbackType.Warning);
        }

        public void SelectionChanged()
        {
            new UISelectionFeedbackGenerator().SelectionChanged();
        }

        public void Error()
        {
            new UINotificationFeedbackGenerator().NotificationOccurred(UINotificationFeedbackType.Error);
        }

        public void Success()
        {
            new UINotificationFeedbackGenerator().NotificationOccurred(UINotificationFeedbackType.Success);
        }

        public IPlatformFeedbackGenerator Generate()
        {
            return new PlatformFeedbackGenerator();
        }

        internal static void Initialize()
        {
        }

        private class PlatformFeedbackGenerator : IPlatformFeedbackGenerator
        {
            private UISelectionFeedbackGenerator m_generator;

            public PlatformFeedbackGenerator()
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