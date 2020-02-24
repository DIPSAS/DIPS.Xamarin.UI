using System;
namespace DIPS.Xamarin.UI.Controls.Slidable
{
    /// <summary>
    /// Inherit this to get information about selection changes. You have to keep the state yourself if you are doing heavy work.
    /// As this is called every tick of movement.
    /// </summary>
    public interface ISliderSelectable
    {
        /// <summary>
        /// Invoked every tick of the SlideLayout moving.
        /// </summary>
        /// <param name="selected">True if this item is selected</param>
        void OnSelectionChanged(bool selected);
    }
}
