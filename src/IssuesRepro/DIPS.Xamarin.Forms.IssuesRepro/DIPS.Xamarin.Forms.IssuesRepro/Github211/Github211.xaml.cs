using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;

namespace DIPS.Xamarin.Forms.IssuesRepro.Github211
{
    [Issue(211)]
    public partial class Github211 : ContentPage
    {
        public Github211()
        {
            var vm = new Issue211ViewModel();
            vm.Initialize();
            BindingContext = vm;
            InitializeComponent();
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            skeleton.MainContent = new Label { Text = "Zup?" };
        }
    }

    public class Issue211ViewModel : INotifyPropertyChanged
    {
        private static readonly Random s_rnd = new Random();
        private bool m_isLoading = false;

        public bool IsLoading { get => m_isLoading; set => PropertyChanged.RaiseWhenSet(ref m_isLoading, value); } 
        public async void Initialize()
        {
            while (true)
            {
                IsLoading = !IsLoading;
                await Task.Delay(s_rnd.Next(100, 2000));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
