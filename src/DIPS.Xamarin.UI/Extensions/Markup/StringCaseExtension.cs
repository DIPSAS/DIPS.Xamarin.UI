using System;
using System.Globalization;
using DIPS.Xamarin.UI.Converters.ValueConverters;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Extensions.Markup
{
    /// <summary>
    /// Represents a string's casing
    /// </summary>
    public enum StringCase
    {
        /// <summary>
        /// No string case 
        /// </summary>
        /// <example>test string => test string</example>
        None = 0,
        /// <summary>
        /// Upper string case
        /// </summary>
        /// <example>Test string => test string</example>
        Upper,
        /// <summary>
        /// Lower string case
        /// </summary>
        /// <example>test string => TEST STRING</example>
        Lower,
        /// <summary>
        /// Title string case
        /// </summary>
        /// <example>test string => Test String</example>
        Title,
    }

    /// <summary>
    /// Converts a <see cref="Input"/> with a <see cref="StringCase"/>. This can be used with static values (like LocalizedStrings).
    /// To get the same functionality with a binding, <see cref="StringCaseConverter"/>
    /// </summary>
    [ContentProperty(nameof(Input))]
    public class StringCaseExtension : IMarkupExtension
    {
        /// <summary>
        /// The string input to use when converting
        /// </summary>
        public string? Input { get; set; }
        /// <summary>
        /// <see cref="StringCase"/> is used when converting
        /// </summary>
        public StringCase StringCase { get; set; }

        /// <inheritdoc/>
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrEmpty(Input))
            {
                return string.Empty;
            }

            switch (StringCase)
            {
                case StringCase.None:
#nullable disable
                    return Input;
#nullable restore
                case StringCase.Upper:
                    return CultureInfo.CurrentCulture.TextInfo.ToUpper(Input);
                case StringCase.Lower:
                    return CultureInfo.CurrentCulture.TextInfo.ToLower(Input);
                case StringCase.Title:
                    return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Input);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
