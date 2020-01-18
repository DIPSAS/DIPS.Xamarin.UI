using System;
using System.ComponentModel;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Samples.Controls.Sheet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SheetPage : ContentPage
    {
        private double y;

        public SheetPage()
        {
            InitializeComponent();
        }

        private async void PanGestureRecognizer_OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            var TranslationX = OuterSheetFrame.TranslationX;
            var TranslationY = OuterSheetFrame.TranslationY;

            var TotalX_Modified = e.TotalX + TranslationX;
            var TotalY_Modified = e.TotalY + TranslationY;

            if (Device.RuntimePlatform == Device.Android)
            {
                e = new PanUpdatedEventArgs(e.StatusType, e.GestureId, TotalX_Modified, TotalY_Modified);
            }

            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    y = OuterSheetFrame.TranslationY;
                    break;
                case GestureStatus.Running:
                    var translateY = y + e.TotalY;
                    await OuterSheetFrame.TranslateTo(OuterSheetFrame.X, translateY, 20);

                    break;
                case GestureStatus.Completed:
                    y = OuterSheetFrame.TranslationY;
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

    public class SheetPageViewModel : INotifyPropertyChanged
    {
        private bool m_isFirstSheetOpen;

        public bool IsFirstSheetOpen
        {
            get => m_isFirstSheetOpen;
            set => PropertyChanged.RaiseWhenSet(ref m_isFirstSheetOpen, value);
        }

        public Func<object> SheetViewModelFactory => () => new InsideSheetViewModel();

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class InsideSheetViewModel : INotifyPropertyChanged
    {
        public string Title => "Sheet Title";

        public event PropertyChangedEventHandler PropertyChanged;
    }
}