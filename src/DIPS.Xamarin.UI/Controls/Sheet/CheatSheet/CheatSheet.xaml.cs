using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DIPS.Xamarin.UI.Internal;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.Sheet.CheatSheet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheatSheet : IGestureAware
    {
        private double m_endY, m_startY;
        private readonly float[] m_latestDeltas = new float[5];
        private bool m_isOpen;
        private Stopwatch m_watch;
        private readonly object m_lock = new object();
        private int m_recordCount = 0;

        public CheatSheet()
        {
            InitializeComponent();
        }

        [TypeConverter(typeof(DoubleCollectionConverter))]
        public IList<double> SnapPoints { get; set; } = new List<double> {.25, 1.0};

        public float PixelsPerSecond { get; set; } = 1000;

        void IGestureAware.SendPan(float totalX, float totalY, float distanceX, float distanceY, GestureStatus status, int id)
        {
            switch (status)
            {
                case GestureStatus.Started:
                    OnPanStarted();
                    break;
                case GestureStatus.Running:
                    MoveSheet(distanceY);
                    RecordDelta(distanceY);
                    break;
                case GestureStatus.Completed:
                    MoveSheet(distanceY);
                    OnPanEnded();
                    break;
                case GestureStatus.Canceled:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }

        private void RecordDelta(float distanceY)
        {
            lock (m_lock)
            {
                if (m_recordCount == m_latestDeltas.Length)
                {
                    m_recordCount = 0;
                }
                
                m_latestDeltas[m_recordCount] = distanceY;
                m_recordCount++;
            }
        }

        void IGestureAware.SendTapped(float x, float y)
        {
        }

        private void MoveSheet(float distanceY)
        {
            if (ShouldOpen(distanceY))
            {
                Open();
                return;
            }

            if (ShouldClose(distanceY))
            {
                Close();
                return;
            }

            TranslationY += distanceY;
        }

        private void Open()
        {
            this.TranslateTo(0, 0, 300, Easing.CubicOut);
            m_isOpen = true;
        }

        private void Close()
        {
            this.TranslateTo(0, Height, 175, Easing.CubicOut);
            m_isOpen = false;
        }

        private bool ShouldClose(float distanceY)
        {
            return TranslationY + distanceY >= Height;
        }

        private bool ShouldOpen(float distanceY)
        {
            return TranslationY + distanceY <= 0;
        }

        public void Init()
        {
            TranslationY = Height; // hide by default
        }

        public void Toggle()
        {
            this.TranslateTo(0, m_isOpen ? Height : Height - (SnapPoints.FirstOrDefault() * Height), 150, Easing.CubicOut);
            m_isOpen = !m_isOpen;
        }

        private void OnPanStarted()
        {
            m_watch = Stopwatch.StartNew();
            m_startY = TranslationY;
        }

        private void OnPanEnded()
        {
            m_endY = TranslationY;
            m_watch.Stop();

            var direction = FindLatestPanDirection();

            if (TryFling(direction))
            {
                return;
            }

            SnapTo(direction);
        }

        private void SnapTo(PanDirection panDirection)
        {
            var positionRatio = 1 - (TranslationY / Height);

            switch (panDirection)
            {
                case PanDirection.Up:
                    for (var i = 0; i < SnapPoints.Count; i++)
                    {
                        if (SnapPoints[i] <= positionRatio)
                        {
                            continue;
                        }

                        TranslateSheet(RatioToYValue(SnapPoints[i]));
                        return;
                    }

                    Open();
                    break;
                case PanDirection.Down:
                    for (var i = SnapPoints.Count - 1; i >= 0; i--)
                    {
                        if (SnapPoints[i] >= positionRatio)
                        {
                            continue;
                        }

                        TranslateSheet(RatioToYValue(SnapPoints[i]));
                        return;
                    }

                    Close();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(panDirection), panDirection, null);
            }
        }

        private void TranslateSheet(float y)
        {
            this.TranslateTo(0, y, 250, Easing.CubicOut);
        }

        private float RatioToYValue(double ratio)
        {
            var pos = ratio * Height;
            var y = Height - pos;
            return (float) y;
        }

        private PanDirection FindLatestPanDirection()
        {
            lock (m_lock)
            {
                var direction = m_latestDeltas.Sum(); // sum of latest movements to get the general/most likely intended direction
                return direction < 0 ? PanDirection.Up : PanDirection.Down;
            }
        }

        private bool TryFling(PanDirection panDirection)
        {
            var length = Math.Abs(m_startY - m_endY);
            var duration = m_watch.Elapsed.TotalSeconds;

            if (length / duration >= PixelsPerSecond) 
            {
                switch (panDirection)
                {
                    case PanDirection.Up:
                        Open();
                        break;
                    case PanDirection.Down:
                        Close();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(panDirection), panDirection, null);
                }

                return true;
            }

            return false;
        }
    }

    internal enum PanDirection
    {
        Up,
        Down
    }
}