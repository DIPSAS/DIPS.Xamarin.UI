namespace DIPS.Xamarin.UI.Controls.RadioButtonGroup.Abstractions {
    internal interface IHandleRadioButtons
    {
        /// <summary>
        /// Method to be invoked when a radio button is tapped
        /// </summary>
        /// <param name="tappedRadioButton"></param>
        void OnRadioButtonTapped(Internal.xaml.RadioButton tappedRadioButton);
    }
}