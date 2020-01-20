using System;
using System.Threading.Tasks;
using DIPS.Xamarin.UI.Controls.Modality;
using DIPS.Xamarin.UI.Resources.Colors;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.Sheet
{
    [ContentProperty(nameof(SheetContent))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class SheetBehavior : Behavior<ModalityLayout>, IModalityHandler
    {
        private bool m_fromIsOpenContext;
        private ModalityLayout? m_modalityLayout;

        private SheetView? m_sheetView;


        public static readonly BindableProperty VerticalContentAlignmentProperty = BindableProperty.Create(nameof(VerticalContentAlignment), typeof(ContentAlignment), typeof(SheetBehavior), ContentAlignment.Fit);

        /// <summary>
        /// Settings this property changes how much of the space the content should take of the entire sheet. 
        /// </summary>
        public ContentAlignment VerticalContentAlignment
        {
            get => (ContentAlignment)GetValue(VerticalContentAlignmentProperty);
            set => SetValue(VerticalContentAlignmentProperty, value);
        }
        public static readonly BindableProperty AlignmentProperty = BindableProperty.Create(
            nameof(Alignment),
            typeof(AlignmentOptions),
            typeof(SheetView),
            AlignmentOptions.Bottom);

        public static readonly BindableProperty IsOpenProperty = BindableProperty.Create(
            nameof(IsOpen),
            typeof(bool),
            typeof(SheetView),
            false,
            BindingMode.TwoWay,
            propertyChanged: IsOpenPropertyChanged);

        public static readonly BindableProperty SheetContentProperty = BindableProperty.Create(nameof(SheetContent), typeof(View), typeof(SheetView), new ContentView(){HeightRequest = 100, VerticalOptions = LayoutOptions.Start});

        public static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(
            nameof(BackgroundColor),
            typeof(Color),
            typeof(SheetView),
            Color.White);

        public static readonly BindableProperty PositionProperty = BindableProperty.Create(
            nameof(Position),
            typeof(double),
            typeof(SheetBehavior),
            0.0,
            propertyChanged: OnPositionPropertyChanged);

        public static readonly BindableProperty MaxPositionProperty = BindableProperty.Create(
            nameof(MaxPosition),
            typeof(double),
            typeof(SheetBehavior),
            1.0);

        public static readonly BindableProperty MinPositionProperty = BindableProperty.Create(
            nameof(MinPosition),
            typeof(double),
            typeof(SheetBehavior),
            0.1);

        public static readonly BindableProperty BindingContextFactoryProperty = BindableProperty.Create(
            nameof(BindingContextFactory),
            typeof(Func<object>),
            typeof(SheetBehavior));

        public static readonly BindableProperty IsDraggableProperty = BindableProperty.Create(
            nameof(IsDraggable),
            typeof(bool),
            typeof(SheetBehavior));

        public static readonly BindableProperty HasShadowProperty = BindableProperty.Create(nameof(HasShadow), typeof(bool), typeof(SheetBehavior), true);

        public bool HasShadow
        {
            get => (bool)GetValue(HasShadowProperty);
            set => SetValue(HasShadowProperty, value);
        }

        public static readonly BindableProperty HandleColorProperty = BindableProperty.Create(nameof(HandleColor), typeof(Color), typeof(SheetBehavior), ColorPalette.QuinaryAir);

        public Color HandleColor
        {
            get => (Color)GetValue(HandleColorProperty);
            set => SetValue(HandleColorProperty, value);
        }

        public AlignmentOptions Alignment
        {
            get => (AlignmentOptions)GetValue(AlignmentProperty);
            set => SetValue(AlignmentProperty, value);
        }

        public Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        public Func<object> BindingContextFactory
        {
            get => (Func<object>)GetValue(BindingContextFactoryProperty);
            set => SetValue(BindingContextFactoryProperty, value);
        }

        public bool IsDraggable
        {
            get => (bool)GetValue(IsDraggableProperty);
            set => SetValue(IsDraggableProperty, value);
        }

        public bool IsOpen
        {
            get => (bool)GetValue(IsOpenProperty);
            set => SetValue(IsOpenProperty, value);
        }

        //Double between 0.1 - 1.0
        public double MaxPosition
        {
            get => (double)GetValue(MaxPositionProperty);
            set => SetValue(MaxPositionProperty, value);
        }

        //Double between 0.1 - 1.0
        public double MinPosition
        {
            get => (double)GetValue(MinPositionProperty);
            set => SetValue(MinPositionProperty, value);
        }

        public double Position
        {
            get => (double)GetValue(PositionProperty);
            set => SetValue(PositionProperty, value);
        }

        public View SheetContent
        {
            get => (View)GetValue(SheetContentProperty);
            set => SetValue(SheetContentProperty, value);
        }

        public void Hide()
        {
            IsOpen = false;
        }

        private static async void OnPositionPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is SheetBehavior sheetBehavior)) return;
            await sheetBehavior.TranslateBasedOnPosition();
        }

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
            sheetBehavior.ToggleSheetVisibility();
        }

        private async void ToggleSheetVisibility()
        {
            m_fromIsOpenContext = true;
            if (m_modalityLayout == null)
            {
                return;
            }

            if (m_sheetView == null)
            {
                m_sheetView = new SheetView(this);
            }

            if (IsOpen)
            {
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
                switch (Alignment)
                {
                    case AlignmentOptions.Bottom:
                        m_sheetView.SheetFrame.TranslationY = m_sheetView.SheetFrame.Height;
                        break;
                    case AlignmentOptions.Top:
                        m_sheetView.SheetFrame.TranslationY = -m_sheetView.SheetFrame.Height;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                //Set position from input
                if (Position <= 0)
                {
                    //Calculate what size the content needs if the position is set to 0
                    Position = m_sheetView.SheetContentHeighRequest / m_modalityLayout.Height;
                }
                else
                {
                    await TranslateBasedOnPosition();
                }
            }
            else
            {
                switch (Alignment)
                {
                    case AlignmentOptions.Bottom:
                        m_modalityLayout.Hide(m_sheetView.SheetFrame, m_sheetView.SheetFrame.TranslateTo(m_sheetView.SheetFrame.X, m_modalityLayout.Height));
                        break;
                    case AlignmentOptions.Top:
                        m_modalityLayout.Hide(m_sheetView.SheetFrame, m_sheetView.SheetFrame.TranslateTo(m_sheetView.SheetFrame.X, -m_modalityLayout.Height));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
            }

            m_fromIsOpenContext = false;
        }

        private async Task TranslateBasedOnPosition()
        {
            if (!IsOpen) return;
            if (m_modalityLayout == null) return;
            if (m_sheetView == null) return;

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

            var yTranslation =
                Alignment switch { 
                    AlignmentOptions.Bottom => m_sheetView.SheetFrame.Height * (1 - Position), 
                    AlignmentOptions.Top => -m_sheetView.SheetFrame.Height * (1 - Position) - m_sheetView.SheetFrame
                                                .CornerRadius
                };
            if (m_fromIsOpenContext)
            {
                await m_sheetView.SheetFrame.TranslateTo(m_sheetView.SheetFrame.X, yTranslation, 250U);
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

            switch (Alignment)
            {
                case AlignmentOptions.Bottom:
                    Position = (m_sheetView.SheetFrame.Height - newYPosition) / m_modalityLayout.Height;
                    break;
                case AlignmentOptions.Top:

                    Position = ((m_sheetView.SheetFrame.Height + newYPosition) / m_modalityLayout.Height);

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum AlignmentOptions
    {
        Bottom,
        Top
    }

    public enum ContentAlignment
    {
        Fit,
        Fill
    }
}