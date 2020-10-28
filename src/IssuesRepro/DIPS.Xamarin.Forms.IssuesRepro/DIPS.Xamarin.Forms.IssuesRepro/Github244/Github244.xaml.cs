using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.Forms.IssuesRepro.Github244
{
    [Issue(244)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Github244 : ContentPage
    {
        public Github244()
        {
            InitializeComponent();
        }
    }

    public class Github244ViewModel
    {
        public List<string> Items => new List<string>() { "item 1", "item 2", "item 3" };
    }
}