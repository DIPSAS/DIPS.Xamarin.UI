using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DIPS.Xamarin.UI.Internal.Utilities;
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
        private Theme.Identifier m_theme;
        private bool m_themeWasSet;
        private StatusColorPalette.Identifier m_statusColorPalette;
        private bool m_statusColorPaletteWasSet;
        private ColorPalette.Identifier m_colorPalette;
        private bool m_colorPaletteWasSet;

        /// <inheritdoc cref="Theme.Identifier"/>
        public Theme.Identifier Theme
        {
            get => m_theme;
            set
            {
                m_theme = value;
                m_themeWasSet = true;
            }
        }

        /// <inheritdoc cref="StatusColorPalette.Identifier"/>
        public StatusColorPalette.Identifier StatusColorPalette
        {
            get => m_statusColorPalette;
            set
            {
                m_statusColorPalette = value;
                m_statusColorPaletteWasSet = true;
            }
        }

        /// <inheritdoc cref="ColorPalette.Identifier"/>
        public ColorPalette.Identifier ColorPalette
        {
            get => m_colorPalette;
            set
            {
                m_colorPalette = value;
                m_colorPaletteWasSet = true;
            }
        }

        /// <inheritdoc/>
        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return ProvideValue(serviceProvider);
        }

        /// <inheritdoc/>
        public Color ProvideValue(IServiceProvider serviceProvider)
        {
            Tuple<string, string?> xamlInfo;
            var numberOfSelectedColorFamilies = new List<bool>() { m_themeWasSet, m_statusColorPaletteWasSet, m_colorPaletteWasSet }.Count(b => b);

            if (numberOfSelectedColorFamilies > 1)//More than one color family is set
            {
                xamlInfo = GetXamlInfo(serviceProvider);
                if (xamlInfo.Item2 != null)
                {
                    throw new XamlParseException($"{xamlInfo.Item1} is using more than one color as {xamlInfo.Item2}. {nameof(DIPSColorExtension)} does not accept this.").WithXmlLineInfo(serviceProvider);
                }
                else
                {
                    throw new XamlParseException($"{xamlInfo.Item1} is using more than one color. {nameof(DIPSColorExtension)} does not accept this.").WithXmlLineInfo(serviceProvider);
                }
            }
            else if (numberOfSelectedColorFamilies == 0) //Need to set at least one color family
            {
                xamlInfo = GetXamlInfo(serviceProvider);
                if (xamlInfo.Item2 != null)
                {
                    throw new XamlParseException($"{xamlInfo.Item1} has not set any color for {xamlInfo.Item2} when using {nameof(DIPSColorExtension)}.").WithXmlLineInfo(serviceProvider);
                }
                else
                {
                    throw new XamlParseException($"{xamlInfo.Item1} has not set any color when using {nameof(DIPSColorExtension)}.").WithXmlLineInfo(serviceProvider);
                }
            }
            else //Happy case
            {
                if (m_themeWasSet)
                {
                    return Theme.FromIdentifier();
                }

                if (m_statusColorPaletteWasSet)
                {
                    return StatusColorPalette.FromIdentifier();
                }

                if (m_colorPaletteWasSet)
                {
                    return ColorPalette.FromIdentifier();
                }
            }

            xamlInfo = GetXamlInfo(serviceProvider);
            if (xamlInfo.Item2 != null)
            {
                throw new XamlParseException($"You need to specify at least one color when using {nameof(DIPSColorExtension)}").WithXmlLineInfo(serviceProvider);
            }
            else
            {
                throw new XamlParseException($"{xamlInfo.Item1} has not set any color when using {nameof(DIPSColorExtension)}.").WithXmlLineInfo(serviceProvider);
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
