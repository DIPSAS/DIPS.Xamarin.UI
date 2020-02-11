using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Samples.Controls.FloatingActionMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FloatingActionMenuPage : ContentPage
    {
        public FloatingActionMenuPage()
        {
            InitializeComponent();
        }

        
    }

    public class FloatingActionMenuPageViewmodel : INotifyPropertyChanged
    {
        private string m_text;

        public FloatingActionMenuPageViewmodel()
        {
            SetTextCommand = new Command<string>((text) => Text = text);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand SetTextCommand { get; }

        public string Text
        {
            get => m_text;
            set => PropertyChanged.RaiseWhenSet(ref m_text, value);
        }
    }
}