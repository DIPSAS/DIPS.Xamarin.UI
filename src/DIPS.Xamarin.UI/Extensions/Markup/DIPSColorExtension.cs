using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DIPS.Xamarin.UI.Resources.Colors;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Extensions.Markup
{
    /// <summary>
    /// This markup extension can be used to set a color for visual elements.
    /// </summary>
    public class DIPSColorExtension : IMarkupExtension<Color>
    {
        /// <inheritdoc cref="Theme.Identifier"/>
        public Theme.Identifier? Theme { get; set; }

        /// <inheritdoc cref="StatusColorPalette.Identifier"/>
        public StatusColorPalette.Identifier? StatusColorPalette { get; set; }

        /// <inheritdoc cref="ColorPalette.Identifier"/>
        public ColorPalette.Identifier? ColorPalette { get; set; }

        /// <inheritdoc/>
        public Color ProvideValue(IServiceProvider serviceProvider)
        {
            var listOfBools = new List<bool>() { Theme != null, StatusColorPalette != null, ColorPalette != null };

            if(listOfBools.Count(b => b) > 1)//More than one color family is set
            {
                throw new XamlParseException("You can not use more than one color.");
            }
            else if(listOfBools.Count(b => b) == 0) //Need to set at least one color family
            {
                throw new XamlParseException("You need to specify at least one color");
            }
            else //Happy case
            {
                if(Theme != null)
                {
                    return Theme.FromIdentifier();
                }

                if(StatusColorPalette != null)
                {
                    return StatusColorPalette.FromIdentifier();
                }

                if(ColorPalette != null)
                {
                    return ColorPalette.FromIdentifier();
                }
            }
            throw new XamlParseException("You need to specify at least one color");
        }

        /// <inheritdoc/>
        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return ProvideValue(serviceProvider);
        }
    }
}
