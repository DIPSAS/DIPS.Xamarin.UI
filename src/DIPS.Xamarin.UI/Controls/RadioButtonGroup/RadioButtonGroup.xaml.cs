using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using DIPS.Xamarin.UI.Controls.RadioButtonGroup.Abstractions;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.RadioButtonGroup
{
    /// <summary>
    ///     An vertical oriented radio button group
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RadioButtonGroup : ContentView, IHandleRadioButtons
    {
        private readonly IList<RadioButton> m_radioButtons = new List<RadioButton>();
        private PropertyInfo? m_displayMember;

        /// <summary>
        ///     <see cref="SelectedColor" />
        /// </summary>
        public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(
            nameof(SelectedColor),
            typeof(Color),
            typeof(RadioButtonGroup),
            Color.Black,
            BindingMode.OneWay,
            propertyChanged: OnSelectedColorPropertyChanged);

        /// <summary>
        ///     <see cref="DeSelectedColor" />
        /// </summary>
        public static readonly BindableProperty DeSelectedColorProperty = BindableProperty.Create(
            nameof(DeSelectedColor),
            typeof(Color),
            typeof(RadioButtonGroup),
            Color.Black,
            BindingMode.OneWay,
            propertyChanged: OnDeSelectedColorPropertyChanged);

        /// <summary>
        ///     <see cref="SeparatorColor" />
        /// </summary>
        public static readonly BindableProperty SeparatorColorProperty = BindableProperty.Create(
            nameof(SeparatorColor),
            typeof(Color),
            typeof(RadioButtonGroup),
            Color.Black,
            BindingMode.OneWay);

        /// <summary>
        ///     <see cref="ItemsSource" />
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            nameof(ItemsSource),
            typeof(IList),
            typeof(Picker),
            default(IList),
            propertyChanged: OnItemsSourcePropertyChanged);

        /// <summary>
        ///     <see cref="SelectedItemChangedCommand" />
        /// </summary>
        public static readonly BindableProperty SelectedItemChangedCommandProperty = BindableProperty.Create(
            nameof(SelectedItemChangedCommand),
            typeof(ICommand),
            typeof(RadioButtonGroup));

        /// <summary>
        ///     <see cref="DisplayMemberPath" />
        /// </summary>
        public static readonly BindableProperty DisplayMemberPathProperty = BindableProperty.Create(
            nameof(DisplayMemberPath),
            typeof(string),
            typeof(RadioButtonGroup),
            string.Empty);

        /// <summary>
        ///     <see cref="SelectedItem" />
        /// </summary>
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
            nameof(SelectedItem),
            typeof(object),
            typeof(RadioButtonGroup),
            null,
            BindingMode.TwoWay,
            propertyChanged: OnIsSelectedPropertyChanged);

        /// <summary>
        ///     Constructs an radio button group
        /// </summary>
        public RadioButtonGroup()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     The color for each radio button when it is not selected
        ///     This is a bindable property
        /// </summary>
        public Color DeSelectedColor
        {
            get => (Color)GetValue(DeSelectedColorProperty);
            set => SetValue(DeSelectedColorProperty, value);
        }

        /// <summary>
        ///     The member path to use for the label for each radio button. This have to point at a property in object of
        ///     <see cref="ItemsSource" />
        ///     This is a bindable property
        /// </summary>
        /// <remarks>If this is not set, the ToString() of each item in the <see cref="ItemsSource" /> will be used</remarks>
        public string DisplayMemberPath
        {
            get => (string)GetValue(DisplayMemberPathProperty);
            set => SetValue(DisplayMemberPathProperty, value);
        }

        /// <summary>
        ///     The collection of items that should be used for each radio button, this should have a property that have to be
        ///     pointed at by the <see cref="DisplayMemberPath" />
        ///     This is a bindable property
        /// </summary>
        public IList ItemsSource
        {
            get => (IList)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        /// <summary>
        ///     The color for each radio button when it is selected
        ///     This is a bindable property
        /// </summary>
        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        /// <summary>
        ///     The selected item from the <see cref="ItemsSource" /> list that the user clicked. This can be set programatically
        ///     to set an initial value.
        ///     This is a bindable property
        ///     <remarks>Setting this will trigger <see cref="SelectedItemChangedCommand" /></remarks>
        ///     <remarks>Can be set to null if you delete the item that you have selected</remarks>
        /// </summary>
        public object? SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        /// <summary>
        ///     Command that triggers when the user clicks an radio button group or when the <see cref="SelectedItem" /> changes.
        ///     <remarks>The command will send the object from the <see cref="ItemsSource" /> as an parameter</remarks>
        ///     This is a bindable property
        /// </summary>
        public ICommand SelectedItemChangedCommand
        {
            get => (ICommand)GetValue(SelectedItemChangedCommandProperty);
            set => SetValue(SelectedItemChangedCommandProperty, value);
        }

        /// <summary>
        ///     The color of the separator that separates each radio button
        ///     This is a bindable property
        /// </summary>
        public Color SeparatorColor
        {
            get => (Color)GetValue(SeparatorColorProperty);
            set => SetValue(SeparatorColorProperty, value);
        }

        void IHandleRadioButtons.OnRadioButtonTapped(RadioButton tappedRadioButton)
        {
            if (SelectedItem == tappedRadioButton.Identifier) return;

            var selectedObject = ItemsSource.Cast<object>().FirstOrDefault(item => tappedRadioButton.Identifier == item);

            if (selectedObject == null) return;

            SelectedItem = selectedObject;
        }

        private static void OnItemsSourcePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is RadioButtonGroup radioButtonGroup)) return;

            var oldNotifyCollectionChanged = oldvalue as INotifyCollectionChanged;
            if (oldNotifyCollectionChanged != null)
            {
                oldNotifyCollectionChanged.CollectionChanged -= radioButtonGroup.CollectionChanged;
            }

            var newItemsSource = newvalue as IEnumerable;
            if (newItemsSource == null) return;

            if (newItemsSource is INotifyCollectionChanged newNotifyCollectionChanged)
            {
                newNotifyCollectionChanged.CollectionChanged += radioButtonGroup.CollectionChanged;
            }

            radioButtonGroup.Initialize(newItemsSource);
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Initialize(ItemsSource);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    Initialize(ItemsSource);
                    break;
            }

            if (radioButtonContainer.Children.Count == 0) firstSeparator.IsVisible = false;
        }

        private static void OnSelectedColorPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is RadioButtonGroup radioButtonGroup)) return;
            if (!(newvalue is Color newColor)) return;

            radioButtonGroup.m_radioButtons.ForEach(
                rb =>
                {
                    rb.SelectedColor = newColor;
                    rb.RefreshColor(rb.IsSelected);
                });
        }

        private static void OnDeSelectedColorPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is RadioButtonGroup radioButtonGroup))
                return;
            if (!(newvalue is Color newColor))
                return;

            radioButtonGroup.m_radioButtons.ForEach(
                rb =>
                {
                    rb.DeSelectedColor = newColor;
                    rb.RefreshColor(rb.IsSelected);
                });
        }

        private void Initialize(IEnumerable newItems)
        {
            m_radioButtons.Clear();
            radioButtonContainer.Children.Clear();

            foreach (var item in newItems)
            {
                Add(item, radioButtonContainer.Children.Count);
            }

            if (SelectedItem != null)
            {
                var selectedRadioButton = m_radioButtons.FirstOrDefault(rb => rb.Identifier == SelectedItem);
                if (selectedRadioButton != null)
                {
                    selectedRadioButton.IsSelected = true;
                }
                else
                {
                    SelectedItem = null;
                }
            }
        }

        private void Add(object item, int row)
        {
            if (!firstSeparator.IsVisible)
            {
                firstSeparator.IsVisible = true;
            }

            //Create inner grid and button + separator
            var grid = new Grid() { RowSpacing = 0 };
            var radioButton = new RadioButton() { Text = item.GetPropertyValue(DisplayMemberPath), Identifier = item };
            var separator = CreateSeparator();

            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            Grid.SetRow(radioButton, 0);
            Grid.SetRow(separator, 1);
            grid.Children.Add(radioButton);
            grid.Children.Add(separator);

            //Initialize radio button
            radioButton.Initialize(this);

            //Set colors for each radiobutton
            radioButton.SelectedColor = SelectedColor;
            radioButton.DeSelectedColor = DeSelectedColor;
            radioButton.RefreshColor(radioButton.IsSelected);

            //Set padding for each button
            radioButton.Padding = new Thickness(0, 15, 0, 15);

            //Add each radiobutton to the FlexLayout
            m_radioButtons.Add(radioButton);

            //Add inner grid to outer grid
            radioButtonContainer.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

            Grid.SetRow(grid, row);
            radioButtonContainer.Children.Add(grid);
        }

        private BoxView CreateSeparator()
        {
            var separator = new BoxView() { HeightRequest = 1 };
            separator.SetBinding(BackgroundColorProperty, new Binding(nameof(SeparatorColor), source: this));
            return separator;
        }

        private static void OnIsSelectedPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is RadioButtonGroup radioButtonGroup)) return;
            if (newvalue == null) return;

            var selectedRadioButton = radioButtonGroup.m_radioButtons.SingleOrDefault(radioButton => radioButton.Identifier == newvalue);
            var unSelectedRadioButton = radioButtonGroup.m_radioButtons.SingleOrDefault(radioButton => radioButton.Identifier == oldvalue);

            if (unSelectedRadioButton != null) unSelectedRadioButton.IsSelected = false;
            if (selectedRadioButton != null) selectedRadioButton.IsSelected = true;

            radioButtonGroup.SelectedItemChangedCommand?.Execute(newvalue);
        }
    }
}