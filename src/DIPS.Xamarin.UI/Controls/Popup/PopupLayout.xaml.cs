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
