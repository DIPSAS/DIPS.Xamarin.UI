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
        private bool m_attachBadge;
        private int m_badgeCounter;
        private Color m_badgeColor;

        public FloatingActionMenuPageViewmodel()
        {
            SetTextCommand = new Command<string>((text) => Text = text);
            FlipBadgeCommand = new Command(() => AttachBadge = !AttachBadge);
            IncreaseCounterCommand = new Command(() => BadgeCounter++);
            ChangeBadgeColorCommand = new Command(() =>
            {
                if ( BadgeColor != Color.Aqua )BadgeColor = Color.Aqua;
                else BadgeColor = Color.Red;
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand SetTextCommand { get; }
        public ICommand FlipBadgeCommand { get; set; }
        public ICommand ChangeBadgeColorCommand { get; set; }
        public ICommand IncreaseCounterCommand { get; set; }

        public Color BadgeColor
        {
            get => m_badgeColor;
            set => PropertyChanged.RaiseWhenSet(ref m_badgeColor, value);
        }

        public int BadgeCounter
        {
            get => m_badgeCounter;
            set => PropertyChanged.RaiseWhenSet(ref m_badgeCounter, value);
        }

        public bool AttachBadge
        {
            get => m_attachBadge;
            set => PropertyChanged.RaiseWhenSet(ref m_attachBadge, value);
        }

        public string Text
        {
            get => m_text;
            set => PropertyChanged.RaiseWhenSet(ref m_text, value);
        }
    }
}