using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.Forms.IssuesRepro.NewIssue
{
    [Issue(137)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Github137 : ContentPage
    {
        public Github137()
        {
            InitializeComponent();
        }
    }
}