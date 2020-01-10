using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Converters.ValueConverters
{
    /// <summary>
    /// Converters an boolean input value to it's respective <see cref="TrueObject"/> or <see cref="FalseObject"/> depending on the <see cref="Inverted"/> value
    /// </summary>
    public class BoolToObjectConverter : IValueConverter, IMarkupExtension
    {
        /// <summary>
        /// The value that will return if the boolean input is true
        /// <remarks>Will be the return value if <see cref="Inverted"/> is set to true</remarks>
        /// </summary>
        public object? TrueObject { get; set; }
        /// <summary>
        /// The value that will return if the boolean input is false
        /// <remarks>Will be the return value if <see cref="Inverted"/> is set to false</remarks>
        /// </summary>
        public object? FalseObject { get; set; }
        /// <summary>
        /// A boolean value to set if the output value should be inverted
        /// </summary>
        public bool Inverted { get; set; }

        /// <inheritdoc />
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool inputValue)) throw new ArgumentException($"Input value has to be of type {nameof(Boolean)}");
            if (TrueObject == null) throw new ArgumentException($"{nameof(TrueObject)} can not be null");
            if(FalseObject == null) throw new ArgumentException($"{nameof(FalseObject)} can not be null");
            if (TrueObject.GetType() != FalseObject.GetType())
                throw new ArgumentException($"{nameof(TrueObject)} has to be the same type as {FalseObject}");

            return Inverted ? (inputValue) ? FalseObject : TrueObject : (inputValue) ? TrueObject : FalseObject;
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
