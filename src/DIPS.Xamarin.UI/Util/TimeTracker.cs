using System;
using System.Diagnostics;

namespace DIPS.Xamarin.UI.Util
{
    internal class TimeTracker
    {
        private Stopwatch m_stopwatch = Stopwatch.StartNew();
        private long m_lastTime;
        private bool m_hasLastTime;
        public float GetTimeF() => GetTime() / 1000.0f;

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

        public void Reset()
        {
            m_hasLastTime = false;
        }
    }
}