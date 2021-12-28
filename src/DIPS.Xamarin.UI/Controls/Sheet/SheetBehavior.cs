using System;
using System.Threading.Tasks;
using DIPS.Xamarin.UI.Controls.Modality;
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
        public Task BeforeRemoval()
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task AfterRemoval()
        {
            ClosedCommand?.Execute(ClosedCommandParameter);
            Closed?.Invoke(this, EventArgs.Empty);

            return Task.CompletedTask;
        }

        /// <summary>
        ///     Moves the sheet to given <paramref name="position" />. Sheet must be open.
        /// </summary>
        /// <param name="position">Valid values are: 0.0 -> 1.0 </param>
        public void MoveTo(double position)
        {
            if (!IsOpen)
            {
                return;
            }

            m_sheetView?.MoveTo(SheetViewUtility.CoerceRatio(position));
        }

        internal AlignmentOptions Alignment => AlignmentOptions.Bottom;
        
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
            if (CancelCommand is not CancelSheetCommand)
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
            if (bindable is not SheetBehavior sheetBehavior)
            {
                return;
            }

            sheetBehavior.m_sheetView?.ChangeVerticalContentAlignment();
        }

        private static void OnSheetContentPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is not SheetBehavior sheetBehavior)
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
            if (bindable is not SheetBehavior sheetBehavior)
            {
                return;
            }

            if (oldvalue is not bool oldValue)
            {
                return;
            }

            if (newvalue is not bool shouldOpen)
            {
                return;
            }

            if (oldValue == shouldOpen)
            {
                return;
            }

            sheetBehavior.ToggleSheetVisibility(shouldOpen);
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

                await m_sheetView.Open();
            }
            else
            {
                if (m_sheetView is null)
                {
                    return;
                }

                BeforeClosing();
                m_sheetView.Close();
                m_modalityLayout.Hide(m_sheetView);
            }
        }

        private void SetupSheet()
        {
            if (m_sheetView is null || m_modalityLayout is null)
            {
                return;
            }

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

            m_sheetView.TranslationY = m_sheetView.Sheet.Height;
        }

        private void SetBindingContext()
        {
            if (m_sheetView?.SheetContentView is null)
            {
                return;
            }

            m_sheetView.SheetContentView.Content.BindingContext = BindingContextFactory?.Invoke() ?? BindingContext;
        }

        internal void AfterOpening()
        {
            if (OpenedCommand is not null && OpenedCommand.CanExecute(OpenedCommandParameter))
            {
                OpenedCommand.Execute(OpenedCommandParameter);
            }

            Opened?.Invoke(this, EventArgs.Empty);
        }

        internal void BeforeOpening()
        {
            if (BeforeOpenedCommand is not null && BeforeOpenedCommand.CanExecute(BeforeOpenedCommandParameter))
            {
                BeforeOpenedCommand.Execute(BeforeOpenedCommandParameter);
            }

            BeforeOpened?.Invoke(this, EventArgs.Empty);
        }

        internal void BeforeClosing()
        {
            if (BeforeClosedCommand is not null && BeforeClosedCommand.CanExecute(BeforeClosedCommandParameter))
            {
                BeforeClosedCommand.Execute(BeforeClosedCommandParameter);
            }

            BeforeClosed?.Invoke(this, EventArgs.Empty);
        }
    }
}