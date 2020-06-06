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
        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return ProvideValue(serviceProvider);
        }

        /// <inheritdoc/>
        public Color ProvideValue(IServiceProvider serviceProvider)
        {
            var listOfBools = new List<bool>() { Theme != null, StatusColorPalette != null, ColorPalette != null };
            Tuple<string, string?> xamlInfo;

            if (listOfBools.Count(b => b) > 1)//More than one color family is set
            {
                xamlInfo = GetXamlInfo(serviceProvider);
                if (xamlInfo.Item2 != null)
                {
                    throw new XamlParseException($"{xamlInfo.Item1} is using more than one color as {xamlInfo.Item2}. {nameof(DIPSColorExtension)} does not accept this.");
                }
                else
                {
                    throw new XamlParseException($"{xamlInfo.Item1} is using more than one color. {nameof(DIPSColorExtension)} does not accept this.");
                }
            }
            else if (listOfBools.Count(b => b) == 0) //Need to set at least one color family
            {
                xamlInfo = GetXamlInfo(serviceProvider);
                if (xamlInfo.Item2 != null)
                {
                    throw new XamlParseException($"{xamlInfo.Item1} has not set any color for {xamlInfo.Item2} when using {nameof(DIPSColorExtension)}.");
                }
                else
                {
                    throw new XamlParseException($"{xamlInfo.Item1} has not set any color when using {nameof(DIPSColorExtension)}.");
                }
            }
            else //Happy case
            {
                if (Theme != null)
                {
                    return Theme.FromIdentifier();
                }

                if (StatusColorPalette != null)
                {
                    return StatusColorPalette.FromIdentifier();
                }

                if (ColorPalette != null)
                {
                    return ColorPalette.FromIdentifier();
                }
            }

            xamlInfo = GetXamlInfo(serviceProvider);
            if (xamlInfo.Item2 != null)
            {
                throw new XamlParseException($"You need to specify at least one color when using {nameof(DIPSColorExtension)}");
            }
            else
            {
                throw new XamlParseException($"{xamlInfo.Item1} has not set any color when using {nameof(DIPSColorExtension)}.");
            }

        }

        private Tuple<string, string?> GetXamlInfo(IServiceProvider serviceProvider)
        {
            var provideValueTarget = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));
            var xamlElement = provideValueTarget.TargetObject.ToString();
            var xamlProperty = provideValueTarget.TargetProperty;
            if (xamlProperty is BindableProperty bp)
            {
                return new Tuple<string, string>(xamlElement, bp.PropertyName);
            }
            return new Tuple<string, string>(xamlElement, null);
        }
    }
}
