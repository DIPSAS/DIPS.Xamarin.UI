using System;
using System.Windows.Input;
using DIPS.Xamarin.UI.Controls.Modality;
using DIPS.Xamarin.UI.Internal.xaml;
using DIPS.Xamarin.UI.Resources.Colors;
using DIPS.Xamarin.UI.Resources.LocalizedStrings;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.Sheet
{
    public partial class SheetBehavior
    {
        /// <summary>
        ///     <see cref="OnBeforeOpenCommand" />
        /// </summary>
        public static readonly BindableProperty OnBeforeOpenCommandProperty = BindableProperty.Create(
            nameof(OnBeforeOpenCommand),
            typeof(ICommand),
            typeof(SheetBehavior));

        /// <summary>
        ///     <see cref="OnBeforeOpenCommandParameter" />
        /// </summary>
        public static readonly BindableProperty OnBeforeOpenCommandParameterProperty = BindableProperty.Create(
            nameof(OnBeforeOpenCommandParameter),
            typeof(object),
            typeof(SheetBehavior));

        /// <summary>
        ///     <see cref="OnOpenCommand" />
        /// </summary>
        public static readonly BindableProperty OnOpenCommandProperty = BindableProperty.Create(
            nameof(OnOpenCommand),
            typeof(ICommand),
            typeof(SheetBehavior));

        /// <summary>
        ///     <see cref="OnOpenCommandProperty" />
        /// </summary>
        public static readonly BindableProperty OnOpenCommandParameterProperty =
            BindableProperty.Create(nameof(OnOpenCommandParameter), typeof(object), typeof(SheetBehavior));

        /// <summary>
        ///     <see cref="OnBeforeCloseCommand" />
        /// </summary>
        public static readonly BindableProperty OnBeforeCloseCommandProperty = BindableProperty.Create(
            nameof(OnBeforeCloseCommand),
            typeof(ICommand),
            typeof(SheetBehavior));

        /// <summary>
        ///     <see cref="TitleSize" />
        /// </summary>
        public static readonly BindableProperty TitleSizeProperty = BindableProperty.Create(nameof(TitleSize),
            typeof(double), typeof(SheetBehavior),
            defaultValueCreator: b => Device.GetNamedSize(NamedSize.Medium, typeof(Label)));

        /// <summary>
        ///     <see cref="TitleFontAttributes" />
        /// </summary>
        public static readonly BindableProperty TitleFontAttributesProperty =
            BindableProperty.Create(nameof(TitleFontAttributes), typeof(FontAttributes), typeof(SheetBehavior),
                FontAttributes.Bold);

        /// <summary>
        ///     <see cref="CancelButtonSize" />
        /// </summary>
        public static readonly BindableProperty CancelButtonSizeProperty =
            BindableProperty.Create(nameof(CancelButtonSize), typeof(double), typeof(SheetBehavior),
                defaultValueCreator: b => Device.GetNamedSize(NamedSize.Small, typeof(Button)));

        /// <summary>
        ///     <see cref="ActionButtonSize" />
        /// </summary>
        public static readonly BindableProperty ActionButtonSizeProperty =
            BindableProperty.Create(nameof(ActionButtonSize), typeof(double), typeof(SheetBehavior),
                defaultValueCreator: b => Device.GetNamedSize(NamedSize.Small, typeof(Button)));


        /// <summary>
        ///     <see cref="OnBeforeCloseCommandParameter" />
        /// </summary>
        public static readonly BindableProperty OnBeforeCloseCommandParameterProperty = BindableProperty.Create(
            nameof(OnBeforeCloseCommandParameter),
            typeof(object),
            typeof(SheetBehavior));

        /// <summary>
        ///     <see cref="CloseCommand" />
        /// </summary>
        public static readonly BindableProperty CloseCommandProperty = BindableProperty.Create(
            nameof(CloseCommand),
            typeof(ICommand),
            typeof(SheetBehavior));

        /// <summary>
        ///     <see cref="CloseCommandParameter" />
        /// </summary>
        public static readonly BindableProperty OnCloseCommandParameterProperty = BindableProperty.Create(
            nameof(CloseCommandParameter),
            typeof(object),
            typeof(SheetBehavior));

        /// <summary>
        ///     <see cref="VerticalContentAlignment" />
        /// </summary>
        public static readonly BindableProperty VerticalContentAlignmentProperty = BindableProperty.Create(
            nameof(VerticalContentAlignment),
            typeof(ContentAlignment),
            typeof(SheetBehavior),
            ContentAlignment.Fit,
            propertyChanged: OnVerticalContentAlignmentPropertyChanged);

        /// <summary>
        ///     <see cref="ActionTitle" />
        /// </summary>
        public static readonly BindableProperty ActionTitleProperty = BindableProperty.Create(
            nameof(ActionTitle),
            typeof(string),
            typeof(SheetBehavior),
            string.Empty);

        /// <summary>
        ///     <see cref="CancelTitle" />
        /// </summary>
        public static readonly BindableProperty CancelTitleProperty = BindableProperty.Create(
            nameof(CancelTitle),
            typeof(string),
            typeof(SheetBehavior),
            InternalLocalizedStrings.Cancel);

        /// <summary>
        ///     <see cref="TitleSeparatorColor" />
        /// </summary>
        public static readonly BindableProperty TitleSeparatorColorProperty = BindableProperty.Create(
            nameof(TitleSeparatorColor),
            typeof(Color),
            typeof(SheetBehavior),
            ColorPalette.QuinaryLight);

        /// <summary>
        ///     <see cref="IsTitleSeparatorVisible" />
        /// </summary>
        public static readonly BindableProperty IsTitleSeparatorVisibleProperty = BindableProperty.Create(
            nameof(IsTitleSeparatorVisible),
            typeof(bool),
            typeof(SheetBehavior),
            true);

        /// <summary>
        ///     <see cref="TitleColor" />
        /// </summary>
        public static readonly BindableProperty TitleColorProperty = BindableProperty.Create(
            nameof(TitleColor),
            typeof(Color),
            typeof(SheetBehavior),
            Label.TextColorProperty.DefaultValue);

        /// <summary>
        ///     <see cref="Title" />
        /// </summary>
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(SheetBehavior),
            string.Empty);

        /// <summary>
        ///     <see cref="Alignment" />
        /// </summary>
        public static readonly BindableProperty AlignmentProperty = BindableProperty.Create(
            nameof(Alignment),
            typeof(AlignmentOptions),
            typeof(SheetView),
            AlignmentOptions.Bottom);

        /// <summary>
        ///     <see cref="IsOpen" />
        /// </summary>
        public static readonly BindableProperty IsOpenProperty = BindableProperty.Create(
            nameof(IsOpen),
            typeof(bool),
            typeof(SheetView),
            false,
            BindingMode.TwoWay,
            propertyChanged: IsOpenPropertyChanged);

        /// <summary>
        ///     <see cref="OnPositionChangedCommand" />
        /// </summary>
        public static readonly BindableProperty OnPositionChangedCommandProperty = BindableProperty.Create(
            nameof(OnPositionChangedCommand),
            typeof(ICommand),
            typeof(SheetBehavior));

        /// <summary>
        ///     <see cref="ShouldAutoClose" />
        /// </summary>
        public static readonly BindableProperty ShouldAutoCloseProperty =
            BindableProperty.Create(nameof(ShouldAutoClose), typeof(bool), typeof(SheetBehavior), true);

        /// <summary>
        ///     <see cref="ShouldRememberPosition" />
        /// </summary>
        public static readonly BindableProperty ShouldRememberPositionProperty = BindableProperty.Create(
            nameof(ShouldRememberPosition),
            typeof(bool),
            typeof(SheetBehavior),
            false);

        /// <summary>
        ///     <see cref="SheetContent" />
        /// </summary>
        public static readonly BindableProperty SheetContentProperty = BindableProperty.Create(
            nameof(SheetContent),
            typeof(View),
            typeof(SheetView),
            new ContentView {HeightRequest = 100, VerticalOptions = LayoutOptions.Start},
            propertyChanged: OnSheetContentPropertyChanged);

        /// <summary>
        ///     <see cref="SheetContentTemplate" />
        /// </summary>
        public static readonly BindableProperty SheetContentTemplateProperty =
            BindableProperty.Create(nameof(SheetContentTemplate), typeof(DataTemplate), typeof(SheetBehavior));


        /// <summary>
        ///     <see cref="HeaderColor" />
        /// </summary>
        public static readonly BindableProperty HeaderColorProperty = BindableProperty.Create(
            nameof(HeaderColor),
            typeof(Color),
            typeof(SheetBehavior),
            Color.White);

        /// <summary>
        ///     <see cref="ContentColor" />
        /// </summary>
        public static readonly BindableProperty ContentColorProperty = BindableProperty.Create(
            nameof(ContentColor),
            typeof(Color),
            typeof(SheetBehavior),
            Color.White);

        /// <summary>
        ///     <see cref="Position" />
        /// </summary>
        public static readonly BindableProperty PositionProperty = BindableProperty.Create(
            nameof(Position),
            typeof(double),
            typeof(SheetBehavior),
            0.0,
            BindingMode.TwoWay);

        /// <summary>
        ///     <see cref="MaxPosition" />
        /// </summary>
        public static readonly BindableProperty MaxPositionProperty = BindableProperty.Create(
            nameof(MaxPosition),
            typeof(double),
            typeof(SheetBehavior),
            1.0,
            BindingMode.TwoWay);

        /// <summary>
        ///     <see cref="MinPosition" />
        /// </summary>
        public static readonly BindableProperty MinPositionProperty = BindableProperty.Create(
            nameof(MinPosition),
            typeof(double),
            typeof(SheetBehavior),
            0.05,
            BindingMode.TwoWay);

        /// <summary>
        ///     <see cref="BindingContextFactory" />
        /// </summary>
        public static readonly BindableProperty BindingContextFactoryProperty = BindableProperty.Create(
            nameof(BindingContextFactory),
            typeof(Func<object>),
            typeof(SheetBehavior));

        /// <summary>
        ///     <see cref="IsDraggable" />
        /// </summary>
        public static readonly BindableProperty IsDraggableProperty = BindableProperty.Create(
            nameof(IsDraggable),
            typeof(bool),
            typeof(SheetBehavior));

        /// <summary>
        ///     <see cref="HasShadow" />
        /// </summary>
        public static readonly BindableProperty HasShadowProperty = BindableProperty.Create(
            nameof(HasShadow),
            typeof(bool),
            typeof(SheetBehavior),
            false);

        /// <summary>
        ///     <see cref="HandleColor" />
        /// </summary>
        public static readonly BindableProperty HandleColorProperty = BindableProperty.Create(
            nameof(HandleColor),
            typeof(Color),
            typeof(SheetBehavior),
            ColorPalette.QuinaryAir);

        /// <summary>
        ///     <see cref="CloseOnOverlayTapped" />
        /// </summary>
        public static readonly BindableProperty CloseOnOverlayTappedProperty = BindableProperty.Create(
            nameof(CloseOnOverlayTapped),
            typeof(bool),
            typeof(SheetBehavior),
            true);

        /// <summary>
        ///     <see cref="ActionTitleColor" />
        /// </summary>
        public static readonly BindableProperty ActionTitleColorProperty = BindableProperty.Create(
            nameof(ActionTitleColor),
            typeof(Color),
            typeof(SheetBehavior),
            Theme.TealPrimary);

        /// <summary>
        ///     <see cref="CancelTitleColor" />
        /// </summary>
        public static readonly BindableProperty CancelTitleColorProperty = BindableProperty.Create(
            nameof(CancelTitleColor),
            typeof(Color),
            typeof(SheetBehavior),
            Theme.TealPrimary);

        /// <summary>
        ///     <see cref="IsCancelButtonVisible" />
        /// </summary>
        public static readonly BindableProperty IsCancelButtonVisibleProperty = BindableProperty.Create(
            nameof(IsCancelButtonVisible),
            typeof(bool),
            typeof(SheetBehavior),
            false);

        /// <summary>
        ///     <see cref="CancelCommand" />
        /// </summary>
        public static readonly BindableProperty CancelCommandProperty = BindableProperty.Create(
            nameof(CancelCommand),
            typeof(ICommand),
            typeof(SheetBehavior),
            defaultValueCreator: DefaultValueCreator);

        /// <summary>
        ///     <see cref="CancelCommandParameter" />
        /// </summary>
        public static readonly BindableProperty CancelCommandParameterProperty =
            BindableProperty.Create(nameof(CancelCommandParameter), typeof(object), typeof(SheetBehavior));

        /// <summary>
        ///     <see cref="ActionCommand" />
        /// </summary>
        public static readonly BindableProperty ActionCommandProperty = BindableProperty.Create(
            nameof(ActionCommand),
            typeof(ICommand),
            typeof(SheetBehavior));

        /// <summary>
        ///     <see cref="ActionCommandParameter" />
        /// </summary>
        public static readonly BindableProperty ActionCommandParameterProperty =
            BindableProperty.Create(nameof(ActionCommandParameter), typeof(object), typeof(SheetBehavior));

        internal static readonly BindablePropertyKey OpenCommandPropertyKey = BindableProperty.CreateReadOnly(
            nameof(OpenCommand),
            typeof(ICommand),
            typeof(SheetBehavior),
            default,
            BindingMode.OneWayToSource,
            defaultValueCreator: OpenSheetCommandValueCreator);

        internal static readonly BindableProperty OpenCommandProperty = OpenCommandPropertyKey.BindableProperty;
        private ModalityLayout? m_modalityLayout;
        private SheetView? m_sheetView;

        /// <summary>
        ///     The size of the title label.
        ///     This is a bindable property.
        /// </summary>
        /// <remarks>Default value is <see cref="NamedSize.Medium" /></remarks>
        [TypeConverter(typeof(FontSizeConverter))]
        public double TitleSize
        {
            get => (double)GetValue(TitleSizeProperty);
            set => SetValue(TitleSizeProperty, value);
        }


        /// <summary>
        ///     Title font attributes.
        ///     This is a bindable property.
        /// </summary>
        /// <remarks>Default is bold</remarks>
        public FontAttributes TitleFontAttributes
        {
            get => (FontAttributes)GetValue(TitleFontAttributesProperty);
            set => SetValue(TitleFontAttributesProperty, value);
        }

        /// <summary>
        ///     The size of the label of the cancel button.
        ///     This is a bindable property.
        /// </summary>
        /// <remarks>Default value is <see cref="NamedSize.Small" /></remarks>
        [TypeConverter(typeof(FontSizeConverter))]
        public double CancelButtonSize
        {
            get => (double)GetValue(CancelButtonSizeProperty);
            set => SetValue(CancelButtonSizeProperty, value);
        }

        /// <summary>
        ///     The size of the label of the action button.
        ///     This is a bindable property.
        ///     <remarks>Default value is <see cref="NamedSize.Small" /></remarks>
        /// </summary>
        [TypeConverter(typeof(FontSizeConverter))]
        public double ActionButtonSize
        {
            get => (double)GetValue(ActionButtonSizeProperty);
            set => SetValue(ActionButtonSizeProperty, value);
        }


        /// <summary>
        ///     The content template to use when the sheet opens.
        ///     This is a bindable property.
        ///     <remarks><see cref="BindingContextFactory" /> to set the binding context of the datatemplate when the sheet opens</remarks>
        /// </summary>
        public DataTemplate SheetContentTemplate
        {
            get => (DataTemplate)GetValue(SheetContentTemplateProperty);
            set => SetValue(SheetContentTemplateProperty, value);
        }

        /// <summary>
        ///     Parameter passed to <see cref="ActionCommand" />.
        ///     This is a bindable property.
        /// </summary>
        public object ActionCommandParameter
        {
            get => GetValue(ActionCommandParameterProperty);
            set => SetValue(ActionCommandParameterProperty, value);
        }

        /// <summary>
        ///     Gets or sets the command that is invoked when the action button is activated.
        ///     This is a bindable property.
        /// </summary>
        public ICommand ActionCommand
        {
            get => (ICommand)GetValue(ActionCommandProperty);
            set => SetValue(ActionCommandProperty, value);
        }

        /// <summary>
        ///     Parameter passed to <see cref="CancelCommand" />.
        ///     This is a bindable property.
        /// </summary>
        public object CancelCommandParameter
        {
            get => GetValue(CancelCommandParameterProperty);
            set => SetValue(CancelCommandParameterProperty, value);
        }

        /// <summary>
        ///     Gets or sets the command that is invoked when the cancel button is activated.
        ///     This is a bindable property.
        /// </summary>
        public ICommand CancelCommand
        {
            get => (ICommand)GetValue(CancelCommandProperty);
            set => SetValue(CancelCommandProperty, value);
        }

        /// <summary>
        ///     Command to open the sheet. This is not meant to be used in your view model, but you can bind to it with for example
        ///     a button.Command to open the sheet directly in xaml
        /// </summary>
        public ICommand? OpenCommand => (ICommand?)GetValue(OpenCommandProperty);

        /// <summary>
        ///     Determines if the cancel button is visible.
        ///     This is a bindable property.
        /// </summary>
        public bool IsCancelButtonVisible
        {
            get => (bool)GetValue(IsCancelButtonVisibleProperty);
            set => SetValue(IsCancelButtonVisibleProperty, value);
        }

        /// <summary>
        ///     Color of the text in cancel button.
        ///     This is a bindable property.
        /// </summary>
        public Color CancelTitleColor
        {
            get => (Color)GetValue(CancelTitleColorProperty);
            set => SetValue(CancelTitleColorProperty, value);
        }

        /// <summary>
        ///     Color of text in action button.
        ///     This is a bindable property.
        /// </summary>
        public Color ActionTitleColor
        {
            get => (Color)GetValue(ActionTitleColorProperty);
            set => SetValue(ActionTitleColorProperty, value);
        }

        /// <summary>
        ///     Action button text.
        ///     This is a bindable property.
        /// </summary>
        public string ActionTitle
        {
            get => (string)GetValue(ActionTitleProperty);
            set => SetValue(ActionTitleProperty, value);
        }

        /// <summary>
        ///     Cancel button text.
        ///     This is a bindable property.
        /// </summary>
        public string CancelTitle
        {
            get => (string)GetValue(CancelTitleProperty);
            set => SetValue(CancelTitleProperty, value);
        }

        /// <summary>
        ///     Color of the title separator.
        ///     This is a bindable property.
        /// </summary>
        public Color TitleSeparatorColor
        {
            get => (Color)GetValue(TitleSeparatorColorProperty);
            set => SetValue(TitleSeparatorColorProperty, value);
        }

        /// <summary>
        ///     Determines if the separator between the title and the sheet content should be visible.
        ///     This is a bindable property.
        /// </summary>
        public bool IsTitleSeparatorVisible
        {
            get => (bool)GetValue(IsTitleSeparatorVisibleProperty);
            set => SetValue(IsTitleSeparatorVisibleProperty, value);
        }

        /// <summary>
        ///     Color of the text in <see cref="Title" />.
        ///     This is a bindable property.
        /// </summary>
        public Color TitleColor
        {
            get => (Color)GetValue(TitleColorProperty);
            set => SetValue(TitleColorProperty, value);
        }

        /// <summary>
        ///     Title text.
        ///     This is a bindable property.
        /// </summary>
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        /// <summary>
        ///     A command that executes when the position of the sheet changes.
        ///     The command parameter will be the new positional value, same as <see cref="Position" />.
        ///     This is a bindable property.
        /// </summary>
        public ICommand OnPositionChangedCommand
        {
            get => (ICommand)GetValue(OnPositionChangedCommandProperty);
            set => SetValue(OnPositionChangedCommandProperty, value);
        }

        /// <summary>
        ///     Determines if the sheet should auto close at the minimum position
        ///     This is a bindable property.
        /// </summary>
        public bool ShouldAutoClose
        {
            get => (bool)GetValue(ShouldAutoCloseProperty);
            set => SetValue(ShouldAutoCloseProperty, value);
        }

        /// <summary>
        ///     Determines if the sheet should remember its position when it is opened and closed.
        ///     This is a bindable property.
        /// </summary>
        public bool ShouldRememberPosition
        {
            get => (bool)GetValue(ShouldRememberPositionProperty);
            set => SetValue(ShouldRememberPositionProperty, value);
        }

        /// <summary>
        ///     Determines the position of the sheet when it appears.
        ///     This is a bindable property.
        /// </summary>
        public AlignmentOptions Alignment
        {
            get => (AlignmentOptions)GetValue(AlignmentProperty);
            set => SetValue(AlignmentProperty, value);
        }

        /// <summary>
        ///     Determines the background color of the header part of the sheet.
        ///     This is a bindable property.
        /// </summary>
        public Color HeaderColor
        {
            get => (Color)GetValue(HeaderColorProperty);
            set => SetValue(HeaderColorProperty, value);
        }

        /// <summary>
        ///     Determines the background color of the content part of the sheet.
        ///     This is a bindable property.
        /// </summary>
        public Color ContentColor
        {
            get => (Color)GetValue(ContentColorProperty);
            set => SetValue(ContentColorProperty, value);
        }

        /// <summary>
        ///     Used to set the binding context of the content of the sheet when the sheet opens.
        ///     This is a bindable property.
        /// </summary>
        public Func<object> BindingContextFactory
        {
            get => (Func<object>)GetValue(BindingContextFactoryProperty);
            set => SetValue(BindingContextFactoryProperty, value);
        }

        /// <summary>
        ///     Determines the color of the handle in the sheet.
        ///     This is a bindable property.
        /// </summary>
        public Color HandleColor
        {
            get => (Color)GetValue(HandleColorProperty);
            set => SetValue(HandleColorProperty, value);
        }

        /// <summary>
        ///     Determines if the sheet should have shadow.
        ///     This is a bindable property
        /// </summary>
        /// <remarks>This only works for iOS.</remarks>
        public bool HasShadow
        {
            get => (bool)GetValue(HasShadowProperty);
            set => SetValue(HasShadowProperty, value);
        }

        /// <summary>
        ///     Determines if the sheet should be draggable or not.
        ///     This is a bindable property.
        /// </summary>
        public bool IsDraggable
        {
            get => (bool)GetValue(IsDraggableProperty);
            set => SetValue(IsDraggableProperty, value);
        }

        /// <summary>
        ///     Determines if the sheet should be visible or not.
        ///     This is a bindable property.
        /// </summary>
        public bool IsOpen
        {
            get => (bool)GetValue(IsOpenProperty);
            set => SetValue(IsOpenProperty, value);
        }

        /// <summary>
        ///     Command that execute when the sheet is about to start it's animation to open.
        ///     This is a bindable property.
        /// </summary>
        public ICommand OnBeforeOpenCommand
        {
            get => (ICommand)GetValue(OnBeforeOpenCommandProperty);
            set => SetValue(OnBeforeOpenCommandProperty, value);
        }

        /// <summary>
        ///     Parameter to pass to <see cref="OnBeforeOpenCommand" />.
        ///     This is a bindable property.
        /// </summary>
        public object OnBeforeOpenCommandParameter
        {
            get => GetValue(OnBeforeOpenCommandParameterProperty);
            set => SetValue(OnBeforeOpenCommandParameterProperty, value);
        }

        /// <summary>
        ///     Command that executes when the sheet has completed it's animation and is open.
        ///     This is a bindable property.
        /// </summary>
        public ICommand OnOpenCommand
        {
            get => (ICommand)GetValue(OnOpenCommandProperty);
            set => SetValue(OnOpenCommandProperty, value);
        }

        /// <summary>
        ///     The parameter to pass to the <see cref="OnOpenCommand" />.
        ///     This is a bindable property.
        /// </summary>
        public object OnOpenCommandParameter
        {
            get => GetValue(OnOpenCommandParameterProperty);
            set => SetValue(OnOpenCommandParameterProperty, value);
        }

        /// <summary>
        ///     Command that execute before the sheet start it's animation when closing.
        ///     This is a bindable property.
        /// </summary>
        public ICommand OnBeforeCloseCommand
        {
            get => (ICommand)GetValue(OnBeforeCloseCommandProperty);
            set => SetValue(OnBeforeCloseCommandProperty, value);
        }

        /// <summary>
        ///     The parameter to pass to <see cref="OnBeforeCloseCommand" />.
        ///     This is a bindable property.
        /// </summary>
        public object OnBeforeCloseCommandParameter
        {
            get => GetValue(OnBeforeCloseCommandParameterProperty);
            set => SetValue(OnBeforeCloseCommandParameterProperty, value);
        }

        /// <summary>
        ///     Command that executes when the sheet has completed it's animation and is closed.
        ///     This is a bindable property.
        /// </summary>
        public ICommand CloseCommand
        {
            get => (ICommand)GetValue(CloseCommandProperty);
            set => SetValue(CloseCommandProperty, value);
        }

        /// <summary>
        ///     The parameter to pass to the <see cref="CloseCommand" />.
        /// </summary>
        public object CloseCommandParameter
        {
            get => GetValue(OnCloseCommandParameterProperty);
            set => SetValue(OnCloseCommandParameterProperty, value);
        }

        /// <summary>
        ///     Determines the maximum position of the sheet when it is visible.
        ///     This is a bindable property.
        /// </summary>
        /// <remarks>This will affect the size of the sheet if <see cref="Position" /> is set to 0</remarks>
        /// <remarks>This will affect the people that are dragging the sheet</remarks>
        /// <remarks>The value have to be between 0 and 1.0 (percentage of the screen)</remarks>
        public double MaxPosition
        {
            get => (double)GetValue(MaxPositionProperty);
            set => SetValue(MaxPositionProperty, value);
        }

        /// <summary>
        ///     Determines the minimum position of the sheet.
        ///     This is a bindable property.
        /// </summary>
        /// <remarks>
        ///     This position is used to determine where the sheet will auto close if <see cref="ShouldAutoClose" /> is set to
        ///     true
        /// </remarks>
        /// <remarks>
        ///     This position is used to determine where the sheet will snap to when <see cref="ShouldAutoClose" /> is set to
        ///     false
        /// </remarks>
        /// <remarks>This will affect the size of the sheet if <see cref="Position" /> is set to 0</remarks>
        /// <remarks>The value have to be between 0 and 1.0 (percentage of the screen)</remarks>
        public double MinPosition
        {
            get => (double)GetValue(MinPositionProperty);
            set => SetValue(MinPositionProperty, value);
        }

        /// <summary>
        ///     Determines the position of the sheet when it is visible.
        ///     This is a bindable property.
        ///     <remarks>This will affect the size of the sheet if <see cref="Position" /> is set to 0</remarks>
        ///     <remarks>The value have to be between 0 and 1.0 (percentage of the screen)</remarks>
        /// </summary>
        public double Position
        {
            get => (double)GetValue(PositionProperty);
            set => SetValue(PositionProperty, value);
        }

        /// <summary>
        ///     The content of the sheet.
        ///     This is a bindable property.
        ///     <remarks><see cref="BindingContextFactory" /> to set the binding context when the sheet opens</remarks>
        /// </summary>
        public View? SheetContent
        {
            get => (View)GetValue(SheetContentProperty);
            set => SetValue(SheetContentProperty, value);
        }

        /// <summary>
        ///     Determines how the content of the sheet should align.
        ///     This is a bindable property.
        /// </summary>
        public ContentAlignment VerticalContentAlignment
        {
            get => (ContentAlignment)GetValue(VerticalContentAlignmentProperty);
            set => SetValue(VerticalContentAlignmentProperty, value);
        }

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        public bool CloseOnOverlayTapped
        {
            get => (bool)GetValue(CloseOnOverlayTappedProperty);
            set => SetValue(CloseOnOverlayTappedProperty, value);
        }

        /// <summary>
        ///     Event that gets raised when the sheet has changed it's position.
        /// </summary>
        public event EventHandler<PositionEventArgs>? PositionChanged;

        /// <summary>
        ///     Event that gets raised when the sheet is about to start it's animation to open.
        /// </summary>
        public event EventHandler? BeforeOpen;

        /// <summary>
        ///     Event that gets raised when the sheet has completed it's animation and is open.
        /// </summary>
        public event EventHandler? Open;

        /// <summary>
        ///     Event that gets raised before the sheet start it's animation when closing
        /// </summary>
        public event EventHandler? BeforeClose;

        /// <summary>
        ///     Event that gets raised when the sheet has completed it's animation and is closed.
        /// </summary>
        public event EventHandler? Close;

        /// <summary>
        ///     Event that gets raised when the user has clicked the action button.
        /// </summary>
        public event EventHandler? ActionClicked;

        /// <summary>
        ///     Event that gets raised when the user has clicked the cancel button.
        /// </summary>
        public event EventHandler? CancelClicked;
    }
}