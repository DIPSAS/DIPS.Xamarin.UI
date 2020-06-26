using System;
using System.Linq;
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
        public void ShowToaster(View toaster = null)
        {
            // get current page
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            if (!(currentPage is ContentPage))
            {
                return;
            }
            
            // arrange view layers
            var oldContent = ((ContentPage)currentPage).Content;
            var newContent = new Grid { BackgroundColor = Color.Red };
            Id = newContent.Id;
            newContent.Children.Add(oldContent);
            
            // display toast view
            var toastView = toaster == null ? GetToast() : toaster;
            newContent.Children.Add(toastView);
            ((ContentPage)currentPage).Content = newContent;
            
        }

        private Toast GetToast()
        {
            var toast = new Toast();
            
            toast.SetBinding(TextProperty, new Binding(nameof(Text), source: this));
            toast.SetBinding(TextColorProperty, new Binding(nameof(TextColor), source: this));

            toast.Text = Text;

            return toast;
        }
        // END: SHOW
        
        // START: BIND PROPERTIES
        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        public Color TextColor
        {
            get => (Color) GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }
        
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(Toaster), Label.TextProperty.DefaultValue);
        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(Toaster),
                Label.TextColorProperty.DefaultValue);
        // END: BIND PROPERTIES
        
        
        public static Guid Id { get; set; }

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