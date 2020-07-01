using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Samples.Converters.MultiValueConverters
{
    public partial class PositionInListConverterPage : ContentPage
    {
        public PositionInListConverterPage()
        {
            InitializeComponent();
        }

   
    }

    public class PositionInListConverterPageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<string> m_items;

        public PositionInListConverterPageViewModel()
        {
            Items = new ObservableCollection<string>();
            AddItemCommand = new Command(AddItem);
            RemoveItemCommand = new Command(RemoveItem, () => Items.Any());
        }

        private void RemoveItem()
        {
            Items.Remove(Items.Last());
            ((Command)RemoveItemCommand).ChangeCanExecute();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<string> Items 
        { 
            get => m_items;
            set => PropertyChanged.RaiseWhenSet(ref m_items, value);
        }
        public ICommand AddItemCommand { get; }
        public ICommand RemoveItemCommand { get; }
        private void AddItem()
        {
            var newList = new ObservableCollection<string>(Items);
            newList.Add($"Item {newList.Count}");
            Items = newList;

            ((Command)RemoveItemCommand).ChangeCanExecute();
        }
    }
}
