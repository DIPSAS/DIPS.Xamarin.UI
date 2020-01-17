using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using DIPS.Xamarin.UI.Controls.Popup;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.Modality
{
    /// <summary>
    /// Layout used to add content showing popups
    /// </summary>
    [ContentProperty(nameof(MainContent))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [ExcludeFromCodeCoverage]
    public partial class ModalityLayout : ContentView
    {
        private readonly TapGestureRecognizer m_closePopupRecognizer;
        private readonly Lazy<Frame> m_blockingFrame;

        /// <summary>
        /// Create an instance
        /// </summary>
        public ModalityLayout()
        {
            InitializeComponent();
            m_closePopupRecognizer = new TapGestureRecognizer { Command = new Command(HideCurrentShowingModality) };
            m_blockingFrame = new Lazy<Frame>(CreateBlockingFrame);
        }

        /// <summary>
        /// <see cref="MainContent" />
        /// </summary>
        public static readonly BindableProperty MainContentProperty =
            BindableProperty.Create(nameof(MainContent), typeof(View), typeof(ModalityLayout), propertyChanged: OnMainContentPropertyChanged);

        private IModality? m_currentShowingModality;

        /// <summary>
        /// Main Content of the layout. This is routed from the Content property, so you don't have to use it.
        /// </summary>
        public View MainContent
        {
            get { return (View)GetValue(MainContentProperty); }
            set { SetValue(MainContentProperty, value); }
        }

        private static void OnMainContentPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is ModalityLayout popupLayout)) return;
            if (!(newvalue is View newView)) return;
            if (newvalue == popupLayout.relativeLayout)
            {
                popupLayout.Content = popupLayout.relativeLayout;
            }
            else
            {
                popupLayout.relativeLayout.Children.Clear();
                popupLayout.relativeLayout.Children.Add(newView, Constraint.RelativeToParent(parent => parent.X), Constraint.RelativeToParent(parent => parent.Y), Constraint.RelativeToParent(parent => parent.Width), Constraint.RelativeToParent(parent => parent.Height));
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
                view.GestureRecognizers.Remove(m_closePopupRecognizer);
                view.GestureRecognizers.Add(m_closePopupRecognizer);
            }
        }

        private void OnOverlayClicked(object sender, EventArgs args)
        {
            HideCurrentShowingModality();
        }

        private void HideCurrentShowingModality()
        {
            m_currentShowingModality?.Hide();
        }

        private Frame CreateBlockingFrame()
        {
            var background = new Frame
            {
                BackgroundColor = Color.Gray,
                IsVisible = true,
                Opacity = 0.5
            };

            background.GestureRecognizers.Add(m_closePopupRecognizer);
            return background;
        }

        public void Show(IModality modality, View content, View relativeView)
        {
            relativeLayout.Children.Add(m_blockingFrame.Value,
                widthConstraint: Constraint.RelativeToParent(r => r.Width),
                heightConstraint: Constraint.RelativeToParent(r => r.Height),
                xConstraint: Constraint.RelativeToParent(r => 0.0),
                yConstraint: Constraint.RelativeToParent(r => 0.0));

            m_currentShowingModality = modality;

            relativeLayout.Children.Add(content,
                yConstraint: Constraint.RelativeToParent((r) => relativeView.GetY(this) + relativeView.Height));
        }

        public void Hide(View content)
        {
            relativeLayout.Children.Remove(content);
            HideOverlay();
        }

        public void HideOverlay()
        {
            relativeLayout.Children.Remove(m_blockingFrame.Value);
        }
    }
}
