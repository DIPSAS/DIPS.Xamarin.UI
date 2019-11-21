using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Extensions
{
    /// <summary>
    /// Extensions for visual elements
    /// </summary>
    public static class VisualElementExtensions
    {
        /// <summary>
        /// Gets the parent of a certain type or returns null if no such parent is found above this item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
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
