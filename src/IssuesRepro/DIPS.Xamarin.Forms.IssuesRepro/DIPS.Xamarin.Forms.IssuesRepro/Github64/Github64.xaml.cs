using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.Forms.IssuesRepro.Github64
{
    [Issue(64)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Github64 : ContentPage
    {
        public Github64()
        {
            InitializeComponent();
        }
    }
}