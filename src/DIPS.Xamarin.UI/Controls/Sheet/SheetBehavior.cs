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
            0.0,
            propertyChanged:OnPositionPropertyChanged);

        private SheetView? m_sheetView;

        private static async void OnPositionPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is SheetBehavior sheetBehavior)) return;
            await sheetBehavior.TranslateBasedOnPosition();
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

        public bool IsOpen
        {
            get => (bool)GetValue(IsOpenProperty);
            set => SetValue(IsOpenProperty, value);
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

            sheetBehavior.ToggleSheetVisibility();
        }

        private async void ToggleSheetVisibility()
        {
            if (m_sheetView == null)
            {
                m_sheetView = new SheetView(this);
            }

            if (m_modalityLayout == null) return;

            if (IsOpen)
            {
                var widthConstraint = Constraint.RelativeToParent(r => m_modalityLayout.Width);
                var heightConstraint =
                    Constraint.RelativeToParent(
                        r => m_modalityLayout.Height+m_sheetView.SheetFrame.CornerRadius); //10 is to make sure it always remove the border radius on the start of the frame
                m_modalityLayout.Show(this, m_sheetView.SheetFrame, widthConstraint: widthConstraint, heightConstraint: heightConstraint);

                m_sheetView.SheetFrame.TranslationY = m_modalityLayout.Height;

                await TranslateBasedOnPosition();
                //var percentage = (SheetContent.Height+10) / m_modalityLayout.Height;
                //Set binding context to either factory or ModalityLayout binding context
            }
            else
            {
                await m_sheetView.SheetFrame.TranslateTo(m_sheetView.SheetFrame.X, m_modalityLayout.Height);
                m_modalityLayout?.Hide(m_sheetView.SheetFrame);
            }
        }

        private async Task TranslateBasedOnPosition()
        {
            if (!IsOpen) return;
            if (m_modalityLayout == null) return;
            if (m_sheetView == null) return;

            if (Position > 1 || Position < 0) return;

            if (Position > 0)
            {
                var yTranslation = 0.0;
                if(Alignment == AlignmentOptions.Bottom)
                {
                    yTranslation = m_modalityLayout.Height * (1 - Position);
                }

                await m_sheetView.SheetFrame.TranslateTo(m_sheetView.SheetFrame.X, yTranslation);
            }
            else
            {
                //Calculate what size the content needs if the position is set to 0
                Position = m_sheetView.SheetContentHeighRequest / m_modalityLayout.Height;
            }
        }
    }

    public enum AlignmentOptions
    {
        Bottom
    }
}