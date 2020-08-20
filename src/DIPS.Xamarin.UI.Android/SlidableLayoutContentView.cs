using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
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
        private GestureDetector m_detector;
        private SlidableLayout m_elem;

        public SlidableLayoutContentView(Context context) : base(context)
        {
            m_detector = new GestureDetector(context, this);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement is SlidableLayout contentView) m_elem = contentView;
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            if (ev.ActionMasked == MotionEventActions.Up || ev.ActionMasked == MotionEventActions.Cancel) m_elem?.SendPan(0, 0, GestureStatus.Completed, ev.GetPointerId(0));

            m_detector.OnTouchEvent(ev);

            return false;
        }

        public bool OnDown(MotionEvent e)
        {
            m_elem?.SendPan(0, 0, GestureStatus.Started, e.GetPointerId(0));
            return true;
        }

        public bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
        {
            m_elem.SendPan(0,0, GestureStatus.Completed, e1.GetPointerId(0));
            return true;
        }

        public void OnLongPress(MotionEvent e)
        {
            return;
        }

        public bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
        {
            m_elem?.SendPan(e2.GetX() - e1.GetX(), e2.GetY() - e1.GetY(), GestureStatus.Running, e1.GetPointerId(0));
            return true;
        }

        public void OnShowPress(MotionEvent e)
        {
            return;
        }

        public bool OnSingleTapUp(MotionEvent e)
        {
            return true;
        }
    }
}