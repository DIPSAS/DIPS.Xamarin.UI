using System;
using Android.Content;
using Android.Views;
using DIPS.Xamarin.UI.Android;
using DIPS.Xamarin.UI.Controls.Sheet.CheatSheet;
using DIPS.Xamarin.UI.Controls.Slidable;
using DIPS.Xamarin.UI.Internal;
using DIPS.Xamarin.UI.Internal.xaml;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Xamarin.Forms.View;

[assembly: ExportRenderer(typeof(SlidableLayout), typeof(SlidableLayoutContentView))]
[assembly: ExportRenderer(typeof(CheatSheet), typeof(SlidableLayoutContentView))]
[assembly: ExportRenderer(typeof(SheetView), typeof(SlidableLayoutContentView))]

namespace DIPS.Xamarin.UI.Android
{
    internal class SlidableLayoutContentView : ViewRenderer, GestureDetector.IOnGestureListener
    {
        private readonly float m_density;
        private readonly GestureDetector m_detector;
        private readonly Random m_random;
        private readonly int m_scaledTouchSlop = 1;
        private IGestureAware m_elem;
        private bool m_isScrolling;
        private int m_pointerId;
        private (float x, float y) m_prev;
        private float m_startX, m_startY;

        public SlidableLayoutContentView(Context context) : base(context)
        {
            m_random = new Random();
            m_detector = new GestureDetector(context, this);
            m_density = context.Resources.DisplayMetrics.Density;
        }

        public bool OnDown(MotionEvent e)
        {
            return true;
        }

        public bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
        {
            return false;
        }

        public void OnLongPress(MotionEvent e)
        {
        }

        public bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
        {
            if (m_isScrolling)
            {
                var (x, y) = ToDip(e2.RawX, e2.RawY);
                m_elem?.SendPan(GetTotal(e1.RawX, e2.RawX), GetTotal(e1.RawY, e2.RawY), GetDelta(x, m_prev.x),
                    GetDelta(y, m_prev.y), GestureStatus.Running, m_pointerId);
                m_prev = (x, y);
            }

            return true;
        }

        public void OnShowPress(MotionEvent e)
        {
        }

        public bool OnSingleTapUp(MotionEvent e)
        {
            return false;
        }

        private float GetTotal(float start, float end)
        {
            return end - start;
        }

        private static float GetDelta(float x, float prev)
        {
            return prev == 0 ? 2 : x - prev;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement is IGestureAware contentView)
            {
                m_elem = contentView;
            }
        }

        private bool StartScroll(MotionEvent ev)
        {
            if (ev.HistorySize < 1)
            {
                return false;
            }

            var historicalX = ev.GetHistoricalX(0);
            if (Math.Abs(historicalX - ev.GetX()) >
                m_scaledTouchSlop) // Increase value if we require a longer drag before scrolling starts.
            {
                m_isScrolling = true;
                m_elem?.SendPan(0, 0, 0, 0, GestureStatus.Started, m_pointerId);
                return true;
            }

            return false;
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            var action = ev.ActionMasked;

            var (x, y) = ToDip(ev.RawX, ev.RawY);

            switch (action)
            {
                case MotionEventActions.Up:
                    if (!m_isScrolling)
                    {
                        m_elem.SendTapped(x, y);
                    }

                    break;
                case MotionEventActions.Move:
                    return m_isScrolling || StartScroll(ev);
                case MotionEventActions.Down
                    : // This case is the only case that is always intercepted no matter the view hierarchy. (I think) 
                    m_startX = x;
                    m_startY = y;
                    m_pointerId = m_random.Next();
                    return false;
            }

            return false;
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if (e.ActionMasked == MotionEventActions.Up || e.ActionMasked == MotionEventActions.Cancel)
            {
                var (x, y) = ToDip(e.RawX, e.RawY);

                if (m_isScrolling) // ended scroll
                {
                    m_elem?.SendPan(GetTotal(m_startX, x), GetTotal(m_startY, y), GetDelta(x, m_prev.x),
                        GetDelta(y, m_prev.y), GestureStatus.Completed, m_pointerId);
                    m_prev = (0, 0); // reset
                }
                else
                {
                    m_elem.SendTapped(x, y);
                }

                m_isScrolling = false;
                return false;
            }

            if (!m_isScrolling &&
                e.ActionMasked ==
                MotionEventActions
                    .Move) // Is needed when this does not contain any children that can handle touch events. 
            {
                StartScroll(e);
            }

            return m_detector.OnTouchEvent(e);
        }

        private (float, float) ToDip(float rawX, float rawY)
        {
            return (rawX / m_density, rawY / m_density);
        }
    }
}