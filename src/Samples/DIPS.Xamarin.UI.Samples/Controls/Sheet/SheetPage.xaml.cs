using System;
using System.ComponentModel;
using System.Windows.Input;
using DIPS.Xamarin.UI.Controls.Sheet;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Samples.Controls.Sheet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SheetPage : ContentPage
    {

        public SheetPage()
        {
            InitializeComponent();
        }

        private void SheetBehavior_OnOnOpen(object sender, EventArgs e)
        {
            if (!(sender is SheetBehavior sheetBehavior)) return;
            var label = new Label() { };
            label.SetBinding(Label.TextProperty, "Title");
            sheetBehavior.SheetContent = label;
        }

        private void SheetBehavior_OnOnClose(object sender, EventArgs e)
        {
            if (!(sender is SheetBehavior sheetBehavior)) return;
            sheetBehavior.SheetContent = null;
        }
    }

    public class SheetPageViewModel : INotifyPropertyChanged
    {
        private AlignmentOptions m_alignment;
        private string m_backgroundColor;
        private string m_handleColor;
        private bool m_hasShadow;
        private bool m_isDraggable;
        private bool m_isSheetOpen;
        private ContentAlignment m_verticalContentAlignment;
        private double m_position;
        private double m_maxPosition = 1;
        private double m_minPosition = 0.1;
        private string m_stateText;

        public SheetPageViewModel()
        {
            OpenSheetCommand = new Command(() => IsSheetOpen = true);
            OnOpenCommand = new Command<string>(SheetOpened);
            OnCloseCommand = new Command<string>(SheetClosed);
        }

        private void SheetClosed(string commandParameter)
        {
            //This is when the sheet has finished it's animation and is closed
            StateText = commandParameter;
        }

        private void SheetOpened(string commandParameter)
        {
            //This is when the sheet has finished it's animation and is open
            StateText = commandParameter;
        }

        public AlignmentOptions Alignment
        {
            get => m_alignment;
            set => PropertyChanged.RaiseWhenSet(ref m_alignment, value);
        }

        public string BackgroundColor
        {
            get => m_backgroundColor;
            set
            {
                try
                {
                    new ColorTypeConverter().ConvertFromInvariantString(value);
                    m_backgroundColor = value;
                    PropertyChanged.Raise();
                }
                catch (Exception e)
                {
                    //Swallow it.
                }
            }
        }

        public string HandleColor
        {
            get => m_handleColor;
            set
                {
                    try
                    {
                        new ColorTypeConverter().ConvertFromInvariantString(value);
                        m_handleColor = value;
                        PropertyChanged.Raise();
                    }
                    catch (Exception e)
                    {
                        //Swallow it.
                    }
                }
                
            }

        public bool HasShadow
        {
            get => m_hasShadow;
            set => PropertyChanged.RaiseWhenSet(ref m_hasShadow, value);
        }

        public bool IsDraggable
        {
            get => m_isDraggable;
            set => PropertyChanged.RaiseWhenSet(ref m_isDraggable, value);
        }

        public bool IsSheetOpen
        {
            get => m_isSheetOpen;
            set => PropertyChanged.RaiseWhenSet(ref m_isSheetOpen, value);
        }


        public double Position
        {
            get => m_position;
            set => PropertyChanged.RaiseWhenSet(ref m_position, value);
        }

        public double MaxPosition
        {
            get => m_maxPosition;
            set => PropertyChanged.RaiseWhenSet(ref m_maxPosition, value);
        }

        public double MinPosition
        {
            get => m_minPosition;
            set => PropertyChanged.RaiseWhenSet(ref m_minPosition, value);
        }

        public Func<object> SheetViewModelFactory => () => new InsideSheetViewModel();

        public ContentAlignment VerticalContentAlignment
        {
            get => m_verticalContentAlignment;
            set => PropertyChanged.RaiseWhenSet(ref m_verticalContentAlignment, value);
        }

        public ICommand OpenSheetCommand { get; }

        public ICommand OnOpenCommand { get; }

        public ICommand OnCloseCommand { get; }

        public string StateText
        {
            get => m_stateText;
            set => PropertyChanged.RaiseWhenSet(ref m_stateText, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class InsideSheetViewModel : INotifyPropertyChanged
    {
        public string Title => "Sheet Title";

        public event PropertyChangedEventHandler PropertyChanged;
    }
}