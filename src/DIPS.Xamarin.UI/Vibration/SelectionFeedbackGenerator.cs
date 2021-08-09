namespace DIPS.Xamarin.UI.Vibration
{
    /// <summary>
    /// Use this for continuous short feedback when a selection changes.
    /// </summary>
    public sealed class SelectionFeedbackGenerator
    {
        private readonly IPlatformFeedbackGenerator? m_generator;

        /// <summary>
        /// Use this for continuous short feedback when a selection changes.
        /// </summary>
        public SelectionFeedbackGenerator()
        {
            if (Vibration.VibrationService != null)
            {
                m_generator = Vibration.VibrationService.Generate();
            }
        }
        
        /// <summary>
        /// Invoke when a selection has occured. Causes feedback.
        /// </summary>
        public void SelectionChanged()
        {
            m_generator?.SelectionChanged();
        }
        
        /// <summary>
        /// Invoke right before feedback is required. For example at the start of a pan where several selections occur rapidly. 
        /// </summary>
        public void Prepare()
        {
            m_generator?.Prepare();
        }

        /// <summary>
        /// Release the underlying iOS generator. Use when this is no longer needed. For example at the end of a pan.
        /// </summary>
        public void Release()
        {
            m_generator?.Release();
        }

    }
}