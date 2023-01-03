using DIPS.Xamarin.UI.Controls.ContextMenu;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Samples.Controls.ContextMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContextMenuPage
    {
        public ContextMenuPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is ContextMenuPageViewModel contextMenuPageViewModel)
            {
                _ = contextMenuPageViewModel.Initialize();
            }
        }
    }
}