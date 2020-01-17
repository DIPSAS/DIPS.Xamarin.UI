using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Samples.Controls.Sheet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SheetPage : ContentPage
    {
        public SheetPage()
        {
            InitializeComponent();
        }
    }

    public class SheetPageViewModel : INotifyPropertyChanged
    {
        private bool m_isChecked;

        public bool IsChecked
        {
            get => m_isChecked;
            set => PropertyChanged.RaiseWhenSet(ref m_isChecked, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}