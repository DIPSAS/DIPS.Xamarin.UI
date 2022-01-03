using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
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

        private void MoveSheet(object sender, EventArgs e)
        {
            SheetBehavior.MoveTo(0.7);
        }
    }

    public class SheetPageViewModel : INotifyPropertyChanged
    {
        private string m_contentColor;
        private string m_handleColor;
        private bool m_hasActionButton;
        private string m_headerColor;
        private bool m_isDraggable = true;
        private bool m_isSheetOpen;
        private double m_position;
        private string m_stateText;
        private string m_title = "Title";
        private ContentAlignment m_verticalContentAlignment;
        private readonly InsideSheetViewModel m_sheetViewModel;
        private bool m_isBusy;

        public SheetPageViewModel()
        {
            OpenSheetCommand = new Command(() => IsSheetOpen = true);
            OnOpenCommand = new Command<string>(SheetOpened);
            OnCloseCommand = new Command<string>(SheetClosed);
            CancelCommand = new CancelSheetCommand(() =>
                {
                    //Do any logic that should get executed regardless of the canCloseSheet logic.
                },
                () =>
                {
                    return true;
                }, async () =>
                {
                    //Do logic to determine if the sheet should close
                    return await Application.Current.MainPage.DisplayAlert("Are you sure?",
                        "Do you want to close the sheet view?", "Yes", "No");
                });

            ActionCommand = new Command(
                () =>
                {
                    //Do work when action is pressed
                });

            InitCommand = new Command(async () => await Init());

            m_sheetViewModel = new InsideSheetViewModel();
        }

        private async Task Init()
        {
            try
            {
                IsBusy = true;

                await Task.Delay(2000);

                IsBusy = false;
            }
            catch (Exception e)
            {
                // ignored
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

        public Func<object> SheetViewModelFactory => () => m_sheetViewModel;

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

        public string ContentColor
        {
            get => m_contentColor;
            set
            {
                try
                {
                    new ColorTypeConverter().ConvertFromInvariantString(value);
                    m_contentColor = value;
                    PropertyChanged.Raise();
                }
                catch (Exception e)
                {
                    //Swallow it.
                }
            }
        }

        public string HeaderColor
        {
            get => m_headerColor;
            set
            {
                try
                {
                    new ColorTypeConverter().ConvertFromInvariantString(value);
                    m_headerColor = value;
                    PropertyChanged.Raise();
                }
                catch (Exception e)
                {
                    //Swallow it.
                }
            }
        }

        public ICommand CancelCommand { get; }

        public ICommand ActionCommand { get; }

        public string Title
        {
            get => m_title;
            set => PropertyChanged.RaiseWhenSet(ref m_title, value);
        }

        public bool HasActionButton
        {
            get => m_hasActionButton;
            set => PropertyChanged.RaiseWhenSet(ref m_hasActionButton, value);
        }

        public ICommand InitCommand
        {
            get;
        }

        public bool IsBusy
        {
            get => m_isBusy;
            set => PropertyChanged.RaiseWhenSet(ref m_isBusy, value);
        }


        public event PropertyChangedEventHandler PropertyChanged;

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
    }

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

    public class SomeClass
    {
        public string Text { get; set; }

        public ICommand ClickedCommand => new Command(Execute);

        private void Execute()
         {
        }
    }
}