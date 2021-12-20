using System;
using System.Threading.Tasks;
using DIPS.Xamarin.UI.Controls.Modality;
using DIPS.Xamarin.UI.Internal.xaml;
using DIPS.Xamarin.UI.Internal.Xaml.Sheet;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.Sheet
{
    /// <summary>
    ///     A behavior to that can be added to a <see cref="ModalityLayout" /> to display a sheet that animates into the view,
    ///     either from top or bottom, when <see cref="IsOpen" /> is set.
    /// </summary>
    [ContentProperty(nameof(SheetContent))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SheetBehavior : Behavior<ModalityLayout>, IModalityHandler
    {
        /// <inheritdoc />
        public void Hide()
        {
            CancelCommand?.Execute(CancelCommandParameter);
            CancelClickedInternal();
        }

        /// <inheritdoc />
        public async Task BeforeRemoval()
        {
            await Task.Delay(200);
        }

        /// <inheritdoc />
        public Task AfterRemoval()
        {
            ClosedCommand?.Execute(ClosedCommandParameter);
            Closed?.Invoke(this, EventArgs.Empty);

            return Task.CompletedTask;
        }
        
        private static object? OpenSheetCommandValueCreator(BindableObject? b)
        {
            if (b is SheetBehavior sheetBehavior)
            {
                return new Command(() => sheetBehavior.IsOpen = true);
            }

            return null;
        }

        private static object DefaultValueCreator(BindableObject bindable)
        {
            if (bindable is SheetBehavior sheetBehavior)
            {
                return new CancelSheetCommand(() => sheetBehavior.IsOpen = false);
            }

            return new CancelSheetCommand(() => { });
        }

        internal void ActionClickedInternal()
        {
            ActionClicked?.Invoke(this, EventArgs.Empty);
        }

        internal async void CancelClickedInternal()
        {
            CancelClicked?.Invoke(this, EventArgs.Empty);
            if (!(CancelCommand is CancelSheetCommand))
            {
                IsOpen = false;
                return;
            }

            if (CancelCommand is CancelSheetCommand cancelSheetCommand &&
                await cancelSheetCommand.CanCloseSheet(CancelCommandParameter))
            {
                IsOpen = false;
            }
        }

        private static void OnVerticalContentAlignmentPropertyChanged(BindableObject bindable, object oldvalue,
            object newvalue)
        {
            if (!(bindable is SheetBehavior sheetBehavior))
            {
                return;
            }

            sheetBehavior.m_sheetView?.ChangeVerticalContentAlignment();
        }

        private static void OnSheetContentPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is SheetBehavior sheetBehavior))
            {
                return;
            }

            sheetBehavior.SetBindingContext();
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
            if (m_modalityLayout?.CurrentShowingModalityLayout == this)
            {
                return; //Jump out of the size changed event if the modality layout size changes and the sheet is currently visible
            }

            ToggleSheetVisibility(IsOpen);
        }

        /// <inheritdoc />
        protected override void OnDetachingFrom(ModalityLayout bindable)
        {
            base.OnDetachingFrom(bindable);
            if (m_modalityLayout == null)
            {
                return;
            }

            m_modalityLayout.BindingContextChanged -= OnBindingContextChanged;
            
            ToggleSheetVisibility(IsOpen);
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            BindingContext = m_modalityLayout?.BindingContext;
        }

        private static void IsOpenPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is SheetBehavior sheetBehavior))
            {
                return;
            }

            if (!(oldvalue is bool boolOldValue))
            {
                return;
            }

            if (!(newvalue is bool boolNewvalue))
            {
                return;
            }

            if (boolOldValue == boolNewvalue)
            {
                return;
            }

            sheetBehavior.ToggleSheetVisibility(boolNewvalue);
        }

        private async void ToggleSheetVisibility(bool shouldOpen)
        {
            if (m_modalityLayout == null)
            {
                return;
            }
            
            if (shouldOpen)
            {
                m_sheetView = new SheetView(this);
                
                if (SheetContentTemplate != null)
                {
                    SheetContent = (View)SheetContentTemplate.CreateContent();
                }
        
                SetupSheet();
                
                m_sheetView.Show();

                // Wait until all the bindings are done
                await Task.Delay(100);
            }
            else
            {
                if (m_sheetView == null) return;
                m_sheetView.InternalClose();
                m_modalityLayout.Hide(m_sheetView);
            }
        }

        private void SetupSheet()
        {
            if (m_sheetView == null || m_modalityLayout == null) return;

            SetBindingContext();
            m_sheetView.Initialize();

            //Set height / width
            var widthConstraint = Constraint.RelativeToParent(r => m_modalityLayout.Width);
            var heightConstraint =
                Constraint.RelativeToParent(
                    r => m_modalityLayout.Height +
                         m_sheetView
                             .Sheet
                             .OuterSheetFrame
                             .CornerRadius); //Respect the corner radius to make sure that we do not display the corner radius at the "start" of the sheet

            m_modalityLayout.Show(this, m_sheetView, widthConstraint: widthConstraint,
                heightConstraint: heightConstraint);
            
            //Set start position
            m_sheetView.Sheet.TranslationY = Alignment switch
            {
                AlignmentOptions.Bottom => m_sheetView.Sheet.Height,
                AlignmentOptions.Top => -m_sheetView.Sheet.Height,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void SetBindingContext()
        {
            if (m_sheetView?.SheetContentView == null)
            {
                return;
            }

            m_sheetView.SheetContentView.Content.BindingContext = BindingContextFactory?.Invoke() ?? BindingContext;
        }
    }
}