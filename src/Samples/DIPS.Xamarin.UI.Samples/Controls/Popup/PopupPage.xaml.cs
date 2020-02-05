using System;
using System.ComponentModel;
using System.Windows.Input;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Samples.Controls.Popup
{
    public partial class PopupPage : ContentPage
    {
        public PopupPage()
        {
            InitializeComponent();

            BindingContext = new PopupPageViewModel();
        }
    }

    public class TemplateSelector : DataTemplateSelector
    {
        public DataTemplate? T1 { get; set; }
        public DataTemplate? T2 { get; set; }
        protected override DataTemplate? OnSelectTemplate(object item, BindableObject container)
        {
            return (item as bool?) == true ? T1 : T2;
        }
    }
}
