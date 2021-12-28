using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using DIPS.Xamarin.UI.Controls.Modality;
using DIPS.Xamarin.UI.Internal.Xaml.Sheet;
using DIPS.Xamarin.UI.Resources.Colors;
using DIPS.Xamarin.UI.Resources.LocalizedStrings;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.Sheet
{
    public partial class SheetBehavior
    {
        /// <summary>
        ///     <see cref="BeforeOpenedCommand" />
        /// </summary>
        public static readonly BindableProperty BeforeOpenedCommandProperty = BindableProperty.Create(
            nameof(BeforeOpenedCommand),
            typeof(ICommand),
            typeof(SheetBehavior));

        /// <summary>
        ///     <see cref="BeforeOpenedCommandParameter" />
        /// </summary>
        public static readonly BindableProperty BeforeOpenedCommandParameterProperty = BindableProperty.Create(
            nameof(BeforeOpenedCommandParameter),
            typeof(object),
            typeof(SheetBehavior));

        /// <summary>
        ///     <see cref="OpenedCommand" />
        /// </summary>
        public static readonly BindableProperty OpenedCommandProperty = BindableProperty.Create(
            nameof(OpenedCommand),
            typeof(ICommand),
            typeof(SheetBehavior));

        /// <summary>
        ///     <see cref="OpenedCommandProperty" />
        /// </summary>
        public static readonly BindableProperty OpenedCommandParameterProperty =
            BindableProperty.Create(nameof(OpenedCommandParameter), typeof(object), typeof(SheetBehavior));

        /// <summary>
        ///     <see cref="BeforeClosedCommand" />
        /// </summary>
        public static readonly BindableProperty BeforeClosedCommandProperty = BindableProperty.Create(
            nameof(BeforeClosedCommand),
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
        ///     <see cref="BeforeClosedCommandParameter" />
        /// </summary>
        public static readonly BindableProperty BeforeClosedCommandParameterProperty = BindableProperty.Create(
            nameof(BeforeClosedCommandParameter),
            typeof(object),
            typeof(SheetBehavior));

        /// <summary>
        ///     <see cref="ClosedCommand" />
        /// </summary>
        public static readonly BindableProperty ClosedCommandProperty = BindableProperty.Create(
            nameof(ClosedCommand),
            typeof(ICommand),
            typeof(SheetBehavior));

        /// <summary>
        ///     <see cref="ClosedCommandParameter" />
        /// </summary>
        public static readonly BindableProperty ClosedCommandParameterProperty = BindableProperty.Create(
            nameof(ClosedCommandParameter),
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
            BindingMode.OneWayToSource);

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
            typeof(SheetBehavior),
            true);

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

        /// <summary>
        ///     <see cref="SnapPoints" />
        /// </summary>
        public static readonly BindableProperty SnapPointsProperty = BindableProperty.Create(
            nameof(SnapPoints),
            typeof(IList<double>),
            typeof(SheetBehavior),
            new List<double> {.0, .5, .98},
            validateValue: ValidateSnapPoints,
            coerceValue: CoerceSnapPoints
        );

        /// <summary>
        ///     <see cref="FlingSpeedThreshold" />
        /// </summary>
        public static readonly BindableProperty FlingSpeedThresholdProperty =
            BindableProperty.Create(nameof(FlingSpeedThreshold), typeof(int), typeof(SheetBehavior), 1000);

        /// <summary>
        ///     <see cref="SheetOpeningStrategy" />
        /// </summary>
        public static readonly BindableProperty SheetOpeningStrategyProperty =
            BindableProperty.Create(nameof(SheetOpeningStrategy), typeof(SheetOpeningStrategyEnum),
                typeof(SheetBehavior),
                SheetOpeningStrategyEnum.MostFittingSnapPoint);

        /// <summary>
        ///     <see cref="InterceptDragGesture " />
        /// </summary>
        public static readonly BindableProperty InterceptDragGestureProperty =
            BindableProperty.Create(nameof(InterceptDragGesture), typeof(bool), typeof(SheetBehavior), true);

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
        ///     Decides when the sheet should fling open or closed based on the speed of the drag gesture.
        ///     This is a bindable property.
        /// </summary>
        /// <remarks>Default value is 1250. Unit is pixels per second. Pre-defined values are <see cref="FlingSensitivity.Low" />, <see cref="FlingSensitivity.Medium" /> and <see cref="FlingSensitivity.High" /></remarks>
        [TypeConverter(typeof(FlingSensitivityConverter))]
        public int FlingSpeedThreshold
        {
            get => (int)GetValue(FlingSpeedThresholdProperty);
            set => SetValue(FlingSpeedThresholdProperty, value);
        }

        /// <summary>
        ///     Decides the position the sheet should open in. See <see cref="SheetOpeningStrategyEnum" />.
        ///     This is a bindable property.
        /// </summary>
        /// <remarks>Default value is <see cref="SheetOpeningStrategyEnum.MostFittingSnapPoint" /></remarks>
        public SheetOpeningStrategyEnum SheetOpeningStrategy
        {
            get => (SheetOpeningStrategyEnum)GetValue(SheetOpeningStrategyProperty);
            set => SetValue(SheetOpeningStrategyProperty, value);
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
        ///     Positions that the sheet should snap to when the drag gesture has ended. 
        ///     This is a bindable property.
        /// </summary>
        /// <remarks>Comma-separated string of doubles, ex: "0, .5, .99". Values will be clamped between 0 and 1. The last snap point decides the maximum position of the sheet. The first snap point decides the pinned position for the sheet. User interaction can NOT drag the sheet below this pinned position. Default value is "0, .5, .98".</remarks>
        [TypeConverter(typeof(SnapPointConverter))]
        public IList<double> SnapPoints
        {
            get => (IList<double>)GetValue(SnapPointsProperty);
            set => SetValue(SnapPointsProperty, value);
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
        public ICommand BeforeOpenedCommand
        {
            get => (ICommand)GetValue(BeforeOpenedCommandProperty);
            set => SetValue(BeforeOpenedCommandProperty, value);
        }

        /// <summary>
        ///     Parameter to pass to <see cref="BeforeOpenedCommand" />.
        ///     This is a bindable property.
        /// </summary>
        public object BeforeOpenedCommandParameter
        {
            get => GetValue(BeforeOpenedCommandParameterProperty);
            set => SetValue(BeforeOpenedCommandParameterProperty, value);
        }

        /// <summary>
        ///     Command that executes when the sheet has completed it's animation and is open.
        ///     This is a bindable property.
        /// </summary>
        public ICommand OpenedCommand
        {
            get => (ICommand)GetValue(OpenedCommandProperty);
            set => SetValue(OpenedCommandProperty, value);
        }

        /// <summary>
        ///     The parameter to pass to the <see cref="OpenedCommand" />.
        ///     This is a bindable property.
        /// </summary>
        public object OpenedCommandParameter
        {
            get => GetValue(OpenedCommandParameterProperty);
            set => SetValue(OpenedCommandParameterProperty, value);
        }

        /// <summary>
        ///     Command that execute before the sheet start it's animation when closing.
        ///     This is a bindable property.
        /// </summary>
        public ICommand BeforeClosedCommand
        {
            get => (ICommand)GetValue(BeforeClosedCommandProperty);
            set => SetValue(BeforeClosedCommandProperty, value);
        }

        /// <summary>
        ///     The parameter to pass to <see cref="BeforeClosedCommand" />.
        ///     This is a bindable property.
        /// </summary>
        public object BeforeClosedCommandParameter
        {
            get => GetValue(BeforeClosedCommandParameterProperty);
            set => SetValue(BeforeClosedCommandParameterProperty, value);
        }

        /// <summary>
        ///     Command that executes when the sheet has completed it's animation and is closed.
        ///     This is a bindable property.
        /// </summary>
        public ICommand ClosedCommand
        {
            get => (ICommand)GetValue(ClosedCommandProperty);
            set => SetValue(ClosedCommandProperty, value);
        }

        /// <summary>
        ///     The parameter to pass to the <see cref="ClosedCommand" />.
        /// </summary>
        public object ClosedCommandParameter
        {
            get => GetValue(ClosedCommandParameterProperty);
            set => SetValue(ClosedCommandParameterProperty, value);
        }

        /// <summary>
        ///     The current position of the sheet.
        ///     This is a bindable property.
        ///     <remarks>This is by default <see cref="BindingMode.OneWayToSource"/>. Setting this will NOT move the sheet. Use <see cref="MoveTo"/> to change the sheet's position.</remarks>
        /// </summary>
        public double Position
        {
            get => (double)GetValue(PositionProperty);
            set => SetValue(PositionProperty, value);
        }

        /// <summary>
        ///     Should the sheet intercept drag gestures done on the content. Will always be disabled in the sheet's maximized
        ///     position.
        ///     This is a bindable property
        ///     <remarks>Default is false</remarks>
        /// </summary>
        public bool InterceptDragGesture
        {
            get => (bool)GetValue(InterceptDragGestureProperty);
            set => SetValue(InterceptDragGestureProperty, value);
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

        private static object CoerceSnapPoints(BindableObject bindable, object value)
        {
            if (value is List<double> snapPoints && snapPoints.Any())
            {
                return snapPoints.OrderBy(d => d).ToList();
            }

            return value;
        }

        private static bool ValidateSnapPoints(BindableObject bindable, object value)
        {
            if (value is not List<double> snapPoints)
            {
                return false;
            }

            if (!snapPoints.Any())
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Event that gets raised when the sheet is about to start it's animation to open.
        /// </summary>
        public event EventHandler? BeforeOpened;

        /// <summary>
        ///     Event that gets raised when the sheet has completed it's animation and is open.
        /// </summary>
        public event EventHandler? Opened;

        /// <summary>
        ///     Event that gets raised before the sheet start it's animation when closing
        /// </summary>
        public event EventHandler? BeforeClosed;

        /// <summary>
        ///     Event that gets raised when the sheet has completed it's animation and is closed.
        /// </summary>
        public event EventHandler? Closed;

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