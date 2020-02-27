using System;
using System.Diagnostics;

namespace DIPS.Xamarin.UI.Util
{
    /// <summary>
    /// Used to track time.
    /// </summary>
    internal class TimeTracker
    {
        private Stopwatch m_stopwatch = Stopwatch.StartNew();
        private long m_lastTime;
        private bool m_hasLastTime;

        /// <summary>
        /// Gets the time in seconds since the last measure, in float.
        /// </summary>
        /// <returns></returns>
        public float GetTimeF() => GetTime() / 1000.0f;

        /// <summary>
        /// Gets the time in seconds since the last measure, in double.
        /// </summary>
        /// <returns></returns>
        public double GetTimeD() => GetTime() / 1000.0d;

        private long GetTime()
        {
            var time = m_stopwatch.ElapsedMilliseconds;
            if (!m_hasLastTime)
            {
                m_lastTime = time;
                m_hasLastTime = true;
            }

            var dt = time - m_lastTime;
            m_lastTime = time;
            return dt;
        }

        /// <summary>
        /// Resets the time, so that the next GetTime will return 0.
        /// </summary>
        public void Reset()
        {
            m_hasLastTime = false;
        }
    }
}