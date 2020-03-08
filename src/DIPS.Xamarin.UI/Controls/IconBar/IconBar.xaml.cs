using System.Collections;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.IconBar
{
    [ContentProperty(nameof(ItemTemplate))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IconBar : ContentView
    {
        public static readonly BindableProperty SourceProperty =
            BindableProperty.CreateAttached(nameof(Source), typeof(IEnumerable), typeof(IconBar),
                BindableLayout.ItemsSourceProperty.DefaultValue);

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.CreateAttached(nameof(ItemTemplate), typeof(DataTemplate), typeof(IconBar),
                BindableLayout.ItemTemplateProperty.DefaultValue);

        public static readonly BindableProperty OrientationProperty = BindableProperty.Create(nameof(Orientation),
            typeof(StackOrientation), typeof(IconBar), StackOrientation.Horizontal,
            propertyChanged: (bindable, oldValue, newValue) => ((IconBar) bindable).InvalidateLayout());

        public static readonly BindableProperty SpacingProperty = BindableProperty.Create(nameof(Spacing),
            typeof(double), typeof(IconBar), 5d,
            propertyChanged: (bindable, oldValue, newValue) => ((IconBar) bindable).InvalidateLayout());

        public IconBar()
        {
            InitializeComponent();
        }

        public IEnumerable Source
        {
            get => (IEnumerable) GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate) GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public StackOrientation Orientation
        {
            get => (StackOrientation) GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        public double Spacing
        {
            get => (double) GetValue(SpacingProperty);
            set => SetValue(SpacingProperty, value);
        }
    }
}