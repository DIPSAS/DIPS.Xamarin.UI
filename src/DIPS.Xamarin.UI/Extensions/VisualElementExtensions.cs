using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Extensions
{
    public static class VisualElementExtensions
    {
        public static T? GetParentOfType<T>(this VisualElement element) where T : class
        {
            if (element != null)
            {
                var parent = element.Parent;
                while (parent != null)
                {
                    if (parent is T popupLayout)
                    {
                        return popupLayout;
                    }

                    parent = parent.Parent;
                }
            }

            return default;
        }
    }
}
