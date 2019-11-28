using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Resources
{
    /// <summary>
    /// ResourceDictionary of the common colors
    /// </summary>
    /// <remarks>
    /// See wiki: https://github.com/DIPSAS/DIPS.Xamarin.UI/wiki/Colors
    /// </remarks>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Colors : ResourceDictionary
    {
        /// <summary>
        /// Constructs the common colors.
        /// </summary>
        public Colors()
        {
            InitializeComponent();
        }
    }
}