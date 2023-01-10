using System.Threading.Tasks;
using Android.Content;
using DIPS.Xamarin.UI.BottomSheet;
using DIPS.Xamarin.UI.Controls;
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
            var modalBottomSheet = new BottomSheetFragment(m_context, contentPage);
            modalBottomSheet.Show(m_context.GetFragmentManager(), nameof(BottomSheetFragment));
            return Task.CompletedTask;
        }
    }
}