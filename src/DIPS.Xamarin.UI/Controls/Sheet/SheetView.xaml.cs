using System;
using Xamarin.Essentials;
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

        private void OnDrag(object sender, PanUpdatedEventArgs e)
        {
            if (!m_sheetBehaviour.IsDraggable) return;
            //Hack to remove jitter from android 
            if (Device.RuntimePlatform == Device.Android)
            {
                var TranslationY = OuterSheetFrame.TranslationY;
                var TotalY_Modified = e.TotalY + TranslationY;

                e = new PanUpdatedEventArgs(e.StatusType, e.GestureId, 0, TotalY_Modified);
            }

            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    break;
                case GestureStatus.Running:
                    if (m_sheetBehaviour.Alignment == AlignmentOptions.Bottom)
                    {
                        m_sheetBehaviour.UpdatePosition(e.TotalY);
                    }

                    break;
                case GestureStatus.Completed:
                    //Snap?
                    break;
                case GestureStatus.Canceled:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}