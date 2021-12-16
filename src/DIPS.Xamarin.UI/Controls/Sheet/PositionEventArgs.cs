using System;

namespace DIPS.Xamarin.UI.Controls.Sheet
{
    /// <summary>
    ///     Event args that will be sent when the position changes
    /// </summary>
    public class PositionEventArgs : EventArgs
    {
        /// <inheritdoc />
        public PositionEventArgs(double newPosition, double oldPosition)
        {
            NewPosition = newPosition;
            OldPosition = oldPosition;
        }

        /// <summary>
        ///     The new position when the sheet changed it's position
        /// </summary>
        public double NewPosition { get; }

        /// <summary>
        ///     The old position when the sheet changed it's position
        /// </summary>
        public double OldPosition { get; }
    }
}