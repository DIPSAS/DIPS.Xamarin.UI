using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DIPS.Xamarin.UI.Samples.Controls.Sheet
{
    public class InsideSheetViewModel : INotifyPropertyChanged
    {
        public string Title => "Sheet Title";
        public ObservableCollection<SomeClass> Items
        {
            get
            {
                var list = new List<SomeClass>();
                for (var i = 0; i < 50; i++)
                {
                    list.Add(new SomeClass(){Text = $"string number: {i}"});
                }

                return new ObservableCollection<SomeClass>(list);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}