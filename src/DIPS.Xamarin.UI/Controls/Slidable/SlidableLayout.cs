using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.Generic;
namespace DIPS.Xamarin.UI.Controls.Slidable
{
    public class SlidableLayout : ContentView
    {
        private int m_lastId = -1;
        private Label m_debug;
        private double m_startSlideLocation;
        public SlidableLayout()
        {
            Config = new SliderConfig(int.MinValue, int.MaxValue, 0.2, true, true);
            SlideProperties = new SlidableProperties(0, -1, false);
            var rec = new PanGestureRecognizer();
            GestureRecognizers.Add(rec);
            rec.PanUpdated += Rec_PanUpdated;
            Content = m_debug = new Label { Text = "Not started", VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
        }

        private static void OnChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var me = (SlidableLayout)bindable;
            if(me.m_lastId == me.SlideProperties.HoldId)
            {
                return;
            }

            var index = me.SlideProperties.Position;
            var dist = me.CalculateDist(index);
            me.m_debug.Text = "other: " + dist + "\n" + index + "\n" + me.SlideProperties.HoldId + "\n" + me.SlideProperties.IsHeld;
            me.OnScrolledInternal();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            OnScrolledInternal();
        }

        private void Rec_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            var currentId = e.GestureId;
            if (!(m_lastId == SlideProperties.HoldId || !SlideProperties.IsHeld || currentId == m_lastId))
            {
                return;
            }

            if (e.StatusType == GestureStatus.Started)
            {
                //Start tracking time
                m_startSlideLocation = CalculateDist(SlideProperties.Position);
            }

            var currentPos = m_startSlideLocation - e.TotalX;
            if (e.StatusType == GestureStatus.Completed || e.StatusType == GestureStatus.Canceled)
            {
                //Slide more.
                // On click is only for when a tap gesture is done. 
                currentPos = CalculateDist(Math.Floor(SlideProperties.Position));
            }

            m_lastId = currentId;
            var index = CalculateIndex(currentPos);
            SlideProperties = new SlidableProperties(index, m_lastId, e.StatusType != GestureStatus.Completed && e.StatusType != GestureStatus.Canceled);
            m_debug.Text = "me: " + currentPos + "\n" + index + "\n" + currentId + "\n" + e.StatusType;
            OnScrolledInternal();
        }

        private double CalculateIndex(double dist)
        {
            var width = GetItemWidth();
            return (dist-Width/2) / width;
        }

        protected double CalculateDist(double index)
        {
            var width = GetItemWidth();
            return index * width + Width / 2;
        }

        protected double GetItemWidth()
        {
            if (!Config.WidthIsProportional) return Config.ElementWidth;
            return Width * Config.ElementWidth;
        }

        private void OnScrolledInternal()
        {
            if (Width < 0.1) return;
            OnScrolled((SlideProperties.Position), Width / 2 - GetItemWidth());
        }
        protected virtual void OnScrolled(double index, double offset)
        {

        }

        protected virtual void OnClick(int location) // Ha en enable click metode?
        {

        }

        public static readonly BindableProperty ConfigProperty = BindableProperty.Create(
            nameof(Config),
            typeof(SliderConfig),
            typeof(SlidableLayout));

        public SliderConfig Config
        {
            get => (SliderConfig)GetValue(ConfigProperty);
            set => SetValue(ConfigProperty, value);
        }

        public static readonly BindableProperty SlidePropertiesProperty = BindableProperty.Create(
            nameof(SlideProperties),
            typeof(SlidableProperties),
            typeof(SlidableLayout),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: OnChanged);



        public SlidableProperties SlideProperties
        {
            get => (SlidableProperties)GetValue(SlidePropertiesProperty);
            set => SetValue(SlidePropertiesProperty, value);
        }

        public static readonly BindableProperty SelectedItemChangedCommandProperty = BindableProperty.Create(
            nameof(SelectedItemChangedCommand),
            typeof(ICommand),
            typeof(SlidableLayout));

        public ICommand SelectedItemChangedCommand
        {
            get => (ICommand)GetValue(SelectedItemChangedCommandProperty);
            set => SetValue(SelectedItemChangedCommandProperty, value);
        }
    }
}
