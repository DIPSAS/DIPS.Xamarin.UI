using System.Globalization;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Tests.TestHelpers
{
    public static class ValueConverterExtensions
    {
        /// <summary>
        /// Runs Convert with TargetType, Parameter and Culture = null
        /// </summary>
        /// <typeparam name="TOutput">The expected output</typeparam>
        /// <param name="valueConverter">The value converter</param>
        /// <param name="input">The input to use for the converter</param>
        /// <returns></returns>
        public static TOutput Convert<TOutput>(this IValueConverter valueConverter, object input)
        {
            return (TOutput)valueConverter.Convert(input, null, null, null);
        }

        /// <summary>
        /// Runs ConvertBack with TargetType, Parameter and Culture = null
        /// </summary>
        /// <typeparam name="TOutput">The expected output</typeparam>
        /// <param name="valueConverter">The value converter</param>
        /// <param name="input">The input to use for the converter</param>
        /// <returns></returns>
        public static TOutput ConvertBack<TOutput>(this IValueConverter valueConverter, object input)
        {
            return (TOutput)valueConverter.ConvertBack(input, null, null, null);
        }

        /// <summary>
        /// Runs Convert with TargetType and Parameter = null
        /// </summary>
        /// <typeparam name="TOutput">The expected output</typeparam>
        /// <param name="valueConverter">The value converter</param>
        /// <param name="input">The input to use for the converter</param>
        /// <param name="culture">The culture to use</param>
        /// <returns></returns>
        public static TOutput Convert<TOutput>(this IValueConverter valueConverter, object input, CultureInfo culture)
        {
            return (TOutput)valueConverter.Convert(input, null, null, culture);
        }
    }
}
