using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using DIPS.Xamarin.UI.BottomSheet;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Samples.Controls.NativeSheet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NativeSheetPage
    {
        public NativeSheetPage()
        {
            InitializeComponent();
        }

        private void OpenSheet(object sender, EventArgs e)
        {
            App.Current.PushBottomSheet(new TheSheetPage());
        }
    }
}