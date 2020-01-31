using System;
namespace DIPS.Xamarin.UI.Util
{
    internal class Snapper
    {
        private readonly bool m_snapWholeNumbers;
        private readonly double[]? m_points = null;

        public Snapper()
        {
            m_snapWholeNumbers = true;
        }

        public Snapper(double[] points)
        {
            m_points = points;
            if (m_points.Length == 0) throw new ArgumentException($"{nameof(points)} can't be empty.");
        }

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
