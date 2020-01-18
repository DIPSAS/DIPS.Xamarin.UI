using System;
using System.Threading.Tasks;
using DIPS.Xamarin.UI.Controls.Modality;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.Sheet
{
    [ContentProperty(nameof(SheetContent))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class SheetBehavior : Behavior<ModalityLayout>, IModalityHandler
    {
        private ModalityLayout? m_modalityLayout;

        private SheetView? m_sheetView;

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

        public static readonly BindableProperty SheetContentProperty = BindableProperty.Create(nameof(SheetContent), typeof(View), typeof(SheetView));

        public static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(
            nameof(BackgroundColor),
            typeof(Color),
            typeof(SheetView),
            Color.White);

        public static readonly BindableProperty PositionProperty = BindableProperty.Create(
            nameof(Position),
            typeof(double),
            typeof(SheetBehavior),
            0.1,
            propertyChanged: OnPositionPropertyChanged);

        public static readonly BindableProperty MaxHeightRequestProperty = BindableProperty.Create(
            nameof(MaxHeightRequest),
            typeof(double),
            typeof(SheetBehavior),
            1.0);

        public static readonly BindableProperty MinHeightRequestProperty = BindableProperty.Create(
            nameof(MinHeightRequest),
            typeof(double),
            typeof(SheetBehavior),
            0.1);

        public static readonly BindableProperty BindingContextFactoryProperty = BindableProperty.Create(
            nameof(BindingContextFactory),
            typeof(Func<object>),
            typeof(SheetBehavior));

        private bool m_firstTimeOpened;

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

        public bool IsOpen
        {
            get => (bool)GetValue(IsOpenProperty);
            set => SetValue(IsOpenProperty, value);
        }

        //Double between 0.1 - 1.0
        public double MaxHeightRequest
        {
            get => (double)GetValue(MaxHeightRequestProperty);
            set => SetValue(MaxHeightRequestProperty, value);
        }

        //Double between 0.1 - 1.0
        public double MinHeightRequest
        {
            get => (double)GetValue(MinHeightRequestProperty);
            set => SetValue(MinHeightRequestProperty, value);
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
            sheetBehavior.ValidatePositionProperties();
            await sheetBehavior.TranslateBasedOnPosition();
        }

        protected override void OnAttachedTo(ModalityLayout bindable)
        {
            base.OnAttachedTo(bindable);
            m_modalityLayout = bindable;
            m_modalityLayout.BindingContextChanged += OnBindingContextChanged;
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

            sheetBehavior.ValidatePositionProperties();

            sheetBehavior.ToggleSheetVisibility();
        }

        private void ValidatePositionProperties()
        {
            if (MinHeightRequest < 0.1)
            {
                throw new ArgumentException($"{nameof(MinHeightRequest)} can not be less than 0.1. Below 0.1 is considered bad user experience");
            }

            if (MinHeightRequest > 1)
            {
                throw new ArgumentException($"{nameof(MinHeightRequest)} can not be more than 1.0");
            }

            if (Position < MinHeightRequest)
            {
                Position = MinHeightRequest;
            }

            if (Position > MaxHeightRequest)
            {
                Position = MaxHeightRequest;
            }
        }

        private async void ToggleSheetVisibility()
        {
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
                SheetContent.BindingContext = BindingContextFactory?.Invoke() ?? BindingContext;

                if (Alignment == AlignmentOptions.Bottom)
                {
                    var widthConstraint = Constraint.RelativeToParent(r => m_modalityLayout.Width);
                    var heightConstraint =
                        Constraint.RelativeToParent(
                            r => m_modalityLayout.Height +
                                 m_sheetView.SheetFrame
                                     .CornerRadius); //Respect the corner radius to make sure that we do not display the corner radius at the "start" of the sheet
                    m_modalityLayout.Show(this, m_sheetView.SheetFrame, widthConstraint: widthConstraint, heightConstraint: heightConstraint);

                    m_sheetView.SheetFrame.TranslationY = m_modalityLayout.Height;

                    await TranslateBasedOnPosition(true);
                }
            }
            else
            {
                m_modalityLayout.Hide(m_sheetView.SheetFrame, m_sheetView.SheetFrame.TranslateTo(m_sheetView.SheetFrame.X, m_modalityLayout.Height));
            }
        }

        private async Task TranslateBasedOnPosition(bool firstTimeOpened = false)
        {
            if (!IsOpen) return;
            if (m_modalityLayout == null) return;
            if (m_sheetView == null) return;

            if (Position > MaxHeightRequest || Position < MinHeightRequest) return;

            if (Position > 0)
            {
                var yTranslation = 0.0;
                if (Alignment == AlignmentOptions.Bottom)
                {
                    yTranslation = m_modalityLayout.Height * (1 - Position);
                }

                if (firstTimeOpened)
                {
                    await m_sheetView.SheetFrame.TranslateTo(m_sheetView.SheetFrame.X, yTranslation);
                }
                else
                {
                    await m_sheetView.SheetFrame.TranslateTo(m_sheetView.SheetFrame.X, yTranslation, 20U);
                }
                
            }
            else
            {
                //Calculate what size the content needs if the position is set to 0
                Position = m_sheetView.SheetContentHeighRequest / m_modalityLayout.Height;
            }
        }

        public void UpdatePosition(double newYPosition)
        {
            if (m_modalityLayout == null) return;
            Position = (m_modalityLayout.Height-newYPosition) / m_modalityLayout.Height;
        }
    }

    public enum AlignmentOptions
    {
        Bottom
    }
}