using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using DIPS.Xamarin.UI.Controls.Sheet;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Internal.Xaml.Sheet
{
    /// <summary>
    ///     A sheetview that is used inside of a <see cref="SheetBehavior" />
    /// </summary>
    /// <remarks>This is a internal Xaml control that should only be used in a <see cref="SheetBehavior" /></remarks>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public partial class SheetView : IPanAware
    {
        private readonly object m_lock = new object();
        private readonly SheetBehavior m_sheetBehaviour;
        private (float, long)[] m_latestDeltas = new (float, long)[15];
        private long m_prevDuration;
        private double m_prevX, m_prevY;
        private int m_recordCount;
        private Stopwatch m_watch;

        /// <summary>
        ///     Constructs a <see cref="SheetView" />
        /// </summary>
        /// <param name="sheetBehavior"></param>
        public SheetView(SheetBehavior sheetBehavior)
        {
            InitializeComponent();
            BindingContext = m_sheetBehaviour = sheetBehavior;

            if (Device.RuntimePlatform == Device.iOS)
            {
                var panGestureRecognizer = new PanGestureRecognizer();
                GestureRecognizers.Add(panGestureRecognizer);
                panGestureRecognizer.PanUpdated += OnPan;
            }
        }

        /// <summary>
        ///     The height that the sheet content needs if it should display all of its content
        /// </summary>
        private double SheetContentHeightRequest =>
            sheetContentView.Content != null
                ? SheetContentView.Content.Height + HeaderGrid.Height + HeaderGrid.Padding.Top +
                  HeaderGrid.Padding.Bottom +
                  OuterSheetFrame.CornerRadius
                : 0;

        internal ContentView SheetContentView => sheetContentView;

        internal SheetView Sheet => this;

        private IList<double> SnapPoints => m_sheetBehaviour.SnapPoints;

        private int PixelsPerSecond => m_sheetBehaviour.FlingVelocityThreshold;

        public void SendPan(float totalX, float totalY, float distanceX, float distanceY, GestureStatus status,
            int id) // android
        {
            switch (status)
            {
                case GestureStatus.Started:
                    OnDragStarted();
                    break;
                case GestureStatus.Running:
                    MoveSheet(distanceY);
                    RecordDelta(distanceY);
                    break;
                case GestureStatus.Completed:
                    MoveSheet(distanceY);
                    OnDragEnded();
                    break;
                case GestureStatus.Canceled:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }

        private void OnPan(object sender, PanUpdatedEventArgs e) // iOS
        {
            SendPan((float)e.TotalX, (float)e.TotalY, (float)(e.TotalX - m_prevX), (float)(e.TotalY - m_prevY),
                e.StatusType, e.GestureId);
            m_prevX = e.TotalX;
            m_prevY = e.TotalY;
        }

        private void RecordDelta(float deltaY)
        {
            lock (m_lock)
            {
                if (m_recordCount == m_latestDeltas.Length)
                {
                    m_recordCount = 0;
                }

                var currentDuration = m_watch.ElapsedMilliseconds;
                m_latestDeltas[m_recordCount] = (deltaY, currentDuration - m_prevDuration);
                m_recordCount++;
                m_prevDuration = currentDuration;
            }
        }

        private void MoveSheet(float deltaY)
        {
            if (ShouldOpen(deltaY))
            {
                TranslationY = SheetViewUtility.RatioToYValue(SnapPoints.LastOrDefault(), Height, m_sheetBehaviour.Alignment);
                return;
            }

            if (ShouldClose(deltaY))
            {
                NotifyClose();
                return;
            }

            TranslationY += deltaY;
        }

        private void Open()
        {
            TranslateSheet(SheetViewUtility.RatioToYValue(SnapPoints.LastOrDefault(), Height,
                m_sheetBehaviour.Alignment));
        }

        private void NotifyClose()
        {
            m_sheetBehaviour.IsOpen = false;
        }

        internal void InternalClose()
        {
            TranslateSheet(m_sheetBehaviour.Alignment == AlignmentOptions.Bottom ? Height : -Height);
        }

        private bool ShouldClose(float deltaY)
        {
            return m_sheetBehaviour.Alignment switch
            {
                AlignmentOptions.Bottom => TranslationY + deltaY >= Height,
                AlignmentOptions.Top => TranslationY + deltaY <= -Height,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private bool ShouldOpen(float deltaY)
        {
            Console.WriteLine($"HERE: SUM: {TranslationY + deltaY}, must be <= {SheetViewUtility.RatioToYValue(SnapPoints.LastOrDefault(), Height, AlignmentOptions.Bottom)}");
            return m_sheetBehaviour.Alignment switch
            {
                AlignmentOptions.Bottom => TranslationY + deltaY <=
                                           SheetViewUtility.RatioToYValue(SnapPoints.LastOrDefault(), Height,
                                               AlignmentOptions.Bottom),
                AlignmentOptions.Top => TranslationY + deltaY >=
                                        SheetViewUtility.RatioToYValue(SnapPoints.LastOrDefault(), Height,
                                            AlignmentOptions.Top),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        internal void Show()
        {
            double y;

            switch (m_sheetBehaviour.SheetOpeningStrategy)
            {
                case SheetOpeningStrategy.FirstSnapPoint:
                    y = SheetViewUtility.RatioToYValue(SnapPoints.FirstOrDefault(), Height, m_sheetBehaviour.Alignment);
                    break;
                case SheetOpeningStrategy.MostFittingSnapPoint:
                    if (TryFindSnapPoint(
                        m_sheetBehaviour.Alignment == AlignmentOptions.Bottom ? DragDirection.Up : DragDirection.Down,
                        SheetViewUtility.YValueToRatio(SheetContentHeightRequest, Height), out y)) { }
                    else
                    {
                        y = Height;
                    }

                    break;
                case SheetOpeningStrategy.FitContent:
                    y = m_sheetBehaviour.Alignment switch
                    {
                        AlignmentOptions.Bottom => Height - SheetContentHeightRequest,
                        AlignmentOptions.Top => SheetContentHeightRequest > Height ? Height : SheetContentHeightRequest,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            TranslateSheet(y);
        }

        private void OnDragStarted()
        {
            m_watch = Stopwatch.StartNew();
        }

        private void OnDragEnded()
        {
            m_watch.Stop();

            var direction = SheetViewUtility.FindLatestDragDirection(ref m_latestDeltas);

            if (TryFling(direction)) { }
            else
            {
                SnapTo(direction);
            }
        }

        private void SnapTo(DragDirection dragDirection)
        {
            var currentPositionRatio = 1 - (Math.Abs(TranslationY) / Height);

            if (TryFindSnapPoint(dragDirection, currentPositionRatio, out var y))
            {
                TranslateSheet(y);
                return;
            }

            switch (dragDirection)
            {
                case DragDirection.Up when m_sheetBehaviour.Alignment == AlignmentOptions.Bottom:
                case DragDirection.Down when m_sheetBehaviour.Alignment == AlignmentOptions.Top:
                    Open();
                    break;
                case DragDirection.Down when m_sheetBehaviour.Alignment == AlignmentOptions.Bottom:
                case DragDirection.Up when m_sheetBehaviour.Alignment == AlignmentOptions.Top:
                    NotifyClose();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dragDirection), dragDirection, null);
            }
        }

        private bool TryFindSnapPoint(DragDirection dragDirection, double target, out double y)
        {
            switch (dragDirection)
            {
                case DragDirection.Up when m_sheetBehaviour.Alignment == AlignmentOptions.Bottom:
                case DragDirection.Down when m_sheetBehaviour.Alignment == AlignmentOptions.Top:
                    for (var i = 0; i < SnapPoints.Count; i++)
                    {
                        if (SnapPoints[i] < target)
                        {
                            continue;
                        }

                        y = SheetViewUtility.RatioToYValue(SnapPoints[i], Height, m_sheetBehaviour.Alignment);
                        return true;
                    }

                    break;
                case DragDirection.Down when m_sheetBehaviour.Alignment == AlignmentOptions.Bottom:
                case DragDirection.Up when m_sheetBehaviour.Alignment == AlignmentOptions.Top:
                    for (var i = SnapPoints.Count - 1; i >= 0; i--)
                    {
                        if (SnapPoints[i] > target)
                        {
                            continue;
                        }

                        y = SheetViewUtility.RatioToYValue(SnapPoints[i], Height, m_sheetBehaviour.Alignment);
                        return true;
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dragDirection), dragDirection, null);
            }

            y = -1;
            return false;
        }

        private void TranslateSheet(double y)
        {
            this.TranslateTo(0, y, 225, Easing.CubicOut);
        }

        private bool TryFling(DragDirection dragDirection)
        {
            if (SheetViewUtility.IsThresholdReached(ref m_latestDeltas, PixelsPerSecond))
            {
                switch (dragDirection)
                {
                    case DragDirection.Up when m_sheetBehaviour.Alignment == AlignmentOptions.Bottom:
                    case DragDirection.Down when m_sheetBehaviour.Alignment == AlignmentOptions.Top:
                        Open();
                        break;
                    case DragDirection.Down when m_sheetBehaviour.Alignment == AlignmentOptions.Bottom:
                    case DragDirection.Up when m_sheetBehaviour.Alignment == AlignmentOptions.Top:
                        NotifyClose();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(dragDirection), dragDirection, null);
                }

                return true;
            }

            return false;
        }

        internal void Initialize()
        {
            //Flip the grid if alignment is set to top
            if (m_sheetBehaviour.Alignment == AlignmentOptions.Top)
            {
                AdjustToTopAlignment();
            }
            else
            {
                AdjustToBottomAlignment();
            }

            ChangeVerticalContentAlignment();
        }

        private void AdjustToBottomAlignment()
        {
            SheetGrid.RowDefinitions[0].Height = GridLength.Auto;
            SheetGrid.RowDefinitions[1].Height = GridLength.Star;
            Grid.SetRow(HeaderGrid, 0);
            Grid.SetRow(SheetContentGrid, 1);
            Grid.SetRow(HandleBoxView, 0);
            Grid.SetRow(SeparatorBoxView, 2);
        }

        private void AdjustToTopAlignment()
        {
            SheetGrid.RowDefinitions[0].Height = GridLength.Star;
            SheetGrid.RowDefinitions[1].Height = GridLength.Auto;
            Grid.SetRow(SheetContentGrid, 0);
            Grid.SetRow(HeaderGrid, 1);
            Grid.SetRow(HandleBoxView, 2);
            Grid.SetRow(SeparatorBoxView, 0);
        }

        internal void ChangeVerticalContentAlignment()
        {
            switch (m_sheetBehaviour.VerticalContentAlignment)
            {
                case ContentAlignment.Fit:
                    SheetContentGrid.VerticalOptions = m_sheetBehaviour.Alignment == AlignmentOptions.Top
                        ? LayoutOptions.EndAndExpand
                        : LayoutOptions.StartAndExpand;
                    break;
                case ContentAlignment.Fill:
                    SheetContentGrid.VerticalOptions = LayoutOptions.Fill;
                    break;
                case ContentAlignment.SameAsSheet:
                    SheetContentGrid.VerticalOptions = m_sheetBehaviour.Alignment == AlignmentOptions.Top
                        ? LayoutOptions.EndAndExpand
                        : LayoutOptions.StartAndExpand;

                    if (m_sheetBehaviour.VerticalContentAlignment == ContentAlignment.SameAsSheet)
                    {
                        Sheet.PropertyChanged += (sender, args) =>
                        {
                            if (args.PropertyName.Equals(TranslationYProperty.PropertyName))
                            {
                                var y = Sheet.Y;
                                var newHeight = Sheet.Height - Sheet.TranslationY -
                                                (SheetContentHeightRequest - SheetContentView.Content.Height);
                                SheetContentGrid.HeightRequest = newHeight;
                            }
                        };
                    }

                    break;
            }
        }

        private void CancelButtonClicked(object sender, EventArgs e)
        {
            m_sheetBehaviour.CancelClickedInternal();
        }

        private void ActionButtonClicked(object sender, EventArgs e)
        {
            m_sheetBehaviour.ActionClickedInternal();
        }

        internal enum DragDirection
        {
            Up,
            Down
        }
    }
}