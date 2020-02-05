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

namespace DIPS.Xamarin.Forms.IssuesRepro.Github123
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Github123 : ContentPage
    {
        public Github123()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (!(BindingContext is Github123ViewModel viewmodel)) return;
            viewmodel.Initialize();
        }
    }

    public class Github123ViewModel : INotifyPropertyChanged
    {
        private DateTime? m_dateTimeValue;
        public event PropertyChangedEventHandler PropertyChanged;

        public void Initialize()
        {
            DateTimeValue = null;
        }

        public DateTime? DateTimeValue
        {
            get => m_dateTimeValue;
            set => PropertyChanged.RaiseWhenSet(ref m_dateTimeValue, value);
        }
    }
}