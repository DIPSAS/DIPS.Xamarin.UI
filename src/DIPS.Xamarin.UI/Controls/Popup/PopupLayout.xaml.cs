using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using DIPS.Xamarin.UI.Controls.Modality;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.Popup
{
    /// <summary>
    /// Layout used to add content showing popups
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [ExcludeFromCodeCoverage]
    [Obsolete("PopupLayout is obsolete because of it's name and responsibility. Please use ModalityLayout instead. This will be removed in future packages")]
    public partial class PopupLayout : ModalityLayout
    {

        /// <summary>
        /// Create an instance
        /// </summary>
        public PopupLayout() : base()
        {
            InitializeComponent();
        }
    }
}
