using System;
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

        private Frame? m_sheetFrame;

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
            0.0);

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
            if (m_sheetFrame == null)
            {
                var sheetView = new SheetView(this);
                m_sheetFrame = sheetView.SheetFrame;
            }

            if (m_modalityLayout == null) return;

            if (IsOpen)
            {
                var widthConstraint = Constraint.RelativeToParent(r => m_modalityLayout.Width);
                var heightConstraint =
                    Constraint.RelativeToParent(
                        r => m_modalityLayout.Height + 10); //10 is to make sure it always remove the border radius on the start of the frame
                m_modalityLayout.Show(this, m_sheetFrame,widthConstraint: widthConstraint, heightConstraint: heightConstraint);

                m_sheetFrame.TranslationY = m_modalityLayout.Height;

                var yTranslation = 0.0;
                if (Position > 0)
                {
                    yTranslation = m_modalityLayout.Height * (1 - Position);
                }
                else
                {
                    yTranslation = m_modalityLayout.Height - SheetContent.Height;
                }
                await m_sheetFrame.TranslateTo(m_sheetFrame.X, yTranslation);
                //var percentage = (SheetContent.Height+10) / m_modalityLayout.Height;
                //Set binding context to either factory or ModalityLayout binding context
            }
            else
            {
                await m_sheetFrame.TranslateTo(m_sheetFrame.X, m_modalityLayout.Height);
                m_modalityLayout?.Hide(m_sheetFrame);
            }
        }
    }

    public enum AlignmentOptions
    {
        Bottom
    }
}