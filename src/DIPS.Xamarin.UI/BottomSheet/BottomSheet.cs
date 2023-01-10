using System.Threading.Tasks;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.BottomSheet
{
    internal static class BottomSheet
    {
        internal static IBottomSheet? Instance { get; set; }
    }
    
    public interface IBottomSheet
    {
        Task PushBottomSheet(ContentPage contentPage);
    }
}