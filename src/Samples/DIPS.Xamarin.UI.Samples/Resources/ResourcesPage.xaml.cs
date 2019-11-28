using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Samples.Resources
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResourcesPage : ContentPage
    {
        public ResourcesPage()
        {
            InitializeComponent();
            BindingContext = new ResourcesPageViewModel(Application.Current.MainPage.Navigation);
        }
    }
}