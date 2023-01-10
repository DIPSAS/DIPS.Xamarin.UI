using System.Threading.Tasks;
using Android.Content;
using Android.OS;
using Android.Views;
using DIPS.Xamarin.UI.BottomSheet;
using DIPS.Xamarin.UI.Controls;
using Google.Android.Material.BottomSheet;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace DIPS.Xamarin.UI.Android.BottomSheet
{
    internal class AndroidBottomSheet : IBottomSheet
    {
        private readonly Context m_context;

        public AndroidBottomSheet(Context context)
        {
            m_context = context;
        }

        public Task PushBottomSheet(ContentPage contentPage)
        {
            var modalBottomSheet = new ModalBottomSheet(m_context, contentPage);
            modalBottomSheet.Show(m_context.GetFragmentManager(), nameof(ModalBottomSheet));
            return Task.CompletedTask;
        }
    }

    internal class ModalBottomSheet : BottomSheetDialogFragment
    {
        private readonly Context m_context;
        private readonly ContentPage m_contentPage;

        public ModalBottomSheet(Context context, ContentPage contentPage)
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