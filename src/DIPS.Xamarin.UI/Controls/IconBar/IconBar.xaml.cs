using System.Collections;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.IconBar
{
    /// <summary>
    ///     A control that displays a collection of items as icons in a single line which can be oriented vertically or
    ///     horizontally.
    /// </summary>
    [ContentProperty(nameof(ItemTemplate))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IconBar : ContentView
    {
        /// <summary>
        ///     Bindable property for <see cref="ItemSource" />
        /// </summary>
        public static readonly BindableProperty ItemSourceProperty =
            BindableProperty.CreateAttached(nameof(ItemSource), typeof(IEnumerable), typeof(IconBar),
                BindableLayout.ItemsSourceProperty.DefaultValue);

        /// <summary>
        ///     Bindable property for <see cref="ItemTemplate" />
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.CreateAttached(nameof(ItemTemplate), typeof(DataTemplate), typeof(IconBar),
                BindableLayout.ItemTemplateProperty.DefaultValue);

        /// <summary>
        ///     Bindable property for <see cref="Orientation" />
        /// </summary>
        public static readonly BindableProperty OrientationProperty = BindableProperty.Create(nameof(Orientation),
            typeof(StackOrientation), typeof(IconBar), StackOrientation.Horizontal,
            propertyChanged: (bindable, oldValue, newValue) => ((IconBar) bindable).InvalidateLayout());

        /// <summary>
        ///     Bindable property for <see cref="Spacing" />
        /// </summary>
        public static readonly BindableProperty SpacingProperty = BindableProperty.Create(nameof(Spacing),
            typeof(double), typeof(IconBar), 5d,
            propertyChanged: (bindable, oldValue, newValue) => ((IconBar) bindable).InvalidateLayout());

        /// <inheritdoc />
        public IconBar()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Gets or sets the source of items to template and display.
        ///     <remarks>
        ///         While any IEnumerable implementer is accepted, any that do not implement IList or IReadOnlyList[+T] (where T is
        ///         a class) will be converted to list by iterating.
        ///         This is a bindable property.
        ///     </remarks>
        /// </summary>
        public IEnumerable ItemSource
        {
            get => (IEnumerable) GetValue(ItemSourceProperty);
            set => SetValue(ItemSourceProperty, value);
        }

        /// <summary>
        ///     Gets or sets the DataTemplate to apply to the <see cref="ItemSource" />.
        ///     <remarks>
        ///         The ItemTemplate is used to define the visual appearance of objects from the <see cref="ItemSource" />. Through
        ///         the item template you can set up data bindings to the user objects supplied to automatically fill in the visual
        ///         and respond to any changes in the user object.
        ///         This is a bindable property.
        ///     </remarks>
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get => (DataTemplate) GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        /// <summary>
        ///     Gets or sets the value which indicates the direction which icons are positioned in the <c>IconBar</c>.
        ///     <remarks>
        ///         Setting the Orientation of a StackLayout triggers a layout cycle if the stack is already inside of a parent
        ///         layout. To prevent wasted layout cycles, set the orientation prior to adding the StackLayout to a parent.
        ///         This is a bindable property.
        ///     </remarks>
        /// </summary>
        public StackOrientation Orientation
        {
            get => (StackOrientation) GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        /// <summary>
        ///     Gets or sets a value which indicates the amount of space between each icon in the <c>IconBar</c>.
        ///     <remarks>
        ///         Setting this value triggers a layout cycle if the StackLayout is already in a parent Layout.
        ///         This is a bindable property.
        ///     </remarks>
        /// </summary>
        public double Spacing
        {
            get => (double) GetValue(SpacingProperty);
            set => SetValue(SpacingProperty, value);
        }
    }
}