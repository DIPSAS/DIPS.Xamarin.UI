using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Samples.Controls.TrendGraph
{
    public partial class TrendGraphPage : ContentPage, INotifyPropertyChanged
    {
        private int maxValue = 35;
        private static Random rnd = new Random();

        public TrendGraphPage()
        {
            InitializeComponent();
            AddGraphItemCommand = new Command(() => TrendItems5.Add(new TrendItemViewModel(rnd.NextDouble() * 100)));
            BindingContext = this;

            TrendItems.Add(new TrendItemViewModel(15));
            TrendItems.Add(new TrendItemViewModel(30));
            TrendItems.Add(new TrendItemViewModel(0));
            TrendItems5.Add(new TrendItemViewModel(rnd.NextDouble() * 100));
        }

        public ICommand AddGraphItemCommand { get; }

        public ObservableCollection<TrendItemViewModel> TrendItems { get; } = new ObservableCollection<TrendItemViewModel>();

        public int MaxValue { get => maxValue; set => this.Set(ref maxValue, value, PropertyChanged); }

        public List<double> TrendItems2 { get; } = new List<double> { 5, 3, 10 };

        public List<int> TrendItems3 { get; } = new List<int> { 3, 3, 3 };

        public List<double> TrendItems4 { get; } = new List<double> { 6, 8 };

        public new event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<TrendItemViewModel> TrendItems5 { get; } = new ObservableCollection<TrendItemViewModel>();
    }

    public class TrendItemViewModel : INotifyPropertyChanged
    {
        public TrendItemViewModel(double value)
        {
            Value = value;
        }

        public double Value { get; set; }
        public int IntValue => (int)Value;
        public float FloatValue => (float)Value;

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
