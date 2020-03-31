using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DIPS.Xamarin.UI.Controls.FloatingActionMenu;
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

        private void CheckBox_OnCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            Library.PreviewFeatures.MenuButtonBadgeAnimation = e.Value;
        }

        private void FloatingActionMenuBehaviour_OnAfterClose(object sender, EventArgs e)
        {
            if (BindingContext is FloatingActionMenuPageViewmodel floatingActionMenuPageViewmodel) floatingActionMenuPageViewmodel.CurrentEvent = "AfterClose";
        }

        private void FloatingActionMenuBehaviour_OnAfterOpen(object sender, EventArgs e)
        {
            if (BindingContext is FloatingActionMenuPageViewmodel floatingActionMenuPageViewmodel) floatingActionMenuPageViewmodel.CurrentEvent = "AfterOpen";
        }

        private void FloatingActionMenuBehaviour_OnBeforeOpen(object sender, EventArgs e)
        {
            if (BindingContext is FloatingActionMenuPageViewmodel floatingActionMenuPageViewmodel) floatingActionMenuPageViewmodel.CurrentEvent = "BeforeOpen";
        }

        private void FloatingActionMenuBehaviour_OnBeforeClose(object sender, EventArgs e)
        {
            if (BindingContext is FloatingActionMenuPageViewmodel floatingActionMenuPageViewmodel) floatingActionMenuPageViewmodel.CurrentEvent = "BeforeClose";

        }
    }

    public class FloatingActionMenuPageViewmodel : INotifyPropertyChanged
    {
        private string m_text;
        private bool m_showBadge;
        private int m_badgeCounter;
        private Color m_badgeColor;
        private string m_currentEvent;

        public FloatingActionMenuPageViewmodel()
        {
            SetTextCommand = new Command<string>((text) => Text = text);
            FlipBadgeCommand = new Command(() => ShowBadge = !ShowBadge);
            IncreaseCounterCommand = new Command(() => BadgeCounter++);
            DecreaseCounterCommand = new Command(() => BadgeCounter--);
            ChangeBadgeColorCommand = new Command(() =>
            {
                if ( BadgeColor != Color.BlueViolet) BadgeColor = Color.BlueViolet;
                else BadgeColor = Color.IndianRed;
            });

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand SetTextCommand { get; }
        public ICommand FlipBadgeCommand { get; set; }
        public ICommand ChangeBadgeColorCommand { get; set; }
        public ICommand IncreaseCounterCommand { get; set; }
        public ICommand DecreaseCounterCommand { get; set; }

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

        public bool ShowBadge
        {
            get => m_showBadge;
            set => PropertyChanged.RaiseWhenSet(ref m_showBadge, value);
        }

        public string Text
        {
            get => m_text;
            set => PropertyChanged.RaiseWhenSet(ref m_text, value);
        }

        public string CurrentEvent
        {
            get => m_currentEvent;
            set => PropertyChanged.RaiseWhenSet(ref m_currentEvent, value);
        }

    }
}