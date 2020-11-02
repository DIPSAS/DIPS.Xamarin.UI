using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.Forms.IssuesRepro.Github246
{
    [Issue(247)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Github246 : ContentPage
    {
        private readonly Random m_random = new Random();

        public Github246()
        {
            InitializeComponent();
            BindingContext = this;

            for (var i = 0; i < 50; i++)
            {
                var itemWithTrends = new ItemWithTrends {ItemName = $"Item {i}"};
                itemWithTrends.TrendItems.Add(new TrendItemViewModel(m_random.NextDouble()));
                itemWithTrends.TrendItems.Add(new TrendItemViewModel(m_random.NextDouble()));
                itemWithTrends.TrendItems.Add(new TrendItemViewModel(m_random.NextDouble()));
                ItemsWithTrends.Add(itemWithTrends);
            }
        }

        public ObservableCollection<ItemWithTrends> ItemsWithTrends { get; } =
            new ObservableCollection<ItemWithTrends>();
    }

    public class ItemWithTrends : INotifyPropertyChanged
    {
        public string ItemName { get; set; }

        public ObservableCollection<TrendItemViewModel> TrendItems { get; } =
            new ObservableCollection<TrendItemViewModel>();

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}