using System;
using System.Diagnostics;
using System.Net.Http.Headers;
using DIPS.Xamarin.UI.Controls.Modality;
using DIPS.Xamarin.UI.Extensions;
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

        private void PanGestureRecognizer_OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            // Handle the pan
            
        }

        public Frame SheetFrame => OuterSheetFrame;
    }


}