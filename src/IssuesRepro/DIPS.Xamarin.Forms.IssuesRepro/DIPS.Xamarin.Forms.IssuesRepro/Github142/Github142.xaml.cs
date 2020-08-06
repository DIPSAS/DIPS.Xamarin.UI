using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.Forms.IssuesRepro.Github142
{
    [Issue(142)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Github142 : ContentPage
    {
        public Github142()
        {
            InitializeComponent();
        }
    }
}