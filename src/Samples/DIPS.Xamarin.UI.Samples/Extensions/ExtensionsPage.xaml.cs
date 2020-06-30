using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Samples.Extensions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExtensionsPage : ContentPage
    {
        public ExtensionsPage()
        {
            InitializeComponent();
            BindingContext = new ExtensionsViewModel(Application.Current.MainPage.Navigation);
        }
    }
}