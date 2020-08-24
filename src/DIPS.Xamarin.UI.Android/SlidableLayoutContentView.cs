using System;
using Android.Content;
using Android.Views;
using DIPS.Xamarin.UI.Android;
using DIPS.Xamarin.UI.Controls.Slidable;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Xamarin.Forms.View;

[assembly: ExportRenderer(typeof(SlidableLayout), typeof(SlidableLayoutContentView))]

namespace DIPS.Xamarin.UI.Android
{
    internal class SlidableLayoutContentView : ViewRenderer, GestureDetector.IOnGestureListener
    {
        private readonly GestureDetector m_detector;
        private SlidableLayout m_elem;
        private bool m_isScrolling;
        private int m_pointerId;
        private readonly Random m_random;
        private readonly int m_scaledTouchSlop = 5;
        private float m_startX;

        public SlidableLayoutContentView(Context context) : base(context)
        {
            m_random = new Random();
            //m_scaledTouchSlop = ViewConfiguration.Get(context).ScaledTouchSlop; //Can be used to get device default.
            m_detector = new GestureDetector(context, this);
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
            if (m_isScrolling) m_elem?.SendPan(e2.GetX() - m_startX, 0, GestureStatus.Running, m_pointerId);
            return true;
        }

        public void OnShowPress(MotionEvent e)
        {
        }

        public bool OnSingleTapUp(MotionEvent e)
        {
            return false;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement is SlidableLayout contentView) m_elem = contentView;
        }

        private bool StartScroll(MotionEvent ev)
        {
            if (ev.HistorySize < 1) return false;
            var historicalX = ev.GetHistoricalX(0);
            if (Math.Abs(historicalX - ev.GetX()) > m_scaledTouchSlop) // Increase value if we require a longer drag before scrolling starts.
            {
                m_isScrolling = true;
                m_elem?.SendPan(0, 0, GestureStatus.Started, m_pointerId);
                return true;
            }

            return false;
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            var action = ev.ActionMasked;

            switch (action)
            {
                case MotionEventActions.Up:
                    if (!m_isScrolling) m_elem.SendTapped(ev.RawX, ev.RawY);
                    break;
                case MotionEventActions.Move:
                    return m_isScrolling || StartScroll(ev);
                case MotionEventActions.Down: // This case is the only case that is always intercepted no matter the view hierarchy. (I think) 
                    m_startX = ev.GetX();
                    m_pointerId = m_random.Next(100000);
                    return false;
            }

            return false;
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if (e.ActionMasked == MotionEventActions.Up || e.ActionMasked == MotionEventActions.Cancel)
            {
                if (m_isScrolling) m_elem?.SendPan(e.GetX() - m_startX, 0, GestureStatus.Completed, m_pointerId);
                else m_elem.SendTapped(e.RawX, e.RawY);
                m_isScrolling = false;
                return false;
            }

            if (!m_isScrolling && e.ActionMasked == MotionEventActions.Move) // Is needed when this does not contain any children that can handle touch events. 
            {
                StartScroll(e);
            }

            return m_detector.OnTouchEvent(e);
        }
    }
}