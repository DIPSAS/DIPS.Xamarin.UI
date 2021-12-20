﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private void SheetBehavior_OnOnPositionChanged(object sender, EventArgs e)
        {
            if (!(sender is SheetBehavior sheetBehavior))
            {
                return;
            }

            sheetBehavior.Position = 0.9;
        }
    }

    public class SheetPageViewModel : INotifyPropertyChanged
    {
        private AlignmentOptions m_alignment;
        private string m_contentColor;
        private string m_handleColor;
        private bool m_hasActionButton;
        private bool m_hasShadow;
        private string m_headerColor;
        private bool m_isDraggable = true;
        private bool m_isSheetOpen;
        private double m_maxPosition = 1;
        private double m_minPosition = 0.05;
        private double m_position;

        private bool m_shouldAutoClose = true;
        private bool m_shouldRememberPosition;
        private string m_stateText;
        private string m_title = "Title";
        private ContentAlignment m_verticalContentAlignment;

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
        }

        public AlignmentOptions Alignment
        {
            get => m_alignment;
            set => PropertyChanged.RaiseWhenSet(ref m_alignment, value);
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

        public bool ShouldRememberPosition
        {
            get => m_shouldRememberPosition;
            set => PropertyChanged.RaiseWhenSet(ref m_shouldRememberPosition, value);
        }

        public bool ShouldAutoClose
        {
            get => m_shouldAutoClose;
            set => PropertyChanged.RaiseWhenSet(ref m_shouldAutoClose, value);
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