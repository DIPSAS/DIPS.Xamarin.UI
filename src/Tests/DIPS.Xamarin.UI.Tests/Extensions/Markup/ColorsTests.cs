using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;
using DIPS.Xamarin.UI.Extensions.Markup;
using DIPS.Xamarin.UI.Resources.Colors;
using FluentAssertions;
using Moq;
using Xamarin.Forms.Xaml;
using Xunit;

namespace DIPS.Xamarin.UI.Tests.Extensions.Markup
{
    public class ColorsTests
    {
        [Theory]
        [MemberData(nameof(ThemeIdentifiers))]
        public void Theme_HasMappedEnumToColor(Theme.Identifier identifier)
        {
            var identifierName =  Enum.GetName(typeof(Theme.Identifier), identifier);

            typeof(Theme).GetField(identifierName, BindingFlags.Static | BindingFlags.Public).Should().NotBeNull(because:$"each identifier for {nameof(Theme)} should have a corresponding static color defined.");
        }

        [Theory]
        [MemberData(nameof(StatusColorPaletteIdentifiers))]
        public void StatusColorPalette_HasMappedEnumToColor(StatusColorPalette.Identifier identifier)
        {
            var identifierName = Enum.GetName(typeof(StatusColorPalette.Identifier), identifier);

            typeof(StatusColorPalette).GetField(identifierName, BindingFlags.Static | BindingFlags.Public).Should().NotBeNull(because: $"each identifier for {nameof(StatusColorPalette)} should have a corresponding static color defined.");
        }

        [Theory]
        [MemberData(nameof(ColorPaletteIdentifiers))]
        public void ColorPalette_HasMappedEnumToColor(ColorPalette.Identifier identifier)
        {
            var identifierName = Enum.GetName(typeof(ColorPalette.Identifier), identifier);

            typeof(ColorPalette).GetField(identifierName, BindingFlags.Static | BindingFlags.Public).Should().NotBeNull(because: $"each identifier for {nameof(ColorPalette)} should have a corresponding static color defined.");
        }


        [Theory]
        [MemberData(nameof(ThemeIdentifiers))]
        public void Theme_FromIdentifier_IdentifierShouldBeMapped(Theme.Identifier identifier)
        {
            Action act = () => Theme.FromIdentifier(identifier);

            act.Should().NotThrow(because: $"every identifier for {nameof(Theme)} should be mapped to a corresponding static color that is defined");
        }

        [Theory]
        [MemberData(nameof(StatusColorPaletteIdentifiers))]
        public void StatusColorPalette_FromIdentifier_IdentifierShouldBeMapped(StatusColorPalette.Identifier identifier)
        {
            Action act = () => StatusColorPalette.FromIdentifier(identifier);

            act.Should().NotThrow(because: $"every identifier for {nameof(Theme)} should be mapped to a corresponding static color that is defined");
        }

        [Theory]
        [MemberData(nameof(ColorPaletteIdentifiers))]
        public void ColorPalette_FromIdentifier_IdentifierShouldBeMapped(ColorPalette.Identifier identifier)
        {
            Action act = () => ColorPalette.FromIdentifier(identifier);

            act.Should().NotThrow(because: $"every identifier for {nameof(Theme)} should be mapped to a corresponding static color that is defined");
        }


        // Test data sets

        public static IEnumerable<object[]> ThemeIdentifiers()
        {
            foreach (var number in Enum.GetValues(typeof(Theme.Identifier)))
            {
                yield return new object[] { number };
            }
        }

        public static IEnumerable<object[]> StatusColorPaletteIdentifiers()
        {
            foreach (var number in Enum.GetValues(typeof(StatusColorPalette.Identifier)))
            {
                yield return new object[] { number };
            }
        }

        public static IEnumerable<object[]> ColorPaletteIdentifiers()
        {
            foreach (var number in Enum.GetValues(typeof(ColorPalette.Identifier)))
            {
                yield return new object[] { number };
            }
        }
    }
}
