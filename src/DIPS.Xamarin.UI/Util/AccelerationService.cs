using System;
using System.Collections.Generic;

namespace DIPS.Xamarin.UI.Util
{
    /// <summary>
    /// Class to simulate acceleration. Use this in connection to a repeated invocation after touch and use GetValue
    /// </summary>
    internal class AccelerationService
    {
        private const double DefaultFriction = 2, DefaultGravity = 10.0, DefaultTrackTime = 0.18, ErrorMargin = 0.01;
        private readonly TimeTracker m_timeTracker = new TimeTracker();

        private readonly Snapper? m_snapper;
        private readonly double m_friction, m_gravity, m_trackTime;
        private readonly object m_lock = new object();

        private double m_value, m_speed;
        private bool m_isDragging;
        private List<Tuple<double, double>> m_moves = new List<Tuple<double, double>>();

        /// <summary>
        /// Maximum value of this accelerator. Will be used in by GetValue
        /// </summary>
        public double? Max { get; set; }

        /// <summary>
        /// Minimum value of this accelerator. Will be used in by GetValue
        /// </summary>
        public double? Min { get; set; }

        /// <summary>
        /// Creates an instance with whole number snaps if set to true
        /// </summary>
        /// <param name="snap"></param>
        /// <param name="friction"></param>
        /// <param name="gravity"></param>
        /// <param name="trackTime"></param>
        public AccelerationService(
            bool snap,
            double friction = DefaultFriction,
            double gravity = DefaultGravity,
            double trackTime = DefaultTrackTime)
        {
            if (snap)
            {
                m_snapper = new Snapper();
            }
            else
            {
                m_snapper = null;
            }

            m_gravity = gravity;
            m_friction = friction;
            m_trackTime = trackTime;
        }

        /// <summary>
        /// Creates an instance where snapping happends on double locations
        /// </summary>
        /// <param name="snapPoints"></param>
        /// <param name="friction"></param>
        /// <param name="gravity"></param>
        /// <param name="trackTime"></param>
        public AccelerationService(
            double[] snapPoints,
            double friction = DefaultFriction,
            double gravity = DefaultGravity,
            double trackTime = DefaultTrackTime) : this(false, friction, gravity, trackTime)
        {
            m_snapper = new Snapper(snapPoints);
        }

        private void AddDrag(double value)
        {
            lock (m_lock)
            {
                m_isDragging = true;
                m_speed = 0.0;
                m_value = value;
                m_moves.Add(new Tuple<double, double>(value, m_timeTracker.GetTimeD()));
                while(m_moves.Count > 50)
                {
                    m_moves.RemoveAt(0);
                }
            }
        }

        /// <summary>
        /// Invoke this when you start a new drag
        /// </summary>
        /// <param name="value"></param>
        public void StartDrag(double value)
        {
            lock (m_lock)
            {
                m_moves.Clear();
                m_timeTracker.Reset();
                AddDrag(value);
            }
        }

        /// <summary>
        /// Invoke every time it's dragged
        /// </summary>
        /// <param name="value"></param>
        public void OnDrag(double value) => AddDrag(value);

        /// <summary>
        /// Invoke when dragging is done. If you have a value on the last drag, invoke OnDrag first.
        /// </summary>
        /// <param name="value"></param>
        public void EndDrag()
        {
            lock (m_lock)
            {
                m_isDragging = false;
                var time = 0.0;
                var i = m_moves.Count - 1;
                for (; i >= 0; i--)
                {
                    time += m_moves[i].Item2;
                    if (m_trackTime < time) break;
                }

                if (i < 0) i = 0;
                if (time < 0.1) time = 0.1;
                m_speed = (m_moves[m_moves.Count - 1].Item1 - m_moves[i].Item1) / time;
            }
        }

        /// <summary>
        /// Called in a small loop defining your wanted FPS. 
        /// </summary>
        /// <param name="isDone">Set to true if the location is close to a snap area with a low speed. Or true if speed is low and there are no defined snap values.</param>
        /// <returns></returns>
        public double GetValue(out bool isDone)
        {
            lock (m_lock)
            {
                if (m_isDragging)
                {
                    isDone = true;
                    return m_value;
                }

                var time = Math.Min(m_timeTracker.GetTimeD(), 1.0);
                if (m_snapper == null)
                {
                    ApplyFriction(time);
                    ApplySpeedByTime(time);
                    isDone = IsDoneWithoutSnap();
                    CapValueAndSpeed();
                    return m_value;
                }

                var snapPoint = m_snapper.GetSnapPoint(m_value);
                ApplyGravity(time, snapPoint);
                ApplyFriction(time);
                ApplySpeedByTime(time);
                CapValueAndSpeed();
                isDone = IsDone(snapPoint);
                if (isDone)
                {
                    m_value = snapPoint;
                    m_speed = 0;
                }
                return m_value;
            }
        }


        private void CapValueAndSpeed()
        {
            if(Max != null && m_value < Min)
            {
                m_speed = 0;
                m_value = Min.Value;
            }

            if(Max != null && m_value > Max)
            {
                m_speed = 0;
                m_value = Max.Value;
            }
        }

        private bool IsDoneWithoutSnap() => Math.Abs(m_speed) < 1;

        private bool IsDone(double snapPoint) => Math.Abs(snapPoint - m_value) < ErrorMargin && IsDoneWithoutSnap();

        private void ApplySpeedByTime(double time)
        {
            m_value += m_speed * time;
        }

        private void ApplyFriction(double time)
        {
            var sign = Math.Sign(m_speed);
            m_speed -= Math.Max(-0.1 * sign, m_speed * m_friction * time * sign) * sign;
        }

        private void ApplyGravity(double time, double snapPoint)
        {
            if (Math.Abs(m_speed) > 5)
                return;

            var forceSpeed = m_gravity;
            if(m_value > snapPoint)
            {
                forceSpeed *= -1;
            }

            m_speed += time * forceSpeed;
        }
    }
}
