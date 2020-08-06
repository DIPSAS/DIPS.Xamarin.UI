using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.Forms.IssuesRepro.Github150
{
    [Issue(150)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Github150 : ContentPage
    {
        public Github150()
        {
            InitializeComponent();
        }
    }
}