﻿using System;
using Android.Content;
using Android.Views;
using DIPS.Xamarin.UI.Android;
using DIPS.Xamarin.UI.Internal;
using DIPS.Xamarin.UI.Internal.Xaml.Sheet;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Xamarin.Forms.View;

[assembly: ExportRenderer(typeof(SheetView), typeof(SheetContentView))]

namespace DIPS.Xamarin.UI.Android
{
    internal class SheetContentView : ViewRenderer, GestureDetector.IOnGestureListener
    {
        private readonly float m_density;
        private readonly GestureDetector m_detector;
        private readonly Random m_random;
        private IPanAware m_elem;
        private bool m_isScrolling;
        private int m_pointerId;
        private (float x, float y) m_prev;
        private float m_startX, m_startY;
        private (float x, float y) m_prevMoveCoordinates;

        public SheetContentView(Context context) : base(context)
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
            if (!m_isScrolling)
            {
                StartScroll(e2);
                return true;
            }

            if (m_isScrolling)
            {
                var (x, y) = ToDip(e2.RawX, e2.RawY);

                m_elem?.SendPan(GetTotal(m_startX, e2.RawX), GetTotal(m_startY, e2.RawY), GetDelta(x, m_prev.x), GetDelta(y, m_prev.y), GestureStatus.Running, m_pointerId);

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

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement is IPanAware contentView)
            {
                m_elem = contentView;
            }
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            switch (ev.ActionMasked)
            {
                case MotionEventActions.Cancel:
                case MotionEventActions.Up:
                    Reset();
                    break;
                case MotionEventActions.Move:
                    if (Math.Abs(ev.RawY - m_prevMoveCoordinates.y) < 2) // on certain devices a tap event will contain move events without any movement, MUST let the child handle the tap event
                    {
                        m_prevMoveCoordinates = (ev.RawX, ev.RawY);
                        return false;
                    }
                    else if (m_elem.ShouldInterceptScroll)// if sheet is not maximized and intercept is enabled
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case MotionEventActions.Down:
                    OnStart(ev.RawX, ev.RawY);
                    return false;
            }

            return false;
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            var (x, y) = ToDip(e.RawX, e.RawY);

            if (e.ActionMasked is MotionEventActions.Up or MotionEventActions.Cancel) // ended gesture
            {
                if (m_isScrolling) // ended scroll
                {
                    m_elem?.SendPan(GetTotal(m_startX, x), GetTotal(m_startY, y), GetDelta(x, m_prev.x),
                        GetDelta(y, m_prev.y), GestureStatus.Completed, m_pointerId); // send completed
                }

                Reset();
                return false;
            }

            return m_detector.OnTouchEvent(e);
        }

        private bool StartScroll(MotionEvent ev)
        {
            OnStart(ev.RawX, ev.RawY);

            var (x, y) = ToDip(ev.RawX, ev.RawY);
            m_prev = (x, y);
            m_isScrolling = true;
            m_elem?.SendPan(0, 0, 0, 0, GestureStatus.Started, m_pointerId);
            return true;
        }

        private void Reset()
        {
            m_isScrolling = false;
            m_startX = 0;
            m_startY = 0;
            m_prev = (0, 0);
        }

        private void OnStart(float x, float y)
        {
            m_prevMoveCoordinates = (x, y);

            var (dx, dy) = ToDip(x, y);
            m_startX = dx;
            m_startY = dy;
            m_pointerId = m_random.Next();
        }

        private float GetTotal(float start, float end)
        {
            return end - start;
        }

        private static float GetDelta(float x, float prev)
        {
            return x - prev;
        }

        private (float, float) ToDip(float rawX, float rawY)
        {
            return (rawX / m_density, rawY / m_density);
        }
    }
}