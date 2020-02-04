using System;
using Android.Content;
using Android.Views;
using DIPS.Xamarin.UI.Android.Slidable;
using DIPS.Xamarin.UI.Controls.Slidable;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static System.Math;

[assembly: ExportRenderer(typeof(PanableLayout), typeof(MyPanableLayout))]
namespace DIPS.Xamarin.UI.Android.Slidable
{
    public class MyPanableLayout : ViewRenderer
    {
        private static readonly int InvalidPointerId = -1;
        private int _activePointerId = InvalidPointerId;
        private float _lastTouchX;
        private float _lastTouchY;
        private float _posX;
        private float _posY;
        private float _scaleFactor = 1.0f;
		private GestureDetector _gestureDetector;
		public MyPanableLayout(Context context) : base(context)
        {
			_gestureDetector = new GestureDetector(Context, new CardsGestureListener((dx, status, id) =>
			{
				if (!(Element is PanableLayout panableLayout))
				{
					return;
				}

				panableLayout.Rec_PanUpdated(this, new PanUpdatedEventArgs(status, id, dx, 0));
			}));
		}

		public class CardsGestureListener : GestureDetector.SimpleOnGestureListener
		{
			private static readonly int InvalidPointerId = -1;
			private int _activePointerId = InvalidPointerId;
			private float _lastTouchX;
			private float _lastTouchY;
			private float _posX;
			private float _posY;
			private float _scaleFactor = 1.0f;
			private readonly Action<double, GestureStatus, int> _onSwiped;

			public CardsGestureListener(Action<double, GestureStatus, int> onSwiped)
			=> _onSwiped = onSwiped;

			public override bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
			{
				var diffX = (e2?.GetX() ?? 0) - (e1?.GetX() ?? 0);
				var diffY = (e2?.GetY() ?? 0) - (e1?.GetY() ?? 0);

				var absDiffX = Abs(diffX);
				var absDiffY = Abs(diffY);

                MotionEventActions action = e2.Action & MotionEventActions.Mask;
				GestureStatus status = GestureStatus.Started;
				int pointerIndex = -1;

				switch (action)
				{
					case MotionEventActions.Down:
						status = GestureStatus.Started;
						_lastTouchX = e2.GetX();
						_lastTouchY = e2.GetY();
						_activePointerId = e2.GetPointerId(0);
						_posX = 0;
						_posY = 0;
						break;

					case MotionEventActions.Move:
						status = GestureStatus.Running;
						pointerIndex = e2.FindPointerIndex(_activePointerId);
						float x = e2.GetX(pointerIndex);
						float y = e2.GetY(pointerIndex);
						float deltaX = x - _lastTouchX;
						float deltaY = y - _lastTouchY;
						_posX += deltaX;
						_posY += deltaY;

						_lastTouchX = x;
						_lastTouchY = y;
						break;

					case MotionEventActions.Up:
					case MotionEventActions.Cancel:
						status = GestureStatus.Completed;
						// This events occur when something cancels the gesture (for example the
						// activity going in the background) or when the pointer has been lifted up.
						// We no longer need to keep track of the active pointer.
						_activePointerId = InvalidPointerId;
						break;

					case MotionEventActions.PointerUp:
						status = GestureStatus.Completed;
						// We only want to update the last touch position if the the appropriate pointer
						// has been lifted off the screen.
						pointerIndex = (int)(e2.Action & MotionEventActions.PointerIndexMask) >> (int)MotionEventActions.PointerIndexShift;
						int pointerId = e2.GetPointerId(pointerIndex);
						if (pointerId == _activePointerId)
						{
							// This was our active pointer going up. Choose a new
							// action pointer and adjust accordingly
							int newPointerIndex = pointerIndex == 0 ? 1 : 0;
							_lastTouchX = e2.GetX(newPointerIndex);
							_lastTouchY = e2.GetY(newPointerIndex);
							_activePointerId = e2.GetPointerId(newPointerIndex);
						}
						break;
				}

				//panableLayout.Rec_PanUpdated(this, new PanUpdatedEventArgs(status, pointerIndex, _posX, 0));
				_onSwiped(_posX, status, pointerIndex);
				return true;
			}
		}



		//public override bool OnTouchEvent(MotionEvent ev)
  //      {
  //          if (!(Element is PanableLayout panableLayout))
  //          {
  //              return false;
  //          }

		//	MotionEventActions action = ev.Action & MotionEventActions.Mask;
		//	GestureStatus status = GestureStatus.Started;
		//	int pointerIndex = -1;

		//	switch (action)
		//	{
		//		case MotionEventActions.Down:
		//			status = GestureStatus.Started;
		//			_lastTouchX = ev.GetX();
		//			_lastTouchY = ev.GetY();
		//			_activePointerId = ev.GetPointerId(0);
		//			_posX = 0;
		//			_posY = 0;
		//			break;

		//		case MotionEventActions.Move:
		//			status = GestureStatus.Running;
		//			pointerIndex = ev.FindPointerIndex(_activePointerId);
		//			float x = ev.GetX(pointerIndex);
		//			float y = ev.GetY(pointerIndex);
		//			float deltaX = x - _lastTouchX;
		//			float deltaY = y - _lastTouchY;
		//			_posX += deltaX;
		//			_posY += deltaY;

		//			_lastTouchX = x;
		//			_lastTouchY = y;
		//			break;

		//		case MotionEventActions.Up:
		//		case MotionEventActions.Cancel:
		//			status = GestureStatus.Completed;
		//			// This events occur when something cancels the gesture (for example the
		//			// activity going in the background) or when the pointer has been lifted up.
		//			// We no longer need to keep track of the active pointer.
		//			_activePointerId = InvalidPointerId;
		//			break;

		//		case MotionEventActions.PointerUp:
		//			status = GestureStatus.Completed;
		//			// We only want to update the last touch position if the the appropriate pointer
		//			// has been lifted off the screen.
		//			pointerIndex = (int)(ev.Action & MotionEventActions.PointerIndexMask) >> (int)MotionEventActions.PointerIndexShift;
		//			int pointerId = ev.GetPointerId(pointerIndex);
		//			if (pointerId == _activePointerId)
		//			{
		//				// This was our active pointer going up. Choose a new
		//				// action pointer and adjust accordingly
		//				int newPointerIndex = pointerIndex == 0 ? 1 : 0;
		//				_lastTouchX = ev.GetX(newPointerIndex);
		//				_lastTouchY = ev.GetY(newPointerIndex);
		//				_activePointerId = ev.GetPointerId(newPointerIndex);
		//			}
		//			break;
		//	}

  //          //if(status != GestureStatus.Started)
		//	panableLayout.Rec_PanUpdated(this, new PanUpdatedEventArgs(status, pointerIndex, _posX, _posY));

		//	return true;
		//}

    }
}
