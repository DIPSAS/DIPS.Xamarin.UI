using System.Threading.Tasks;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.BottomSheet
{
    public static class BottomSheetExtensions
    {
        public static async Task PushBottomSheet(this Application app, ContentPage contentPage)
        {
            if (BottomSheet.Instance != null)
            {
                await BottomSheet.Instance.PushBottomSheet(contentPage);
            }
        }
    }
}