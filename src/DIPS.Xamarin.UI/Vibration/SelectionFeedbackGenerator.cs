namespace DIPS.Xamarin.UI.Vibration
{
    /// <summary>
    /// Use this for continuous short feedback when a selection changes.
    /// </summary>
    public sealed class SelectionFeedbackGenerator
    {
        private readonly IGenerator? m_generator;

        public SelectionFeedbackGenerator()
        {
            if (Vibration.VibrationService != null)
            {
                m_generator = Vibration.VibrationService.Generate();
            }
        }
        
        public void SelectionChanged()
        {
            m_generator?.SelectionChanged();
        }
        
        public void Prepare()
        {
            m_generator?.Prepare();
        }

        public void Release()
        {
            m_generator?.Release();
        }

    }
}