using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Internal
{
    internal interface IPanAware
    {
        public void SendPan(float totalX, float totalY, float distanceX, float distanceY, GestureStatus status, int id);
        
        bool ShouldInterceptScroll { get; }
    }
}