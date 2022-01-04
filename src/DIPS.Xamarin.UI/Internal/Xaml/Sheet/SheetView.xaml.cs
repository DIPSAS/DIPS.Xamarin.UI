using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly object m_lock = new();
        internal readonly SheetBehavior m_sheetBehaviour;
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

            m_sheetBehaviour.PropertyChanged += SheetBehaviorOnPropertyChanged;
            
            if (Device.RuntimePlatform == Device.iOS)
            {
                // OverlayBoxView.IsVisible = m_sheetBehaviour.InterceptDragGesture;
                var panGestureRecognizer = new PanGestureRecognizer();
                GestureRecognizers.Add(panGestureRecognizer);
                panGestureRecognizer.PanUpdated += OnPan;
            }
            else
            {
                OverlayBoxView.IsVisible = false;
            }
        }

        private void SheetBehaviorOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is nameof(SheetBehavior.InterceptDragGesture) && sender is SheetBehavior sheetBehavior && Device.RuntimePlatform is Device.iOS)
            {
                // OverlayBoxView.IsVisible = sheetBehavior.InterceptDragGesture;
            }
        }

        /// <summary>
        ///     The height that the sheet content needs if it should display all of its content
        /// </summary>
        internal double SheetContentHeightRequest =>
            sheetContentView.Content != null
                ? SheetContentView.Content.Height + HeaderGrid.Height + HeaderGrid.Padding.Top +
                  HeaderGrid.Padding.Bottom +
                  OuterSheetFrame.CornerRadius
                : 0;

        internal ContentView SheetContentView => sheetContentView;

        internal SheetView Sheet => this;

        internal IList<double> SnapPoints => m_sheetBehaviour.SnapPoints;
        private int PixelsPerSecond => m_sheetBehaviour.FlingSpeedThreshold;
        private bool IsDraggable => m_sheetBehaviour.IsDraggable;

        private SheetState CurrentState { get; set; } = SheetState.NotMaximized;

        /// <summary>
        /// For internal use.
        /// </summary>
        public void SendPan(float totalX, float totalY, float distanceX, float distanceY, GestureStatus status,
            int id) // android
        {
            if (!IsDraggable)
            {
                return;
            }

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

        /// <summary>
        /// For internal use on Android.
        /// </summary>
        public bool ShouldInterceptScroll => m_sheetBehaviour.InterceptDragGesture && CurrentState is not SheetState.Maximized;

        private void OnPan(object sender, PanUpdatedEventArgs e) // iOS
        {
            SendPan((float)e.TotalX, (float)e.TotalY,
                e.StatusType == GestureStatus.Completed ? 0 : (float)(e.TotalX - m_prevX),
                e.StatusType == GestureStatus.Completed ? 0 : (float)(e.TotalY - m_prevY),
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
            if (ShouldMinimize(deltaY))
            {
                var minimizedPosition = this.RatioToYValue(SnapPoints.FirstOrDefault(), m_sheetBehaviour.Alignment);
                TranslationY += minimizedPosition - TranslationY;
            }
            else if (ShouldMaximize(deltaY))
            {
                var maximizedPosition = this.RatioToYValue(SnapPoints.LastOrDefault(), m_sheetBehaviour.Alignment);
                TranslationY += maximizedPosition - TranslationY;
            }
            else
            {
                TranslationY += deltaY;
            }
            
            NotifyPositionChange();
        }

        private async Task Maximize(float velocity = 1500)
        {
            await TranslateSheetTo(this.RatioToYValue(SnapPoints.LastOrDefault(), m_sheetBehaviour.Alignment), velocity);
        }

        private async Task Minimize(float velocity = 1500)
        {
            if (SnapPoints.FirstOrDefault() is 0.0)
            {
                m_sheetBehaviour.IsOpen = false;            
            }
            else
            {
                await TranslateSheetTo(this.RatioToYValue(SnapPoints.FirstOrDefault(), m_sheetBehaviour.Alignment), velocity);
            }
        }

        internal Task Close()
        {
            m_sheetBehaviour.PropertyChanged -= SheetBehaviorOnPropertyChanged;
            return TranslateSheetTo(m_sheetBehaviour.Alignment == AlignmentOptions.Bottom ? Height : -Height, 1750);
        }

        private void SetState(SheetState state)
        {
            CurrentState = state;

            if (Device.RuntimePlatform is Device.Android) return;
            
            // OverlayBoxView.IsVisible = state switch
            // {
            //     SheetState.Maximized => false,
            //     SheetState.NotMaximized => m_sheetBehaviour.InterceptDragGesture,
            //     _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
            // };
        }

        private bool ShouldMinimize(float deltaY)
        {
            return m_sheetBehaviour.Alignment switch
            {
                AlignmentOptions.Bottom => TranslationY + deltaY >= 
                                           this.RatioToYValue(SnapPoints.FirstOrDefault(), 
                                               AlignmentOptions.Bottom),
                AlignmentOptions.Top => TranslationY + deltaY <= 
                                        this.RatioToYValue(SnapPoints.FirstOrDefault(),
                                            AlignmentOptions.Top),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private bool ShouldMaximize(float deltaY)
        {
            return m_sheetBehaviour.Alignment switch
            {
                AlignmentOptions.Bottom => TranslationY + deltaY <=
                                           this.RatioToYValue(SnapPoints.LastOrDefault(),
                                               AlignmentOptions.Bottom),
                AlignmentOptions.Top => TranslationY + deltaY >=
                                        this.RatioToYValue(SnapPoints.LastOrDefault(),
                                            AlignmentOptions.Top),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        internal async Task Open()
        {
            await Task.Delay(50);// to avoid split-second of showing sheet on iOS
            Opacity = 1;

            FillerBoxView.HeightRequest = (Height * (1 - SnapPoints.LastOrDefault())) + OuterSheetFrame.CornerRadius;

            m_sheetBehaviour.BeforeOpening();
            this.FindOpeningY(out var y);

            await TranslateSheetTo(y);

            m_sheetBehaviour.AfterOpening();
        }

        private void OnDragStarted()
        {
            m_watch = Stopwatch.StartNew();
        }

        private void OnDragEnded()
        {
            m_watch.Stop();

            var direction = SheetViewUtility.FindLatestDragDirection(ref m_latestDeltas);

            if (TryFling(direction, out var velocity)) { }
            else
            {
                SnapTo(direction, velocity);
            }
        }

        private async Task SnapTo(DragDirection dragDirection, float velocity)
        {
            var currentPositionRatio = 1 - (Math.Abs(TranslationY) / Height);

            if (this.TryFindSnapPoint(dragDirection, currentPositionRatio, m_sheetBehaviour.Alignment, out var y))
            {
                await TranslateSheetTo(y, velocity);
                return;
            }

            switch (dragDirection)
            {
                case DragDirection.Up when m_sheetBehaviour.Alignment == AlignmentOptions.Bottom:
                case DragDirection.Down when m_sheetBehaviour.Alignment == AlignmentOptions.Top:
                    await Maximize(velocity);
                    break;
                case DragDirection.Down when m_sheetBehaviour.Alignment == AlignmentOptions.Bottom:
                case DragDirection.Up when m_sheetBehaviour.Alignment == AlignmentOptions.Top:
                    await Minimize(velocity);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dragDirection), dragDirection, null);
            }
        }

        private async Task TranslateSheetTo(double y, float velocity = 1500)
        {
            var travel = Math.Abs(y - TranslationY);
            var duration = travel / velocity * 1000;

            duration = duration > 275 ? 275 : duration < 175 ? 175 : duration;

            await this.TranslateTo(0, y, (uint)duration, Easing.CubicOut);

            SetState(this.IndexOfClosestSnapPoint(y) < SnapPoints.Count - 1
                ? SheetState.NotMaximized
                : SheetState.Maximized);
            
            NotifyPositionChange();
        }

        private void NotifyPositionChange()
        {
            m_sheetBehaviour.Position = 1 - this.YValueToRatio(TranslationY);
        }

        private bool TryFling(DragDirection dragDirection, out float velocity)
        {
            var (thresholdReached, v) = SheetViewUtility.IsThresholdReached(ref m_latestDeltas, PixelsPerSecond);

            velocity = v;

            if (thresholdReached)
            {
                switch (dragDirection)
                {
                    case DragDirection.Up when m_sheetBehaviour.Alignment == AlignmentOptions.Bottom:
                    case DragDirection.Down when m_sheetBehaviour.Alignment == AlignmentOptions.Top:
                        Maximize(v);
                        break;
                    case DragDirection.Down when m_sheetBehaviour.Alignment == AlignmentOptions.Bottom:
                    case DragDirection.Up when m_sheetBehaviour.Alignment == AlignmentOptions.Top:
                        Minimize(v);
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
                        ? LayoutOptions.End
                        : LayoutOptions.Start;
                    break;
                case ContentAlignment.Fill:
                    SheetContentGrid.VerticalOptions = LayoutOptions.Fill;
                    break;
                case ContentAlignment.SameAsSheet:
                    SheetContentGrid.VerticalOptions = m_sheetBehaviour.Alignment == AlignmentOptions.Top
                        ? LayoutOptions.End
                        : LayoutOptions.Start;

                    if (m_sheetBehaviour.VerticalContentAlignment == ContentAlignment.SameAsSheet)
                    {
                        Sheet.PropertyChanged += (sender, args) =>
                        {
                            if (args.PropertyName.Equals(TranslationYProperty.PropertyName))
                            {
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

        internal void MoveTo(double position)
        {
            var y = this.RatioToYValue(position, m_sheetBehaviour.Alignment);
            MoveSheet((float)(y - TranslationY));
        }

        // only used on iOS.
        private void OverlayOnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            // pan until a certain state, then ignore

            if (CurrentState is SheetState.Maximized)
            {
                return;
            }
            
            OnPan(sender, e);
        }

        internal enum DragDirection
        {
            Up,
            Down
        }

        private enum SheetState
        {
            Maximized,
            NotMaximized
        }

        internal async Task OnBusyChanged(bool isBusy)
        {
            if (isBusy)
            {
                BusyFrame.Opacity = 1;
                BusyFrame.IsVisible = true;
            }
            else
            {
                await BusyFrame.FadeTo(0, 150, Easing.CubicIn);
                BusyFrame.IsVisible = false;
            }
        }
    }
}