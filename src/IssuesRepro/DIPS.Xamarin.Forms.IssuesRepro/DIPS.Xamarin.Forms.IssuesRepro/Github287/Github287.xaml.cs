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
        private bool m_isSheetAOpen;
        private bool m_isSheetBOpen;
        private bool m_isCloseEnabled;

        public Github287()
        {
            InitializeComponent();
            IsCloseEnabled = true;
        }

        public bool IsSheetAOpen
        {
            get => m_isSheetAOpen;
            set
            {
                m_isSheetAOpen = value;
                OnPropertyChanged(nameof(IsSheetAOpen));
            }
        }

        public bool IsSheetBOpen
        {
            get => m_isSheetBOpen;
            set
            {
                m_isSheetBOpen = value;
                OnPropertyChanged(nameof(IsSheetBOpen));
            }
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

        private void MenuItemA_OnClicked(object sender, EventArgs e)
        {
            IsSheetAOpen = true;
        }
        
        private void MenuItemB_OnClicked(object sender, EventArgs e)
        {
            IsSheetAOpen = !IsSheetAOpen;
        }
    }
}