using System;

namespace DIPS.Xamarin.UI.Controls.Slidable
{
    public class PanEventArgs : EventArgs
    {
        public int CurrentIndex { get; }

        public PanEventArgs(int currentIndex)
        {
            CurrentIndex = currentIndex;
        }
    }
}