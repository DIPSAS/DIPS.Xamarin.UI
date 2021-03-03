using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Internal.Xaml
{
    /// <summary>
    ///     Toast container that act as a wrapper to the presented view
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToastContainer : Grid
    {
        public ToastContainer()
        {
            InitializeComponent();
        }
    }
}