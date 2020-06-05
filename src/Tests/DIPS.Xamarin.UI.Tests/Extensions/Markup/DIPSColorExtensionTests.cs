using System;
using System.Collections.Generic;
using System.Text;
using DIPS.Xamarin.UI.Extensions.Markup;
using Moq;
using Xunit;

namespace DIPS.Xamarin.UI.Tests.Extensions.Markup
{
    public class DIPSColorExtensionTests
    {


        [Fact]
        public void Method_Scenario_Expectation()
        {
            try
            {
                var extension = new DIPSColorExtension();
                extension.Theme = Resources.Colors.ThemeEnum.TealPrimary;
                extension.StatusColorPalette = Resources.Colors.StatusColorPaletteEnum.DangerAir;
                extension.ColorPalette = Resources.Colors.ColorPaletteEnum.AccentLight;

                extension.ProvideValue(new Mock<IServiceProvider>().Object);
            }
            catch (Exception exception)
            {

                throw;
            }
        }
    }
}
