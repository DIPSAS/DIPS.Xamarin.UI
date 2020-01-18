using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.Sheet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SheetView : ContentView
    {
        private SheetBehavior m_sheetBehaviour;

        public SheetView(SheetBehavior sheetBehavior)
        {
            InitializeComponent();
            OuterSheetFrame.BindingContext = m_sheetBehaviour = sheetBehavior;
        }

        /// <summary>
        /// The height that the sheet content needs if it should display all of its content
        /// </summary>
        public double SheetContentHeighRequest =>
            SheetContent.Height + HandleBoxView.Height + OuterSheetFrame.Padding.Top + OuterSheetFrame.Padding.Bottom + OuterSheetFrame.CornerRadius;

        public Frame SheetFrame => OuterSheetFrame;

        private void PanGestureRecognizer_OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            //Should never 
            // Handle the pan
            //Calculate position of the sheetview based on the 
        }
    }
}