using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Internal
{
    public interface IGestureAware
    {
        internal void SendPan(float totalX, float totalY, float distanceX, float distanceY, GestureStatus status, int id);
        internal void SendTapped(float x, float y);
    }
}