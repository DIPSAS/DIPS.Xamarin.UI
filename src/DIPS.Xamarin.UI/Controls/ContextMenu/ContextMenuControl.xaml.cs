using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.ContextMenu
{
    [ContentProperty(nameof(TheContent))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContextMenuControl : IContextMenu
    {
        public ContextMenuControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// <see cref="TheContent"/>
        /// </summary>
        public static readonly BindableProperty TheContentProperty = BindableProperty.Create(
            nameof(TheContent),
            typeof(View),
            typeof(ContextMenuControl), propertyChanged:TheContentPropertyChanged);

        private static void TheContentPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is not ContextMenuControl contextMenuControl) return;
            if (newvalue is not View theView) return;
            theView.SizeChanged += (sender, args) =>
            {
                contextMenuControl.ContextMenuButton.MenuPosition = theView.Bounds;
            };
        }

        /// <summary>
        /// The content to be displayed which the user clicks to open a context menu
        /// </summary>
        public View TheContent
        {
            get => (View)GetValue(TheContentProperty);
            set => SetValue(TheContentProperty, value);
        }

        /// <summary>
        /// <inheritdoc cref="IContextMenu"/>
        /// </summary>
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(ContextMenuControl),
            defaultValue: ContextMenuButton.TitleProperty.DefaultValue);

        /// <summary>
        /// <inheritdoc cref="IContextMenu"/>
        /// </summary>
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        /// <summary>
        /// <inheritdoc cref="IContextMenu"/>
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            nameof(ItemsSource),
            typeof(IList<ContextMenuItem>),
            typeof(ContextMenuControl), defaultValueCreator:(bindable => new List<ContextMenuItem>()));

        /// <summary>
        /// <inheritdoc cref="IContextMenu"/>
        /// </summary>
        public IList<ContextMenuItem> ItemsSource
        {
            get => (IList<ContextMenuItem>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        /// <summary>
        /// <inheritdoc cref="IContextMenu"/>
        /// </summary>
        public static readonly BindableProperty ItemClickedCommandProperty = BindableProperty.Create(
            nameof(ItemClickedCommand),
            typeof(ICommand),
            typeof(ContextMenuControl),
            defaultValue: ContextMenuButton.ItemClickedCommandProperty.DefaultValue);

        /// <summary>
        /// <inheritdoc cref="IContextMenu"/>
        /// </summary>
        public ICommand? ItemClickedCommand
        {
            get => (ICommand)GetValue(ItemClickedCommandProperty);
            set => SetValue(ItemClickedCommandProperty, value);
        }

        /// <summary>
        /// <inheritdoc cref="IContextMenu"/>
        /// </summary>
        public static readonly BindableProperty ContextMenuHorizontalOptionsProperty = BindableProperty.Create(
            nameof(ContextMenuHorizontalOptions),
            typeof(ContextMenuHorizontalOptions),
            typeof(ContextMenuControl),
            defaultValue: ContextMenuButton.ContextMenuHorizontalOptionsProperty.DefaultValue);

        public event EventHandler? ItemClicked;

        /// <summary>
        /// <inheritdoc cref="IContextMenu"/>
        /// </summary>
        public ContextMenuHorizontalOptions ContextMenuHorizontalOptions
        {
            get => (ContextMenuHorizontalOptions)GetValue(ContextMenuHorizontalOptionsProperty);
            set => SetValue(ContextMenuHorizontalOptionsProperty, value);
        }

        /// <summary>
        /// <inheritdoc cref="IContextMenu"/>
        /// </summary>
        public event EventHandler? ContextMenuOpened;

        private void ContextMenuButton_OnContextMenuOpened(object sender, EventArgs e)
        {
            ContextMenuOpened?.Invoke(sender, e);
        }

        private void ContextMenuButton_OnItemClicked(object sender, EventArgs e)
        {
            ItemClicked?.Invoke(sender, e);
        }
    }
}