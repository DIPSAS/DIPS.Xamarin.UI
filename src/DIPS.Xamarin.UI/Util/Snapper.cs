using System;
namespace DIPS.Xamarin.UI.Util
{
    /// <summary>
    /// Helps in finding the closest target to snap to.
    /// </summary>
    internal class Snapper
    {
        private readonly bool m_snapWholeNumbers;
        private readonly double[]? m_points = null;

        /// <summary>
        /// Creates an instance which snaps to whole numbers.
        /// </summary>
        public Snapper()
        {
            m_snapWholeNumbers = true;
        }

        /// <summary>
        /// Creates an instance which snaps to certain selected points.
        /// </summary>
        /// <param name="points"></param>
        public Snapper(double[] points)
        {
            m_points = points;
            if (m_points.Length == 0) throw new ArgumentException($"{nameof(points)} can't be empty.");
        }

        /// <summary>
        /// Gets the closest snap point in a given time.
        /// </summary>
        /// <param name="currentValue"></param>
        /// <returns></returns>
        public double GetSnapPoint(double currentValue)
        {
            if (m_snapWholeNumbers || m_points == null)
            {
                return Math.Round(currentValue);
            }

            var bestDiff = Math.Abs(m_points[0] - currentValue);
            var bestValue = m_points[0];
            for (var i = 1; i < m_points.Length; i++)
            {
                var diff = Math.Abs(m_points[i] - currentValue);
                if (diff < bestDiff)
                {
                    bestDiff = diff;
                    bestValue = m_points[i];
                }
            }

            return bestValue;
        }
    }
}
