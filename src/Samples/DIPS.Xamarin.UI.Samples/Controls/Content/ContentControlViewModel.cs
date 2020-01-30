using System;
using System.ComponentModel;
using System.Windows.Input;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Samples.Controls.Content
{
    public class ContentControlViewModel : INotifyPropertyChanged
    {
        private object content = new ViewModel1();

        public ContentControlViewModel()
        {
            SwapCommand = new Command(() =>
            {
                if (content is ViewModel1) Content = new ViewModel2();
                else Content = new ViewModel1();
            });
        }

        public ICommand SwapCommand { get; }
        public object Content { get => content; set => PropertyChanged.RaiseWhenSet(ref content, value); }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
