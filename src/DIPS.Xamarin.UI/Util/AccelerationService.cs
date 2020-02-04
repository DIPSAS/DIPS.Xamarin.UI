using System;
using System.Collections.Generic;

namespace DIPS.Xamarin.UI.Util
{
    internal class AccelerationService
    {
        private const double DefaultFriction = 0.66, DefaultGravity = 1.0, DefaultTrackTime = 0.18, ErrorMargin = 0.01;
        private readonly TimeTracker m_timeTracker = new TimeTracker();

        private readonly Snapper? m_snapper;
        private readonly double m_friction, m_gravity, m_trackTime;
        private readonly object m_lock = new object();

        private double m_value, m_speed;
        private bool m_isDragging;
        private List<Tuple<double, double>> m_moves = new List<Tuple<double, double>>();

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

        public void StartDrag(double value)
        {
            lock (m_lock)
            {
                m_moves.Clear();
                m_timeTracker.Reset();
                AddDrag(value);
            }
        }

        public void OnDrag(double value) => AddDrag(value);

        public void EndDrag(double value)
        {
            lock (m_lock)
            {
                //AddDrag(value);
                m_isDragging = false;
                var time = 0.0;
                var dist = 0.0;
                var prev = m_moves[m_moves.Count - 1];
                for (var i = m_moves.Count-1; i >= 0 && time < DefaultTrackTime; i--)
                {
                    var move = m_moves[i];
                    var d = move.Item1;
                    var dt = move.Item2;
                    time += dt;
                    if(m_trackTime < time)
                    {
                        var diff = prev.Item1-d;
                        var speed = diff / dt;
                        var exceeding = time - m_trackTime;
                        var actualTime = dt - exceeding;
                        dist += speed * actualTime;
                        time = m_trackTime;
                        break;
                    }

                    dist += prev.Item1-d;
                    prev = move;
                }

                if(time > 0.1)
                {
                    m_speed = dist * 1000.0 / time;
                }
            }
        }

        public double GetValue(out bool isDone)
        {
            lock (m_lock)
            {
                if (m_isDragging)
                {
                    isDone = true;
                    return m_value;
                }

                var time = m_timeTracker.GetTimeD();
                if (m_snapper == null)
                {
                    ApplySpeedByTime(time);
                    ApplyFriction(time);
                    isDone = IsDoneWithoutSnap();
                    return m_value;
                }

                var snapPoint = m_snapper.GetSnapPoint(m_value);
                ApplyGravity(time, snapPoint);
                ApplySpeedByTime(time);
                ApplyFriction(time);
                isDone = IsDone(snapPoint);
                if (isDone)
                {
                    m_value = snapPoint;
                }

                return m_value;
            }
        }

        private bool IsDoneWithoutSnap() => Math.Abs(m_speed) < ErrorMargin;

        private bool IsDone(double snapPoint) => Math.Abs(snapPoint - m_value) < ErrorMargin && IsDoneWithoutSnap();

        private void ApplySpeedByTime(double time)
        {
            m_value += m_speed * time;
        }

        private void ApplyFriction(double time)
        {
            m_speed -= m_speed * m_friction * time;
        }

        private void ApplyGravity(double time, double snapPoint)
        {
            var forceSpeed = m_gravity;
            if(m_value > snapPoint)
            {
                forceSpeed *= -1;
            }

            m_speed += time * forceSpeed;
        }
    }
}
