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
        private double m_previousPosition;

        public SheetPage()
        {
            InitializeComponent();
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