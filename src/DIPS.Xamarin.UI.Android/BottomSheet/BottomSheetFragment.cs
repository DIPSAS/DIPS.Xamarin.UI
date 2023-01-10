using Android.Content;
using Android.OS;
using Android.Views;
using Google.Android.Material.BottomSheet;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace DIPS.Xamarin.UI.Android.BottomSheet
{
    internal class BottomSheetFragment : BottomSheetDialogFragment
    {
        private readonly Context m_context;
        private readonly ContentPage m_contentPage;

        public BottomSheetFragment(Context context, ContentPage contentPage)
        {
            m_context = context;
            m_contentPage = contentPage;
        }

        public override global::Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container,
            Bundle savedInstanceState)
        {
            //TODO: Check if we can use the entire content page as view, and not just ContentPage.Content
            return new ContainerView(m_context, m_contentPage.Content);
        }

        public override void OnStart()
        {
            m_contentPage.SendAppearing();
            base.OnStart();
        }

        public override void OnDestroy()
        {
            m_contentPage.SendDisappearing();
            base.OnDestroy();
        }
    }
}