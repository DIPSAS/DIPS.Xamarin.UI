using System;
using System.Diagnostics.CodeAnalysis;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.Modality
{
    /// <summary>
    ///     Layout used to add content showing modality components
    /// </summary>
    [ContentProperty(nameof(MainContent))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [ExcludeFromCodeCoverage]
    public partial class ModalityLayout : ContentView
    {
        private readonly TapGestureRecognizer m_closeModalityRecognizer;
        private readonly Lazy<Frame> m_overLay;

        private IModalityHandler? m_currentShowingModalityHandler;

        /// <summary>
        ///     <see cref="MainContent" />
        /// </summary>
        public static readonly BindableProperty MainContentProperty = BindableProperty.Create(
            nameof(MainContent),
            typeof(View),
            typeof(ModalityLayout),
            propertyChanged: OnMainContentPropertyChanged);

        /// <summary>
        ///     <see cref="OverlayColor" />
        /// </summary>
        public static readonly BindableProperty OverlayColorProperty = BindableProperty.Create(
            nameof(OverlayColor),
            typeof(Color),
            typeof(ModalityLayout),
            Color.Gray);

        /// <summary>
        ///     Create an instance
        /// </summary>
        public ModalityLayout()
        {
            InitializeComponent();
            m_closeModalityRecognizer = new TapGestureRecognizer { Command = new Command(HideCurrentShowingModality) };
            m_overLay = new Lazy<Frame>(CreateOverlay);
        }

        /// <summary>
        ///     Main Content of the layout. This is routed from the Content property, so you don't have to use it.
        /// </summary>
        public View MainContent
        {
            get => (View)GetValue(MainContentProperty);
            set => SetValue(MainContentProperty, value);
        }

        /// <summary>
        ///     The color of the overlay when a modality component is showing
        /// </summary>
        public Color OverlayColor
        {
            get => (Color)GetValue(OverlayColorProperty);
            set => SetValue(OverlayColorProperty, value);
        }

        private static void OnMainContentPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is ModalityLayout modalityLayout)) return;
            if (!(newvalue is View newView)) return;
            if (newvalue == modalityLayout.relativeLayout)
            {
                modalityLayout.Content = modalityLayout.relativeLayout;
            }
            else
            {
                modalityLayout.relativeLayout.Children.Clear();
                modalityLayout.relativeLayout.Children.Add(
                    newView,
                    Constraint.RelativeToParent(parent => parent.X),
                    Constraint.RelativeToParent(parent => parent.Y),
                    Constraint.RelativeToParent(parent => parent.Width),
                    Constraint.RelativeToParent(parent => parent.Height));
            }
        }

        internal void AddOnCloseRecognizer(View view)
        {
            if (view is Button button)
            {
                button.Clicked -= OnOverlayClicked;
                button.Clicked += OnOverlayClicked;
            }
            else
            {
                view.GestureRecognizers.Remove(m_closeModalityRecognizer);
                view.GestureRecognizers.Add(m_closeModalityRecognizer);
            }
        }

        private void OnOverlayClicked(object sender, EventArgs args)
        {
            HideCurrentShowingModality();
        }

        private void HideCurrentShowingModality()
        {
            m_currentShowingModalityHandler?.Hide();
        }

        private Frame CreateOverlay()
        {
            var background = new Frame { BackgroundColor = OverlayColor, IsVisible = true, Opacity = 0.5 };

            background.GestureRecognizers.Add(m_closeModalityRecognizer);
            return background;
        }

        /// <summary>
        ///     Shows a view relative to a another view inside of a modality layout
        /// </summary>
        /// <param name="modalityHandler">The handler of a modality</param>
        /// <param name="view">The view to show</param>
        /// <param name="relativeView">The view to place the modality view relative to</param>
        public void Show(IModalityHandler modalityHandler, View view, View relativeView)
        {
            m_currentShowingModalityHandler = modalityHandler;

            ShowOverlay();

            relativeLayout.Children.Add(view, yConstraint: Constraint.RelativeToParent(r => relativeView.GetY(this) + relativeView.Height));
        }

        private void ShowOverlay()
        {
            relativeLayout.Children.Add(
                m_overLay.Value,
                widthConstraint: Constraint.RelativeToParent(r => r.Width),
                heightConstraint: Constraint.RelativeToParent(r => r.Height),
                xConstraint: Constraint.RelativeToParent(r => 0.0),
                yConstraint: Constraint.RelativeToParent(r => 0.0));
        }

        /// <summary>
        ///     Hides a view from the modality layout
        /// </summary>
        /// <remarks>Also hides the overlay</remarks>
        /// <param name="view"></param>
        public void Hide(View view)
        {
            relativeLayout.Children.Remove(view);
            HideOverlay();
        }

        private void HideOverlay()
        {
            relativeLayout.Children.Remove(m_overLay.Value);
        }
    }
}