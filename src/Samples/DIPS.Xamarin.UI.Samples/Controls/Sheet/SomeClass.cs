using System;
using System.ComponentModel;
using System.Windows.Input;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Samples.Controls.Sheet
{
    public class SomeClass : INotifyPropertyChanged
    {
        private string m_text;
        private readonly Random m_random;

        public SomeClass()
        {
            m_random = new Random();
        }
        
        internal string CreateString(int stringLength)
        {
            const string allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789!@$?_-";
            var chars = new char[stringLength];

            for (var i = 0; i < stringLength; i++) chars[i] = allowedChars[m_random.Next(0, allowedChars.Length)];

            return new string(chars);
        }
        
        public string Text
        {
            get => m_text;
            set => PropertyChanged.RaiseWhenSet(ref m_text, value);
        }

        public ICommand ClickedCommand => new Command<SomeClass>(Execute);

        public string Title => Summary();

        private string Summary()
        {
            return CreateString(m_random.Next(5,50));
        }

        private void Execute(SomeClass someClass)
        {
            someClass.Text = "CLICKED!";
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}