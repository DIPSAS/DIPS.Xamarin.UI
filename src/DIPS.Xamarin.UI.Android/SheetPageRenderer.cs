using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using DIPS.Xamarin.UI.Android;
using DIPS.Xamarin.UI.Controls;
using Google.Android.Material.BottomSheet;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using FragmentContainer = AndroidX.Fragment.App.FragmentContainer;
using View = Xamarin.Forms.View;

[assembly: ExportRenderer(typeof(SheetPage), typeof(SheetPageRenderer))]
namespace DIPS.Xamarin.UI.Android
{
    public class SheetPageRenderer : PageRenderer
    {
        private SheetPage m_sheetPage;

        public SheetPageRenderer(Context context):base(context)
        {
            
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (e.NewElement is SheetPage sheetPage)
                {
                    m_sheetPage = sheetPage;
                    sheetPage.SheetOpened += SheetPageOnSheetOpened;
                } 
            }
            else
            {
                m_sheetPage.SheetOpened -= SheetPageOnSheetOpened;
            }
        }

        private void SheetPageOnSheetOpened(object sender, EventArgs e)
        {
            var modalBottomSheet = new ModalBottomSheet(Context, m_sheetPage);
            modalBottomSheet.Show(Context.GetFragmentManager(),nameof(ModalBottomSheet));
        }
    }

    internal class ModalBottomSheet : BottomSheetDialogFragment
    {
        private readonly Context m_context;
        private readonly SheetPage m_sheetPage;

        public ModalBottomSheet(Context context, SheetPage sheetPage)
        {
            m_context = context;
            m_sheetPage = sheetPage;
        }

        public override global::Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container,
            Bundle savedInstanceState)
        {
            return new ContainerView(m_context, m_sheetPage.m_sheetPage.Content);
        }
    }
}