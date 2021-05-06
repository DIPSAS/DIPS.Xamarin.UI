using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DIPS.Xamarin.UI.Controls.Slidable;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Samples.Controls.SlideLayout
{
    public partial class SlideLayoutPage : ContentPage
    {
        public SlideLayoutPage()
        {
            InitializeComponent();
            BindingContext = new SlideLayoutViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((SlideLayoutViewModel)BindingContext).Initialize();
        }

        private async void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            if(sender is Frame view && view.BindingContext is CalendarViewModel calendar && view.Content is Grid grid && grid.Children.OfType<Label>().Any())
            {
                label.Text = grid.Children.OfType<Label>().First().Text;
            }
            frame.FadeTo(1, 150);
            await Task.Delay(1000);
            frame.FadeTo(0, 150);
        }

        private void SlidableLayout_OnPanStarted(object sender, PanEventArgs e)
        {
            if (BindingContext is SlideLayoutViewModel ctx)
            {
                ctx.PanStartedIndex = e.CurrentIndex;
            }
        }

        private void SlidableLayout_OnPanEnded(object sender, PanEventArgs e)
        {
            if (BindingContext is SlideLayoutViewModel ctx)
            {
                ctx.PanEndedIndex = e.CurrentIndex;
            }
        }
    }
}
