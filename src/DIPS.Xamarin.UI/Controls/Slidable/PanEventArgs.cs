using System;

namespace DIPS.Xamarin.UI.Controls.Slidable
{
    public class PanEventArgs : EventArgs
    {
        /// <summary>
        /// Current index when pan event occurs.
        /// </summary>
        public int CurrentIndex { get; }

        /// <summary>
        /// Used in <see cref="SlidableLayout.PanEnded"/> and <see cref="SlidableLayout.PanStarted"/> 
        /// </summary>
        /// <param name="currentIndex"></param>
        public PanEventArgs(int currentIndex)
        {
            CurrentIndex = currentIndex;
        }
    }
}