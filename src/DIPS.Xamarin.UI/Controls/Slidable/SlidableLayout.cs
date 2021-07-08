using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.Generic;
using DIPS.Xamarin.UI.Util;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Diagnostics;
using DIPS.Xamarin.UI.Vibration;

namespace DIPS.Xamarin.UI.Controls.Slidable
{
    /// <summary>
    /// Layout used to scroll through indexes smoothly. This has enabled acceleration.
    /// </summary>
    public class SlidableLayout : ContentView
    {
        private static int m_tappedValue = -3;
        private readonly PanGestureRecognizer m_rec = new PanGestureRecognizer();
        private readonly AccelerationService m_accelerator = new AccelerationService(true);
        private int m_lastId = -2; // Different than default of SlideProperties
        private double m_startSlideLocation;
        private int m_lastIndex = int.MinValue;
        private bool disableTouchScroll;
        /// <summary>
        /// Disables touch stopping of the slider.
        /// </summary>
        protected bool m_disableTouchStop;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public SlidableLayout()
        {
            Padding = 0;
            Margin = 0;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            VerticalOptions = LayoutOptions.FillAndExpand;
            Config = new SliderConfig(int.MinValue, int.MaxValue);
            m_rec = new PanGestureRecognizer();

            if (Device.RuntimePlatform == Device.iOS)
            {
                GestureRecognizers.Add(m_rec);
                m_rec.PanUpdated += Rec_PanUpdated;
            }
        }

        private static void OnChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var me = (SlidableLayout)bindable;
            if (me.m_lastId == me.SlideProperties.HoldId)
            {
                return;
            }

            me.OnScrolledInternal();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            OnScrolledInternal(true);
        }

        private static int s_scrollToId = -42;

        /// <summary>
        /// Scrolls to the index
        /// </summary>
        /// <param name="index">Index to scroll to</param>
        /// <param name="length">Time used on the scrolling</param>
        public void ScrollTo(int index, int length = 250)
        {
            SlidableProperties.ScrollTo(s => SlideProperties = s, () => SlideProperties, index, length);
        }

        private void Rec_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (DisableTouchScroll)
            {
                return;
            }

            if (Config == null)
            {
                return;
            }

            var currentId = e.GestureId;
            if (!(m_lastId == SlideProperties.HoldId || !SlideProperties.IsHeld || currentId == m_lastId))
            {
                return;
            }

            if (e.StatusType == GestureStatus.Started)
            {
                // Start tracking time
                m_startSlideLocation = CalculateDist(SlideProperties.Position);
                PanStarted?.Invoke(this, new PanEventArgs((int) Math.Round(CalculateIndex(m_startSlideLocation))));
                if (VibrateOnSelectionChanged)
                {
                    m_feedbackGenerator = new SelectionFeedbackGenerator();
                    m_feedbackGenerator.Prepare();
                }
            }

            var currentPos = m_startSlideLocation - e.TotalX;
            m_lastId = currentId;
            var index = Math.Max(Config.MinValue - 0.45, Math.Min(Config.MaxValue + 0.45, CalculateIndex(currentPos)));

            if (e.StatusType == GestureStatus.Completed || e.StatusType == GestureStatus.Canceled)
            {
                index = SlideProperties.Position;
                m_accelerator.OnDrag(index);
                SlideProperties = new SlidableProperties(index, m_lastId, e.StatusType != GestureStatus.Completed && e.StatusType != GestureStatus.Canceled);
            }
            else
            {
                SlideProperties = new SlidableProperties(index, m_lastId, e.StatusType != GestureStatus.Completed && e.StatusType != GestureStatus.Canceled);
                OnScrolledInternal();
            }

            if (e.StatusType == GestureStatus.Completed || e.StatusType == GestureStatus.Canceled)
            {
                m_accelerator.EndDrag();

                if (StopOnGestureEnded)
                {
                    PanEnded?.Invoke(this, new PanEventArgs((int) Math.Round(index)));
                    m_feedbackGenerator.Release();
                    return;
                }
                
                Device.StartTimer(TimeSpan.FromMilliseconds(20), () => // ~40 fps
                {
                    if (currentId != SlideProperties.HoldId) return false;
                    m_accelerator.Min = Config.MinValue - 0.45;
                    m_accelerator.Max = Config.MaxValue + 0.45;
                    var next = m_accelerator.GetValue(out bool isDone);
                    index = next;
                    if (SlideProperties.IsHeld) return false;
                    SlideProperties = new SlidableProperties(index, m_lastId, false);
                    OnScrolledInternal();
                    
                    if (isDone) PanEnded?.Invoke(this, new PanEventArgs((int) Math.Round(index)));
                    
                    return !isDone;
                });
            }
            else if (e.StatusType == GestureStatus.Started)
            {
                m_accelerator.StartDrag(index);
            }
            else
            {
                m_accelerator.OnDrag(index);
            }
        }

        private double CalculateIndex(double dist)
        {
            var itemWidth = GetItemWidth();
            return (dist - Width / 2) / itemWidth;
        }

        /// <summary>
        /// Gets the index of a value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected int GetIndexFromValue(double value) => (int)Math.Round(value);

        /// <summary>
        /// Center of the screen
        /// </summary>
        protected double Center => Width / 2;

        /// <summary>
        /// Gets the center
        /// </summary>
        /// <param name="value"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        protected double GetCenterPosition(double value, double index) => Center + GetItemWidth() * (GetIndexFromValue(value) - index);

        /// <summary>
        /// Gets the left position of the item, used in drawing a bigger item
        /// </summary>
        /// <param name="value"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        protected double GetLeftPosition(double value, double index) => Center + GetItemWidth() * (GetIndexFromValue(value) - 0.5 - index);

        /// <summary>
        /// Calculates the distance from the center
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected double CalculateDist(double index)
        {
            var width = GetItemWidth();
            return index * width + Width / 2;
        }

        /// <summary>
        /// Gets the width of the item, by extracting the ElementWidth and Proportional settings.
        /// </summary>
        /// <returns></returns>
        protected double GetItemWidth()
        {
            if (!WidthIsProportional) return ElementWidth;
            return Width * ElementWidth;
        }

        private void OnScrolledInternal(bool initial = false)
        {
            var index = (int)Math.Round(SlideProperties.Position);
            if (index != m_lastIndex && SelectedItemChangedCommand != null)
            {
                if (!initial)
                {
                    Vibrate();
                }

                SelectedItemChangedCommand?.Execute(index);
                m_lastIndex = index;
            }

            OnScrolled(SlideProperties.Position);
        }

        private void Vibrate()
        {
            if (!VibrateOnSelectionChanged) return;
            try
            {
                m_feedbackGenerator.SelectionChanged();
            }
            catch
            {
                VibrateOnSelectionChanged = false;
            }
        }

        /// <summary>
        /// Override this to handle the scrolling of this layout
        /// </summary>
        /// <param name="index"></param>
        protected virtual void OnScrolled(double index)
        {
        }

        /// <summary>
        /// <see cref="Config"/>
        /// </summary>
        public static readonly BindableProperty ConfigProperty = BindableProperty.Create(
            nameof(Config),
            typeof(SliderConfig),
            typeof(SlidableLayout));
        
        /// <summary>
        /// Configuration indicating max and min values of this layout. 
        /// </summary>
        public SliderConfig Config
        {
            get => (SliderConfig)GetValue(ConfigProperty);
            set => SetValue(ConfigProperty, value);
        }

        /// <summary>
        /// <see cref="SlideProperties"/>
        /// </summary>
        public static readonly BindableProperty SlidePropertiesProperty = BindableProperty.Create(
            nameof(SlideProperties),
            typeof(SlidableProperties),
            typeof(SlidableLayout),
            defaultBindingMode: BindingMode.TwoWay,
            defaultValue: new SlidableProperties(0),
            propertyChanged: OnChanged);

        /// <summary>
        /// Properties used to define where the slider is at the moment, in terms of index and some internal properties used for the scrolling.
        /// </summary>
        public SlidableProperties SlideProperties
        {
            get => (SlidableProperties)GetValue(SlidePropertiesProperty);
            set => SetValue(SlidePropertiesProperty, value);
        }

        /// <summary>
        /// <see cref="SelectedItemChangedCommand"/>
        /// </summary>
        public static readonly BindableProperty SelectedItemChangedCommandProperty = BindableProperty.Create(
            nameof(SelectedItemChangedCommand),
            typeof(ICommand),
            typeof(SlidableLayout));

        /// <summary>
        /// Command invoked every time the selection of an index changes.
        /// </summary>
        public ICommand SelectedItemChangedCommand
        {
            get => (ICommand)GetValue(SelectedItemChangedCommandProperty);
            set => SetValue(SelectedItemChangedCommandProperty, value);
        }

        /// <summary>
        /// <see cref="ElementWidth"/>
        /// </summary>
        public static readonly BindableProperty ElementWidthProperty = BindableProperty.Create(
            nameof(ElementWidth),
            typeof(double),
            typeof(SlidableLayout),
            0.2);

        /// <summary>
        /// Width of an Element, either proportional or exact.
        /// </summary>
        public double ElementWidth
        {
            get => (double)GetValue(ElementWidthProperty);
            set => SetValue(ElementWidthProperty, value);
        }

        /// <summary>
        /// <see cref="WidthIsProportional"/>
        /// </summary>
        public static readonly BindableProperty WidthIsProportionalProperty = BindableProperty.Create(
            nameof(WidthIsProportional),
            typeof(bool),
            typeof(SlidableLayout),
            true);

        private SelectionFeedbackGenerator m_feedbackGenerator;


        /// <summary>
        /// Default true and defines if the ElementWidth is proportional to the width of the parent or exact pixel values.
        /// </summary>
        public bool WidthIsProportional
        {
            get => (bool)GetValue(WidthIsProportionalProperty);
            set => SetValue(WidthIsProportionalProperty, value);
        }

        /// <summary>
        /// Set this to true if you want a small vibration every time the index changes.
        /// </summary>
        public bool VibrateOnSelectionChanged { get; set; }

        /// <summary>
        /// Disables the scrolling on this Layout. Use this if you layout has to be inside a ScrollView on Android.
        /// </summary>
        public bool DisableTouchScroll { get => disableTouchScroll;
            set
            {
                disableTouchScroll = value;
                GestureRecognizers.Remove(m_rec);
                if (!value)
                {
                    GestureRecognizers.Add(m_rec);
                }
            }
        }

        internal void SendPan(float distanceX, float distanceY, GestureStatus status, int id)
        {
            Rec_PanUpdated(this, new PanUpdatedEventArgs(status, id, distanceX, distanceY));
        }

        internal void SendTapped(float x, float y)
        {
            
        }
        
        /// <summary>
        /// Toggles drag effect after pan gesture is completed. Set to true if scroll should stop immediately after finger is lifted. 
        /// </summary>
        public bool StopOnGestureEnded { get; set; }
        
        /// <summary>
        /// Invoked on start of a Pan gesture
        /// </summary>
        public event EventHandler<PanEventArgs> PanStarted;
        
        /// <summary>
        /// Invoked on end of Pan gesture.
        /// </summary>
        public event EventHandler<PanEventArgs> PanEnded;
    }
}
