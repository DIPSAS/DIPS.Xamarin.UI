using System;

namespace DIPS.Xamarin.UI.Controls.Slidable
{
    /// <summary>
    ///     Used in <see cref="SlidableLayout.PanEnded" /> and <see cref="SlidableLayout.PanStarted" />
    /// </summary>
    /// <param name="currentIndex"></param>
    public class PanEventArgs : EventArgs
    {
        /// <summary>
        /// </summary>
        /// <param name="currentIndex"></param>
        public PanEventArgs(int currentIndex)
        {
            CurrentIndex = currentIndex;
        }

        /// <summary>
        ///     Current index when pan event occurs.
        /// </summary>
        public int CurrentIndex { get; }
    }
}