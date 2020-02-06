using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPS.Xamarin.UI.Controls.Modality;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.FloatingActionMenu
{
    /// <summary>
    /// 
    /// </summary>
    [ContentProperty(nameof(Content))]
    public class FloatingActionMenuBehaviour : Behavior<Layout>
    {
        private FloatingActionMenu? m_floatingActionMenu;

        /// <summary>
        /// 
        /// </summary>
        public Layout? Content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bindable"></param>
        protected override void OnAttachedTo(Layout bindable)
        {
            base.OnAttachedTo(bindable);

            m_floatingActionMenu = Content as FloatingActionMenu ?? Content?.Children.First(c => c.GetType() == typeof(FloatingActionMenu)) as FloatingActionMenu;
            if (!(bindable is ModalityLayout)) m_floatingActionMenu?.AddTo(bindable);
            bindable.SizeChanged += BindableOnSizeChanged;
        }

        private void BindableOnSizeChanged(object sender, EventArgs e)
        {
            if (sender is ModalityLayout modality) m_floatingActionMenu?.AddTo(modality.relativeLayout);
            m_floatingActionMenu?.RaiseMenu();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bindable"></param>
        protected override void OnDetachingFrom(Layout bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.SizeChanged -= BindableOnSizeChanged;
        }
    }
}
