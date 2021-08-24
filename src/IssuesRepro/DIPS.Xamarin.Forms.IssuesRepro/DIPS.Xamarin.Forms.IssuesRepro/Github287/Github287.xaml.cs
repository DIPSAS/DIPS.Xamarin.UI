using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.Forms.IssuesRepro.Github287
{
    [Issue(287)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Github287 : ContentPage
    {
        private bool m_isCloseEnabled;

        public Github287()
        {
            InitializeComponent();
            IsCloseEnabled = true;
        }

        public bool IsCloseEnabled
        {
            get => m_isCloseEnabled;
            set
            {
                m_isCloseEnabled = value;
                OnPropertyChanged(nameof(IsCloseEnabled));
            }
        }
    }
}