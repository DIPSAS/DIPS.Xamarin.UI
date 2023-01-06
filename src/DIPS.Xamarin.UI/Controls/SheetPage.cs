using System;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls
{
    public class SheetPage : ContentPage
    {
        internal readonly ContentPage m_sheetPage = new();
        public SheetPage()
        {
            m_sheetPage.SetBinding(ContentPage.ContentProperty, new Binding(nameof(SheetContent), source: this));
        }
        
        
        public static readonly BindableProperty SheetContentProperty = BindableProperty.Create(
            nameof(SheetContent),
            typeof(View),
            typeof(SheetPage));

        public View SheetContent
        {
            get => (View)GetValue(SheetContentProperty);
            set => SetValue(SheetContentProperty, value);
        }

        public void OpenSheet()
        {
            SheetOpened?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler? SheetOpened;

        public Command OpenSheetCommand => new(OpenSheet);
    }
}