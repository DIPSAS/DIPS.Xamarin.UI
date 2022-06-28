using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using DIPS.Xamarin.UI.Controls.Sheet;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Samples.Controls.Sheet
{
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

            InsideSheetViewModel = new InsideSheetViewModel();
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

        public InsideSheetViewModel InsideSheetViewModel { get; }
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
}