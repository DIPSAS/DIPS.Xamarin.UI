using System.ComponentModel;
using Android.Content;
using Android.Views;
using DIPS.Xamarin.UI.Android;
using DIPS.Xamarin.UI.Internal;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using TextAlignment = Xamarin.Forms.TextAlignment;

[assembly: ExportRenderer(typeof(InternalButton), typeof(InternalButtonRenderer))]

namespace DIPS.Xamarin.UI.Android
{
    internal class InternalButtonRenderer : ButtonRenderer
    {
        public InternalButtonRenderer(Context context) : base(context)
        {
        }

        public new InternalButton Element => (InternalButton)base.Element;

        /// <summary>
        /// Called when [element changed].
        /// </summary>
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
            {
                return;
            }

            SetHorizonalTextAlignment();
            SetVerticalTextAlignment();
        }

        /// <summary>
        /// Handles the <see cref="E:ElementPropertyChanged" /> event.
        /// </summary>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == InternalButton.HorizontalTextAlignmentProperty.PropertyName)
            {
                SetHorizonalTextAlignment();
            }

            if (e.PropertyName == InternalButton.VerticalTextAlignmentProperty.PropertyName)
            {
                SetVerticalTextAlignment();
            }
        }
       
        /// <summary>
        /// To set the horizontal text alignment.
        /// </summary>
        private void SetHorizonalTextAlignment()
        {
            Control.Gravity = Element.HorizontalTextAlignment.ToHorizontalGravityFlags() |
                              Element.VerticalTextAlignment.ToVerticalGravityFlags();
        }

        /// <summary>
        /// To set the vertical text alignment.
        /// </summary>
        private void SetVerticalTextAlignment()
        {
            Control.Gravity = Element.VerticalTextAlignment.ToVerticalGravityFlags() |
                              Element.HorizontalTextAlignment.ToHorizontalGravityFlags();
        }
    }

    public static class AlignmentHelper
    {
        /// <summary>
        /// To set the flags for horizontal text alignment.
        /// </summary>
        public static GravityFlags ToHorizontalGravityFlags(this TextAlignment alignment)
        {
            if (alignment == TextAlignment.Center)
            {
                return GravityFlags.CenterHorizontal;
            }

            return alignment == TextAlignment.End ? GravityFlags.Right : GravityFlags.Left;
        }

        /// <summary>
        /// To set the flags for vertictal text alignment.
        /// </summary>
        public static GravityFlags ToVerticalGravityFlags(this TextAlignment alignment)
        {
            if (alignment == TextAlignment.Center)
            {
                return GravityFlags.CenterVertical;
            }

            return alignment == TextAlignment.End ? GravityFlags.Top : GravityFlags.Bottom;
        }
    }
}