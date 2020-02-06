using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.Generic;
using DIPS.Xamarin.UI.Util;

namespace DIPS.Xamarin.UI.Controls.Slidable
{
    /// <summary>
    /// To be added
    /// </summary>
    public class SlidableLayout : ContentView
    {
        private readonly PanGestureRecognizer m_rec = new PanGestureRecognizer();
        private readonly AccelerationService m_accelerator = new AccelerationService(true);
        private int m_lastId = -1;
        private double m_startSlideLocation;
        private int m_lastIndex = int.MinValue;
        private bool disableTouchScroll;

        /// <summary>
        /// To be added
        /// </summary>
        public SlidableLayout()
        {
            Padding = 0;
            Margin = 0;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            VerticalOptions = LayoutOptions.FillAndExpand;
            Config = new SliderConfig(int.MinValue, int.MaxValue);
            SlideProperties = new SlidableProperties(0, -1, false);
            m_rec = new PanGestureRecognizer();
            GestureRecognizers.Add(m_rec);
            m_rec.PanUpdated += Rec_PanUpdated;
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
        /// To be added
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            OnScrolledInternal();
        }

        private void Rec_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (DisableTouchScroll)
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
            }

            var currentPos = m_startSlideLocation - e.TotalX;
            m_lastId = currentId;
            var index = Math.Max(Config.MinValue - 0.45, Math.Min(Config.MaxValue + 0.45, CalculateIndex(currentPos)));

            if (e.StatusType == GestureStatus.Completed || e.StatusType == GestureStatus.Canceled)
            {
                index = SlideProperties.Position;
                //TODO: Verify short time and set tap instead of drag.
                //TODO: Check if a tap is even recognized as a 
                //currentPos = CalculateDist(Math.Round(SlideProperties.Position));
                SlideProperties = new SlidableProperties(index, m_lastId, e.StatusType != GestureStatus.Completed && e.StatusType != GestureStatus.Canceled);
            }
            else
            {
                SlideProperties = new SlidableProperties(index, m_lastId, e.StatusType != GestureStatus.Completed && e.StatusType != GestureStatus.Canceled);
                OnScrolledInternal();
            }

            if (e.StatusType == GestureStatus.Completed || e.StatusType == GestureStatus.Canceled)
            {
                m_accelerator.Max = Config.MaxValue + 0.45;
                m_accelerator.Min = Config.MinValue + 0.45;
                m_accelerator.EndDrag();
                Device.StartTimer(TimeSpan.FromMilliseconds(32), () => // ~30 fps
                {
                    var next = m_accelerator.GetValue(out bool isDone);
                    index = next;
                    if (SlideProperties.IsHeld) return true;
                    SlideProperties = new SlidableProperties(index, m_lastId, false);
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
        /// To be added
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected double CalculateDist(double index)
        {
            var width = GetItemWidth();
            return index * width + Width / 2;
        }

        /// <summary>
        /// To be added
        /// </summary>
        /// <returns></returns>
        protected double GetItemWidth()
        {
            if (!WidthIsProportional) return ElementWidth;
            return Width * ElementWidth;
        }

        private void OnScrolledInternal()
        {
            var index = (int)Math.Round(SlideProperties.Position);
            if (index != m_lastIndex && SelectedItemChangedCommand != null)
            {
                SelectedItemChangedCommand?.Execute(index);
                m_lastIndex = index;
            }
            if (Width < 0.1) return;
            OnScrolled(SlideProperties.Position);
        }

        /// <summary>
        /// To be added
        /// </summary>
        /// <param name="index"></param>
        protected virtual void OnScrolled(double index)
        {
        }

        /// <summary>
        /// To be added
        /// </summary>
        /// <param name="location"></param>
        protected virtual void OnClick(int item)
        {
            // On click is only for when a tap gesture is done and we enble tap. 
        }

        /// <summary>
        /// To be added
        /// </summary>
        public static readonly BindableProperty ConfigProperty = BindableProperty.Create(
            nameof(Config),
            typeof(SliderConfig),
            typeof(SlidableLayout));

        /// <summary>
        /// To be added
        /// </summary>
        public SliderConfig Config
        {
            get => (SliderConfig)GetValue(ConfigProperty);
            set => SetValue(ConfigProperty, value);
        }

        /// <summary>
        /// To be added
        /// </summary>
        public static readonly BindableProperty SlidePropertiesProperty = BindableProperty.Create(
            nameof(SlideProperties),
            typeof(SlidableProperties),
            typeof(SlidableLayout),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: OnChanged);

        /// <summary>
        /// To be added
        /// </summary>
        public SlidableProperties SlideProperties
        {
            get => (SlidableProperties)GetValue(SlidePropertiesProperty);
            set => SetValue(SlidePropertiesProperty, value);
        }

        /// <summary>
        /// To be added
        /// </summary>
        public static readonly BindableProperty SelectedItemChangedCommandProperty = BindableProperty.Create(
            nameof(SelectedItemChangedCommand),
            typeof(ICommand),
            typeof(SlidableLayout));

        /// <summary>
        /// To be added
        /// </summary>
        public ICommand SelectedItemChangedCommand
        {
            get => (ICommand)GetValue(SelectedItemChangedCommandProperty);
            set => SetValue(SelectedItemChangedCommandProperty, value);
        }

        /// <summary>
        /// To be added
        /// </summary>
        public static readonly BindableProperty ElementWidthProperty = BindableProperty.Create(
            nameof(ElementWidth),
            typeof(double),
            typeof(SlidableLayout),
            0.2);

        /// <summary>
        /// To be added
        /// </summary>
        public double ElementWidth
        {
            get => (double)GetValue(ElementWidthProperty);
            set => SetValue(ElementWidthProperty, value);
        }

        /// <summary>
        /// To be added
        /// </summary>
        public static readonly BindableProperty WidthIsProportionalProperty = BindableProperty.Create(
            nameof(WidthIsProportional),
            typeof(bool),
            typeof(SlidableLayout),
            true);

        /// <summary>
        /// To be added
        /// </summary>
        public bool WidthIsProportional
        {
            get => (bool)GetValue(WidthIsProportionalProperty);
            set => SetValue(WidthIsProportionalProperty, value);
        }

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
    }
}
