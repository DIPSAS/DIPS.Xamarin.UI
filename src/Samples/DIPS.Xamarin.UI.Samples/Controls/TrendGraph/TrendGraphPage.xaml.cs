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
        private static readonly Random rnd = new Random();

        private ICommand m_dragToRefreshCommand;

        private bool m_isRefreshing;
        private int maxValue = 35;

        public TrendGraphPage()
        {
            InitializeComponent();
            AddGraphItemCommand = new Command(() => TrendItems5.Add(new TrendItemViewModel(rnd.NextDouble() * 100)));
            RefreshAnimationsCommand = new Command(() =>
            {
                BindingContext = null;
                BindingContext = this;
            });

            BindingContext = this;
            TrendItems.Add(new TrendItemViewModel(15));
            TrendItems.Add(new TrendItemViewModel(30));
            TrendItems.Add(new TrendItemViewModel(0));
            TrendItems5.Add(new TrendItemViewModel(rnd.NextDouble() * 100));
        }

        public ICommand AddGraphItemCommand { get; }

        public ICommand RefreshAnimationsCommand { get; }

        public ObservableCollection<TrendItemViewModel> TrendItems { get; } =
            new ObservableCollection<TrendItemViewModel>();

        public int MaxValue { get => maxValue; set => this.Set(ref maxValue, value, PropertyChanged); }

        public List<double> TrendItems2 { get; } = new List<double> {9, 7.1, 5};

        public int[] TrendItems3 { get; } = {3, 3, 3};

        public double UpperBound => 7;
        public double LowerBound => 4;

        public List<double> TrendItems4 { get; } = new List<double> {6, 8};

        public ObservableCollection<TrendItemViewModel> TrendItems5 { get; } =
            new ObservableCollection<TrendItemViewModel>();

        public new event PropertyChangedEventHandler? PropertyChanged;
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