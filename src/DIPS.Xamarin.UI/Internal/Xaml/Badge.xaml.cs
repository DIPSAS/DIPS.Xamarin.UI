using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Internal.Xaml
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Badge : ContentView
    {
        public Badge()
        {
            InitializeComponent();
        }
    }
}