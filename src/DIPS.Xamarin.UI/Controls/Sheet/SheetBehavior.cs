using System;
using System.Threading.Tasks;
using System.Windows.Input;
using DIPS.Xamarin.UI.Controls.Modality;
using DIPS.Xamarin.UI.Internal.xaml;
using DIPS.Xamarin.UI.Resources.Colors;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.Sheet
{
    /// <summary>
    /// A behavior to that can be added to a <see cref="ModalityLayout"/> to display a sheet that animates into the view, either from top or bottom, when <see cref="IsOpen"/> is set.
    /// </summary>
    [ContentProperty(nameof(SheetContent))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class SheetBehavior : Behavior<ModalityLayout>, IModalityHandler
    {
        private bool m_fromIsOpenContext;
        private ModalityLayout? m_modalityLayout;

        private SheetView? m_sheetView;
        public event EventHandler? OnOpen;
        public event EventHandler? OnClose;
        /// <summary>
        /// <see cref="OnOpenCommand"/>
        /// </summary>
        public static readonly BindableProperty OnOpenCommandProperty = BindableProperty.Create(
            nameof(OnOpenCommand),
            typeof(ICommand),
            typeof(SheetBehavior));

        /// <summary>
        /// <see cref="OnOpenCommandProperty"/>
        /// </summary>
        public static readonly BindableProperty OnOpenCommandParameterProperty =
            BindableProperty.Create(nameof(OnOpenCommandParameter),
                typeof(object),
                typeof(SheetBehavior));

        /// <summary>
        /// <see cref="OnCloseCommand"/>
        /// </summary>
        public static readonly BindableProperty OnCloseCommandProperty = BindableProperty.Create(
            nameof(OnCloseCommand),
            typeof(ICommand),
            typeof(SheetBehavior));

        /// <summary>
        /// <see cref="OnCloseCommandParameter"/>
        /// </summary>
        public static readonly BindableProperty OnCloseCommandParameterProperty = BindableProperty.Create(
            nameof(OnCloseCommandParameter),
            typeof(object),
            typeof(SheetBehavior));

        /// <summary>
        ///     <see cref="VerticalContentAlignment" />
        /// </summary>
        public static readonly BindableProperty VerticalContentAlignmentProperty = BindableProperty.Create(
            nameof(VerticalContentAlignment),
            typeof(ContentAlignment),
            typeof(SheetBehavior),
            ContentAlignment.Fit);

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
        ///     <see cref="SheetContent" />
        /// </summary>
        public static readonly BindableProperty SheetContentProperty = BindableProperty.Create(
            nameof(SheetContent),
            typeof(View),
            typeof(SheetView),
            new ContentView() { HeightRequest = 100, VerticalOptions = LayoutOptions.Start });

        /// <summary>
        ///     <see cref="BackgroundColor" />
        /// </summary>
        public static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(
            nameof(BackgroundColor),
            typeof(Color),
            typeof(SheetView),
            Color.White);

        /// <summary>
        ///     <see cref="Position" />
        /// </summary>
        public static readonly BindableProperty PositionProperty = BindableProperty.Create(
            nameof(Position),
            typeof(double),
            typeof(SheetBehavior),
            0.0,
            BindingMode.TwoWay,
            propertyChanged: OnPositionPropertyChanged);

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
            0.1,
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

        private bool m_fromIsDraggingContext;

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
        ///     Determines the color of the background of the sheet.
        ///     This is a bindable property.
        /// </summary>
        public Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
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
        /// Command that executes when the sheet has completed it's animation and is open
        /// </summary>
        public ICommand OnOpenCommand
        {
            get => (ICommand)GetValue(OnOpenCommandProperty);
            set => SetValue(OnOpenCommandProperty, value);
        }

        /// <summary>
        /// The parameter to pass to the <see cref="OnOpenCommand"/>
        /// </summary>
        public object OnOpenCommandParameter
        {
            get => GetValue(OnOpenCommandParameterProperty);
            set => SetValue(OnOpenCommandParameterProperty, value);
        }

        /// <summary>
        /// Command that executes when the sheet has completed it's animation and is closed
        /// </summary>
        public ICommand OnCloseCommand
        {
            get => (ICommand)GetValue(OnCloseCommandProperty);
            set => SetValue(OnCloseCommandProperty, value);
        }

        /// <summary>
        /// The parameter to pass to the <see cref="OnCloseCommand"/>
        /// </summary>
        public object OnCloseCommandParameter
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
        ///     Determines the minimum position of the sheet when it is visible.
        ///     This is a bindable property.
        /// </summary>
        /// <remarks>This will affect the size of the sheet if <see cref="Position" /> is set to 0</remarks>
        /// <remarks>This will affect the people that are dragging the sheet</remarks>
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
        public View SheetContent
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

        /// <inheritdoc />
        public void Hide()
        {
            IsOpen = false;
        }

        private static async void OnPositionPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is SheetBehavior sheetBehavior)) return;
            if (!(oldvalue is double doubleOldValue)) return;
            if (!(newvalue is double doubleNewvalue)) return;
            if (doubleOldValue == doubleNewvalue) return;
            await sheetBehavior.TranslateBasedOnPosition();
        }

        /// <inheritdoc />
        protected override void OnAttachedTo(ModalityLayout bindable)
        {
            base.OnAttachedTo(bindable);
            m_modalityLayout = bindable;
            m_modalityLayout.BindingContextChanged += OnBindingContextChanged;
            m_modalityLayout.SizeChanged += OnModalityLayoutSizeChanged;
        }

        private void OnModalityLayoutSizeChanged(object sender, EventArgs e)
        {
            ToggleSheetVisibility();
        }

        /// <inheritdoc />
        protected override void OnDetachingFrom(ModalityLayout bindable)
        {
            base.OnDetachingFrom(bindable);
            if (m_modalityLayout == null) return;
            m_modalityLayout.BindingContextChanged -= OnBindingContextChanged;
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            BindingContext = m_modalityLayout?.BindingContext;
        }

        private static void IsOpenPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is SheetBehavior sheetBehavior))
                return;
            if (!(oldvalue is bool boolOldValue)) return;
            if (!(newvalue is bool boolNewvalue)) return;
            if (boolOldValue == boolNewvalue) return;
            sheetBehavior.ToggleSheetVisibility();
        }

        private async void ToggleSheetVisibility()
        {
            m_fromIsOpenContext = true;
            if (m_modalityLayout == null)
            {
                return;
            }

            if (IsOpen)
            {
                m_sheetView = new SheetView(this);
                m_sheetView.Initialize();
                SheetContent.BindingContext = BindingContextFactory?.Invoke() ?? BindingContext;

                //Set height / width
                var widthConstraint = Constraint.RelativeToParent(r => m_modalityLayout.Width);
                var heightConstraint =
                    Constraint.RelativeToParent(
                        r => m_modalityLayout.Height +
                             m_sheetView.SheetFrame
                                 .CornerRadius); //Respect the corner radius to make sure that we do not display the corner radius at the "start" of the sheet
                m_modalityLayout.Show(this, m_sheetView.SheetFrame, widthConstraint: widthConstraint, heightConstraint: heightConstraint);

                //Set start position
                m_sheetView.SheetFrame.TranslationY = Alignment switch { 
                    AlignmentOptions.Bottom => m_sheetView.SheetFrame.Height,
                    AlignmentOptions.Top => -m_sheetView.SheetFrame.Height,
                    _ => throw new ArgumentOutOfRangeException()
                };

                
                //Set position based on size of content
                if (Position <= 0)
                {
                    //Calculate what size the content needs if the position is set to 0
                    Position = m_sheetView.SheetContentHeighRequest / m_modalityLayout.Height;
                }
                else //Set position from input
                {
                    await TranslateBasedOnPosition(false);
                    OnOpenCommand?.Execute(OnOpenCommandParameter);
                    OnOpen?.Invoke(this, new EventArgs());
                }
            }
            else
            {
                if (m_sheetView == null) return;

                var y = Alignment switch { 
                        AlignmentOptions.Bottom => m_modalityLayout.Height,
                        AlignmentOptions.Top => -m_modalityLayout.Height,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                m_modalityLayout.Hide(m_sheetView.SheetFrame, m_sheetView.SheetFrame.TranslateTo(m_sheetView.SheetFrame.X, y), () =>
                {
                    OnCloseCommand?.Execute(OnCloseCommandParameter);
                    OnClose?.Invoke(this, new EventArgs());
                });
            }

            m_fromIsOpenContext = false;
        }

        private async Task TranslateBasedOnPosition(bool shouldExecuteOpenedCommand = true)
        {
            if (!IsOpen) return;
            if (m_modalityLayout == null) return;
            if (m_sheetView == null) return;

            if (MinPosition <= 0 || MinPosition > 1)
            {
                MinPosition = (double)MinPositionProperty.DefaultValue;
            }

            if (MaxPosition <= 0 || MaxPosition > 1)
            {
                MaxPosition = (double)MaxPositionProperty.DefaultValue;
            }

            if (MinPosition > MaxPosition)
            {
                MinPosition = (double)MinPositionProperty.DefaultValue;
            }

            if (Position < MinPosition)
            {
                Position = MinPosition;
            }

            if (Position > MaxPosition)
            {
                Position = MaxPosition;
            }

            var yTranslation = Alignment switch { 
                    AlignmentOptions.Bottom => m_sheetView.SheetFrame.Height * (1 - Position), 
                    AlignmentOptions.Top => -m_sheetView.SheetFrame.Height * (1 - Position) - m_sheetView.SheetFrame.CornerRadius, 
                    _ => throw new ArgumentOutOfRangeException()
            };

            if (m_fromIsOpenContext || !m_fromIsDraggingContext)
            {
                await m_sheetView.SheetFrame.TranslateTo(m_sheetView.SheetFrame.X, yTranslation);

                if (shouldExecuteOpenedCommand)
                {
                    OnOpenCommand?.Execute(OnOpenCommandParameter);
                    OnOpen?.Invoke(this, new EventArgs());
                }
            }
            else
            {
                m_sheetView.SheetFrame.TranslationY = yTranslation;
            }
        }

        internal void UpdatePosition(double newYPosition)
        {
            if (m_modalityLayout == null) return;
            if (m_sheetView == null) return;

            Position = Alignment switch {
                AlignmentOptions.Bottom => (m_sheetView.SheetFrame.Height - newYPosition) / m_modalityLayout.Height,
                AlignmentOptions.Top => (m_sheetView.SheetFrame.Height + newYPosition) / m_modalityLayout.Height,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        internal bool IsDragging
        {
            get => m_fromIsDraggingContext;
            set => m_fromIsDraggingContext = value;
        }
    }

    /// <summary>
    ///     The positions of the sheet when it appears.
    /// </summary>
    public enum AlignmentOptions
    {
        /// <summary>
        ///     Positions the sheet at the bottom of the screen and will be draggable from bottom to top.
        /// </summary>
        Bottom = 0,

        /// <summary>
        ///     Positions the sheet at the top of the screen and will be draggable from top to bottom.
        /// </summary>
        Top
    }

    /// <summary>
    ///     The alignment of the content of the sheet.
    /// </summary>
    public enum ContentAlignment
    {
        /// <summary>
        ///     The content will only use as much space as it needs.
        /// </summary>
        Fit = 0,

        /// <summary>
        ///     The content will use the entire sheet as space.
        /// </summary>
        Fill
    }
}