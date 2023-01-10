using System;
using System.Windows.Input;
using DIPS.Xamarin.UI.Samples.Controls.BorderBox;
using DIPS.Xamarin.UI.Samples.Controls.Content;
using DIPS.Xamarin.UI.Samples.Controls.DatePicker;
using DIPS.Xamarin.UI.Samples.Controls.FloatingActionMenu;
using DIPS.Xamarin.UI.Samples.Controls.NativeSheet;
using DIPS.Xamarin.UI.Samples.Controls.Popup;
using DIPS.Xamarin.UI.Samples.Controls.RadioButtonGroup;
using DIPS.Xamarin.UI.Samples.Controls.Sheet;
using DIPS.Xamarin.UI.Samples.Controls.Skeleton;
using DIPS.Xamarin.UI.Samples.Controls.SlideLayout;
using DIPS.Xamarin.UI.Samples.Controls.Tag;
using DIPS.Xamarin.UI.Samples.Controls.TimePicker;
using DIPS.Xamarin.UI.Samples.Controls.Toast;
using DIPS.Xamarin.UI.Samples.Controls.TrendGraph;
using DIPS.Xamarin.UI.Samples.Controls.Vibration;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Samples.Controls
{
    public class ControlsViewModel
    {
        private readonly INavigation m_navigation;

        public ControlsViewModel(INavigation navigation)
        {
            m_navigation = navigation;
            NavigateToCommand = new Command<string>(NavigateTo);
        }

        public ICommand NavigateToCommand { get; }

        private async void NavigateTo(string parameter)
        {
            try
            {
                if (parameter.Equals("DatePicker"))
                    await m_navigation.PushAsync(new DatePickerPage {Title = parameter});
                if (parameter.Equals("TimePicker"))
                    await m_navigation.PushAsync(new TimePickerPage {Title = parameter});
                if (parameter.Equals("Popup"))
                    await m_navigation.PushAsync(new PopupPage {Title = parameter});
                if (parameter.Equals("Sheet"))
                    await m_navigation.PushAsync(new Sheet.SheetPage() {Title = parameter});
                if (parameter.Equals("RadioButtonGroup"))
                    await m_navigation.PushAsync(new RadioButtonGroupPage {Title = parameter});
                if (parameter.Equals("TrendGraph"))
                    await m_navigation.PushAsync(new TrendGraphPage {Title = parameter});
                if (parameter.Equals("Sliding"))
                    await m_navigation.PushAsync(new SlideLayoutPage());
                if (parameter.Equals("ContentControl"))
                    await m_navigation.PushAsync(new ContentControlPage {Title = parameter});
                if (parameter.Equals("FloatingActionMenu"))
                    await m_navigation.PushAsync(new FloatingActionMenuPage {Title = parameter});
                if (parameter.Equals("TagControl"))
                    await m_navigation.PushAsync(new TagPage {Title = parameter});
                if (parameter.Equals("ToastControl"))
                    await m_navigation.PushAsync(new ToastPage {Title = parameter});
                if (parameter.Equals("Skeleton"))
                    await m_navigation.PushAsync(new SkeletonPage {Title = parameter});
                if (parameter.Equals("BorderBoxControl"))
                    await m_navigation.PushAsync(new BorderBoxPage {Title = parameter});
                if (parameter.Equals("Vibration"))
                    await m_navigation.PushAsync(new VibrationPage() {Title = parameter});
                if (parameter.Equals("SheetPage"))
                    await m_navigation.PushAsync(new NativeSheetPage() {Title = parameter});
            }
            catch (Exception e)
            {
                await m_navigation.PushAsync(new ContentPage
                {
                    Content = new StackLayout
                    {
                        Children =
                        {
                            new Label {Text = e.Message, Margin = new Thickness(10)},
                            new Label {Text = e.StackTrace, Margin = new Thickness(10)}
                        }
                    }
                });
            }
        }
    }
}