using DIPS.Xamarin.UI.Android.Renderers.DatePicker;

namespace DIPS.Xamarin.UI.Android
{
    /// <summary>
    /// A static class to use when having to interact with the library on Android platform
    /// </summary>
    public static class Library
    {

        public static void Initialize()
        {
            DatePickerImplementation.Initialize();
        }
    }
}