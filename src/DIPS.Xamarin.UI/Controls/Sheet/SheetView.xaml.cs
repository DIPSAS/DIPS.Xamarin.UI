using System;
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

            var TranslationY = OuterSheetFrame.TranslationY;

            var TotalY_Modified = e.TotalY + TranslationY;
            //Hack to remove jitter from android 
            if (Device.RuntimePlatform == Device.Android)
            {
                e = new PanUpdatedEventArgs(e.StatusType, e.GestureId, 0, TotalY_Modified);
            }

            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    break;
                case GestureStatus.Running:
                    if (m_sheetBehaviour.Alignment == AlignmentOptions.Bottom)
                    {
                        /*var translationY = SheetContent.TranslationY + e.TotalY*/;
                        m_sheetBehaviour.UpdatePosition(e.TotalY);

                        //if (e.TotalY > 0) //Pan up
                        //{
                        //    var newHeightOfSheet = SheetContentHeighRequest + (e.TotalY * -1);
                        //    m_sheetBehaviour.UpdatePosition(newHeightOfSheet);
                        //}
                        //else //Pan down
                        //{
                        //    var newHeightOfSheet = SheetContentHeighRequest + (e.TotalY);
                        //    m_sheetBehaviour.UpdatePosition(newHeightOfSheet);
                        //}
                    }

                    break;
                case GestureStatus.Completed:
                    break;
                case GestureStatus.Canceled:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            //Should never 
            // Handle the pan
            //Calculate position of the sheetview based on the 
        }
    }
}