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
        private int m_lastId = -1;
        private double m_startSlideLocation;
        private int m_lastIndex = int.MinValue;
        private AccelerationService m_accelerator = new AccelerationService(true);
        /// <summary>
        /// To be added
        /// </summary>
        public SlidableLayout()
        {
            ElementWidth = 0.2;
            WidthIsProportional = true;
            Config = new SliderConfig(int.MinValue, int.MaxValue);
            SlideProperties = new SlidableProperties(0, -1, false);
            var rec = new PanGestureRecognizer();
            GestureRecognizers.Add(rec);
            rec.PanUpdated += Rec_PanUpdated;
        }

        private static void OnChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var me = (SlidableLayout)bindable;
            if(me.m_lastId == me.SlideProperties.HoldId)
            {
                return;
            }

            var index = me.SlideProperties.Position;
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

        private List<PanUpdatedEventArgs> args = new List<PanUpdatedEventArgs>();
        public void Rec_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            args.Add(e);
            var currentId = e.GestureId;
            //if (!(m_lastId == SlideProperties.HoldId || !SlideProperties.IsHeld || currentId == m_lastId))
            //{
            //    return;
            //}

            if (e.StatusType == GestureStatus.Started)
            {
                // Start tracking time
                m_startSlideLocation = CalculateDist(SlideProperties.Position);
            }

            var currentPos = m_startSlideLocation - e.TotalX;
            
            if (e.StatusType == GestureStatus.Completed || e.StatusType == GestureStatus.Canceled)
            {
                currentPos = CalculateDist(Math.Round(SlideProperties.Position));

            }
            else
            {
                
            }

            m_lastId = currentId;
            var index = Math.Max(Config.MinValue-0.45, Math.Min(Config.MaxValue+0.45, CalculateIndex(currentPos)));
            SlideProperties = new SlidableProperties(index, m_lastId, e.StatusType != GestureStatus.Completed && e.StatusType != GestureStatus.Canceled);
            OnScrolledInternal();

            //if (e.StatusType == GestureStatus.Completed || e.StatusType == GestureStatus.Canceled)
            //{
            //    m_accelerator.EndDrag(index);
            //    Device.StartTimer(TimeSpan.FromMilliseconds(60d / 1000d), () =>
            //    {
            //        var next = m_accelerator.GetValue(out bool isDone);
            //        index = Math.Max(Config.MinValue - 0.45, Math.Min(Config.MaxValue + 0.45, next));
            //        if (SlideProperties.IsHeld) return false;
            //        SlideProperties = new SlidableProperties(index, m_lastId, false);
            //        return isDone;
            //    });
            //}
            //else if (e.StatusType == GestureStatus.Started)
            //{
            //    m_accelerator.StartDrag(index);
            //}
            //else
            //{
            //    m_accelerator.OnDrag(index);
            //}
        }

        private double CalculateIndex(double dist)
        {
            var width = GetItemWidth();
            return (dist-Width/2) / width;
        }

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
            OnScrolled((SlideProperties.Position+0.5), Width / 2 - GetItemWidth()/2, index);
        }

        /// <summary>
        /// To be added
        /// </summary>
        /// <param name="index"></param>
        /// <param name="offset"></param>
        /// <param name="selectedIndex"></param>
        protected virtual void OnScrolled(double index, double offset, int selectedIndex)
        {
        }

        /// <summary>
        /// To be added
        /// </summary>
        /// <param name="location"></param>
        protected virtual void OnClick(int location)
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
            typeof(SlidableLayout));

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
            typeof(SlidableLayout));

        /// <summary>
        /// To be added
        /// </summary>
        public bool WidthIsProportional
        {
            get => (bool)GetValue(WidthIsProportionalProperty);
            set => SetValue(WidthIsProportionalProperty, value);
        }
    }
}
