using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.Toast
{
    public class Toaster : BindableObject
    {
        // START: CURRENT
        private Toaster()
        {
        }
        
        public static Toaster Current { get; } = new Toaster();
        // END: CURRENT
        
        // START: SHOW
        public static Guid Id { get; set; }
        
        public async Task ShowToaster(View toaster = null)
        {
            // get current page
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            if (!(currentPage is ContentPage))
            {
                return;
            }
            
            // arrange view layers
            var oldContent = ((ContentPage)currentPage).Content;
            var newContent = new Grid() {BackgroundColor = Color.Transparent};
            Id = newContent.Id;
            newContent.Children.Add(oldContent);
            
            // display toast view
            var toastView = toaster == null ? GetToast() : toaster;
            toastView.Opacity = 0;
            newContent.Children.Add(toastView);
            ((ContentPage)currentPage).Content = newContent;
            
            // animate toast
            await toastView.FadeTo(1, 750, Easing.Linear);
            
            // hide toast
            _ = HideToasterIn();
        }

        private async Task HideToasterIn(int timeInSeconds = 5)
        {
            await Task.Delay(timeInSeconds * 1000);
            
            await HideToaster();
        }
        
        public async Task HideToaster()
        {
            // get current page
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            if (!(currentPage is ContentPage))
            {
                return;
            }
            
            // get toast container grid
            var toastContent = ((ContentPage)currentPage).Content;
            if (toastContent.Id != Id || !(toastContent is Grid))
            {
                return;
            }
            
            // get toast view
            var toastGrid = (Grid)toastContent;
            var toastView = toastGrid.Children.Last();
            
            // animate toast
            await toastView.FadeTo(0, 750, Easing.Linear);
            
            // remove toast
            toastGrid.Children.Remove(toastView);
        }

        private Toast GetToast()
        {
            var toast = new Toast();
            
            toast.SetBinding(Toast.TextProperty, new Binding(nameof(Text), source: this));
            toast.SetBinding(Toast.FontSizeProperty, new Binding(nameof(FontSize), source: this));
            toast.SetBinding(Toast.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
            toast.SetBinding(Toast.TextColorProperty, new Binding(nameof(TextColor), source: this));
            toast.SetBinding(Toast.BackgroundColorProperty, new Binding(nameof(BackgroundColor), source: this));
            toast.SetBinding(Toast.CornerRadiusProperty, new Binding(nameof(CornerRadius), source: this));
            toast.SetBinding(Toast.PaddingProperty, new Binding(nameof(Padding), source: this));
            toast.SetBinding(Toast.PositionYProperty, new Binding(nameof(PositionY), source: this));

            return toast;
        }
        // END: SHOW
        
        // START: BIND PROPERTIES
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        [TypeConverter(typeof(FontSizeConverter))]
        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }
        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }
        public new Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }
        public float CornerRadius
        {
            get => (float)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
        
        public new Thickness Padding
        {
            get => (Thickness) GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }

        public new double PositionY
        {
            get => (double) GetValue(PositionYProperty);
            set => SetValue(PositionYProperty, value);
        }
        
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(Toaster), Label.TextProperty.DefaultValue);

        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(nameof(FontSize), typeof(double), typeof(Toaster),
                Label.FontSizeProperty.DefaultValue,
                defaultValueCreator: FontSizeDefaultValueCreator);

        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(Toaster),
                Label.FontFamilyProperty.DefaultValue);

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(Toaster),
                Label.TextColorProperty.DefaultValue);

        public static new readonly BindableProperty BackgroundColorProperty =
            BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(Toaster), Color.Default);

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius),
            typeof(float), typeof(Toaster), -1f,
            validateValue: OnCornerRadiusValidate);
        
        public new static readonly BindableProperty PaddingProperty =
            BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(Toaster), new Thickness(5, 5, 5, 5));
        
        public new static readonly BindableProperty PositionYProperty =
            BindableProperty.Create(nameof(PositionY), typeof(double), typeof(Toaster), 10d);
        
        private static object FontSizeDefaultValueCreator(BindableObject bindable)
        {
            return Device.GetNamedSize(NamedSize.Default, typeof(Toast));
        }
        private static bool OnCornerRadiusValidate(BindableObject bindable, object value)
        {
            if (value is float f)
            {
                return (int)f == -1 || f >= 0f;
            }

            return false;
        }
        // END: BIND PROPERTIES
        
        

        public void RemoveToast()
        {
            var lastItem = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            if (lastItem != null && lastItem is ContentPage contentPage)
            {
                var content = contentPage.Content;
                if (content.Id == Id && content is Grid gridContent)
                {
                    gridContent.Children.RemoveAt(gridContent.Children.Count - 1);
                }
            }
        }
        
        public void ShowToast()
        {
            // var count = Application.Current.MainPage.Navigation.NavigationStack.Count;
            var lastItem = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            if (lastItem != null && lastItem is ContentPage contentPage)
            {
                var content = contentPage.Content;
                if (content.Id == Id)
                {
                    return;
                }
                
                var newContent = new Grid { BackgroundColor = Color.Red };
                Id = newContent.Id;
                newContent.Children.Add(content);
                
                var toast = new Label
                {
                    Text = "Hello Moon", TextColor = Color.White, FontSize = 24, FontAttributes = FontAttributes.Bold, BackgroundColor = Color.Black, Padding = new Thickness(20),
                    VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.Center,
                    Margin = new Thickness(0,40,0,0)
                };
                var tapGesture = new TapGestureRecognizer();
                tapGesture.Tapped += (s, e) =>
                {
                    RemoveToast();
                };
                toast.GestureRecognizers.Add(tapGesture);
                newContent.Children.Add(toast);
                
                contentPage.Content = newContent;
            }
            
            // var content = ((ContentPage) Application.Current.MainPage.Navigation.NavigationStack.Last()).Content;
            // var newContent = new Grid {BackgroundColor = Color.Red};
            // newContent.Children.Add(content);
            //
            // ((ContentPage) Application.Current.MainPage.Navigation.NavigationStack.Last()).Content = newContent;
            
            // var count = Application.Current.MainPage.Navigation.NavigationStack.Count;
            // var content = ((ContentPage) Application.Current.MainPage.Navigation.NavigationStack[count - 1]).Content;
            // var newContent = new Grid {BackgroundColor = Color.Red};
            // newContent.Children.Add(content);
            //
            // ((ContentPage) Application.Current.MainPage.Navigation.NavigationStack[count - 1]).Content = newContent;
        }
    }
}