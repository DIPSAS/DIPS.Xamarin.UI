using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Samples.Controls.NativeSheet
{
    public class TheSheetPageViewModel : INotifyPropertyChanged
    {
        private string m_labelText = "Test1";

        public TheSheetPageViewModel()
        {
            ChangeLabelCommand = new Command(() =>
            {
                LabelText = (m_labelText.Equals("Test1", StringComparison.InvariantCultureIgnoreCase)
                    ? "Test2"
                    : "Test1");
            });

            for (var i = 0; i < 100; i++)
            {
                Items.Add($"Item {i}");
            }
        }

        public string LabelText
        {
            get => m_labelText;
            set => PropertyChanged?.RaiseWhenSet(ref m_labelText, value);
        }

        public ICommand ChangeLabelCommand { get; }
        public IList<string> Items { get; } = new List<string>();

        public event PropertyChangedEventHandler PropertyChanged;
    }
}