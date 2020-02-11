using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly Frame m_overLay;

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

        private View? m_currentView;

        /// <summary>
        ///     Create an instance
        /// </summary>
        public ModalityLayout()
        {
            InitializeComponent();
            m_closeModalityRecognizer = new TapGestureRecognizer { Command = new Command(HideCurrentShowingModality) };
            //m_overLay = new Lazy<Frame>(CreateOverlay);
            m_overLay = new Frame { BackgroundColor = OverlayColor, IsVisible = true, Opacity = 0.0 , InputTransparent = true};
            m_overLay.GestureRecognizers.Add(m_closeModalityRecognizer);
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
                modalityLayout.relativeLayout.Children.Add(
                    modalityLayout.m_overLay,
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
                if (view.GestureRecognizers.Any(g => g == m_closeModalityRecognizer)) return;
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

        //private Frame CreateOverlay()
        //{
        //    var overlayFrame = new Frame { BackgroundColor = OverlayColor, IsVisible = true, Opacity = 0.0 };

        //    overlayFrame.GestureRecognizers.Add(m_closeModalityRecognizer);
        //    return overlayFrame;
        //}

        /// <summary>
        ///     Shows a view relative to a another view inside of a modality layout
        /// </summary>
        /// <param name="modalityHandler">The handler of a modality</param>
        /// <param name="view">The view to show</param>
        /// <param name="relativeView">The view to place the modality view relative to</param>
        public void Show(IModalityHandler modalityHandler, View view, View relativeView) => Show(modalityHandler, view, yConstraint: Constraint.RelativeToParent(r => Y + relativeView.Height));

        /// <summary>
        /// Shows a view relative to the modality layout by constraints
        /// </summary>
        /// <param name="modalityHandler">The handler of a modality</param>
        /// <param name="view">The view to show</param>
        /// <param name="widthConstraint">RelativeLayout width constraint</param>
        /// <param name="heightConstraint">RelativeLayout height constraint</param>
        /// <param name="xConstraint">RelativeLayout x constraint</param>
        /// <param name="yConstraint">RelativeLayout y constraint</param>
        /// <remarks>Using relative to parent with the constraints will give you the <see cref="ModalityLayout"/></remarks>
        public void Show(IModalityHandler modalityHandler, View view, Constraint? xConstraint = null, Constraint? yConstraint = null, Constraint? widthConstraint = null, Constraint? heightConstraint = null)
        {
            m_currentShowingModalityHandler = modalityHandler;
            m_currentView = view;

            ShowOverlay();

            relativeLayout.Children.Add(view, xConstraint, yConstraint, widthConstraint, heightConstraint);
            view.SizeChanged += ContentSizeChanged;
        }

        /// <summary>
        ///     Shows an overlay beneath the <paramref name="view"/>
        /// /// </summary>
        /// <param name="modalityHandler">The handler of a modality</param>
        /// <param name="view">The view that's over he overlay</param>
        public void Show(IModalityHandler modalityHandler, View view)
        {
            m_currentShowingModalityHandler = modalityHandler;
            ShowOverlay();
        }

        private void ContentSizeChanged(object sender, EventArgs e)
        {
            relativeLayout.ForceLayout();
        }

        private void ShowOverlay()
        {
            var overlay = m_overLay;
            overlay.FadeTo(0.5);
            overlay.InputTransparent = false;
        }

        /// <summary>
        ///     Hides a view from the modality layout
        /// </summary>
        /// <remarks>Also hides the overlay</remarks>
        /// <param name="view"></param>
        public async void Hide(View view)
        {
            view.SizeChanged -= ContentSizeChanged;
            if (!relativeLayout.Children.Contains(view)) return;
            if (m_currentShowingModalityHandler == null) return;

            var hideOverLayTask = HideOverlay();

            await m_currentShowingModalityHandler.BeforeRemoval();
            await hideOverLayTask;

            relativeLayout.Children.Remove(view);
        }

        internal async Task HideOverlay()
        {
            var overlay = m_overLay;
            m_overLay.InputTransparent = true;
            await overlay.FadeTo(0);
        }

        private void OnChildRemoved(object sender, ElementEventArgs e)
        {
            if (m_currentShowingModalityHandler == null) return;
            if (m_currentView == null) return;

            if (!relativeLayout.Children.Contains(m_currentView))
            {
                m_currentShowingModalityHandler.AfterRemoval();
                m_currentView = null;
            }
        }
    }
}