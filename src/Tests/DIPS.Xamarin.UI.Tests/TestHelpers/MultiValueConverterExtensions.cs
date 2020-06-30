using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Tests.TestHelpers
{
    public static class MultiValueConverterExtensions
    {
        /// <summary>
        /// Runs Convert with TargetType, Parameter and Culture = null
        /// </summary>
        /// <typeparam name="TOutput">The expected output</typeparam>
        /// <param name="valueConverter">The value converter</param>
        /// <param name="inputs">The inputs to use for the converter</param>
        /// <returns></returns>
        public static TOutput Convert<TOutput>(this IMultiValueConverter valueConverter, object[] inputs)
        {
            return (TOutput)valueConverter.Convert(inputs, null, null, null);
        }
    }
}
