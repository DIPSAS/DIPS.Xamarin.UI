using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.IconBar
{
    [ContentProperty(nameof(ItemTemplate))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IconBar : ContentView
    {
        public IconBar()
        {
            InitializeComponent();
        }
        
        public static readonly BindableProperty SourceProperty =
            BindableProperty.CreateAttached(nameof(Source), typeof(IEnumerable), typeof(IconBar), BindableLayout.ItemsSourceProperty.DefaultValue);

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(IconBar), Label.TextProperty.DefaultValue);
        
        public static readonly BindableProperty IconColorProperty =
            BindableProperty.Create(nameof(IconColor), typeof(Color), typeof(IconBar),
                Label.TextColorProperty.DefaultValue);
        
        
        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.CreateAttached(nameof(ItemTemplate), typeof(DataTemplate), typeof(IconBar), BindableLayout.ItemTemplateProperty.DefaultValue);
        
        public IEnumerable Source
        {
            get => (IEnumerable) GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }
        
        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        
        public Color IconColor
        {
            get => (Color) GetValue(IconColorProperty);
            set => SetValue(IconColorProperty, value);
        }
        
        public DataTemplate ItemTemplate
        {
            get => (DataTemplate) GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }
    }
}