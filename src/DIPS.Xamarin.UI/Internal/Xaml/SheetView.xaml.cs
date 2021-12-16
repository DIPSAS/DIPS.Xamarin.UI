using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using DIPS.Xamarin.UI.Controls.Sheet;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Internal.xaml
{
    /// <summary>
    ///     A sheetview that is used inside of a <see cref="SheetBehavior" />
    /// </summary>
    /// <remarks>This is a internal Xaml control that should only be used in a <see cref="SheetBehavior" /></remarks>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public partial class SheetView : IGestureAware
    {
        private readonly float[] m_latestDeltas = new float[5];
        private readonly object m_lock = new object();
        private readonly SheetBehavior m_sheetBehaviour;
        private double m_endY, m_startY;
        private bool m_isOpen;
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

        /// <summary>
        ///     The internal outer sheet frame of the view
        /// </summary>
        internal SheetView Sheet => this;

        private IList<double> SnapPoints { get; } = new List<double> {.25, .5, 1.0};

        private float PixelsPerSecond { get; } = 1000;

        void IGestureAware.SendPan(float totalX, float totalY, float distanceX, float distanceY, GestureStatus status,
            int id)
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

        void IGestureAware.SendTapped(float x, float y)
        {
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

        private void MoveSheet(float distanceY)
        {
            if (ShouldOpen(distanceY))
            {
                Open();
                return;
            }

            if (ShouldClose(distanceY))
            {
                NotifyClose();
                return;
            }

            TranslationY += distanceY;
        }

        private void Open()
        {
            // handle direction

            this.TranslateTo(0, 0, 300, Easing.CubicOut);
            m_isOpen = true;
        }

        private void NotifyClose()
        {
            m_sheetBehaviour.IsOpen = false;
        }

        internal void InternalClose()
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

        internal void Toggle()
        {
            this.TranslateTo(0, m_isOpen ? Height : Height - (SnapPoints.FirstOrDefault() * Height), 150,
                Easing.CubicOut);
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

                    NotifyClose();
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
            return (float)y;
        }

        private PanDirection FindLatestPanDirection()
        {
            lock (m_lock)
            {
                var direction =
                    m_latestDeltas.Sum(); // sum of latest movements to get the general/most likely intended direction
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
                        NotifyClose();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(panDirection), panDirection, null);
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

        internal enum PanDirection
        {
            Up,
            Down
        }
    }
}