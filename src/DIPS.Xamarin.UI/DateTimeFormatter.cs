using System;
using System.Globalization;
using DIPS.Xamarin.UI.Converters.ValueConverters;
using DIPS.Xamarin.UI.Extensions;
using DIPS.Xamarin.UI.Resources.LocalizedStrings;

namespace DIPS.Xamarin.UI
{
    /// <summary>
    /// </summary>
    public class DateTimeFormatter
    {
        private const string Space = " ";

        /// <summary>
        ///     Converts the Date and TimeSpan of the given DateTime to the correct short or text format.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="culture"></param>
        /// <param name="ignoreLocalTime"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string FormatDateAndTime(DateTime dateTime, CultureInfo culture, bool ignoreLocalTime = false,
            DateAndTimeConverter.DateAndTimeConverterFormat format =
                DateAndTimeConverter.DateAndTimeConverterFormat.Default)
        {
            return format switch
            {
                DateAndTimeConverter.DateAndTimeConverterFormat.Short => FormatShortDateAndTime(dateTime, culture,
                    ignoreLocalTime),
                DateAndTimeConverter.DateAndTimeConverterFormat.Text => FormatTextDateAndTime(dateTime, culture,
                    ignoreLocalTime),
                _ => throw new ArgumentOutOfRangeException(nameof(format), format, null)
            };
        }
        
        /// <summary>
        /// <see cref="FormatDateAndTime(System.DateTime,System.Globalization.CultureInfo,bool,DIPS.Xamarin.UI.Converters.ValueConverters.DateAndTimeConverter.DateAndTimeConverterFormat)"/> with <see cref="CultureInfo.CurrentCulture"/>.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="ignoreLocalTime"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string FormatDateAndTime(DateTime dateTime, bool ignoreLocalTime = false,
            DateAndTimeConverter.DateAndTimeConverterFormat format =
                DateAndTimeConverter.DateAndTimeConverterFormat.Default)
        {
            return FormatDateAndTime(dateTime, CultureInfo.CurrentCulture, ignoreLocalTime, format);
        }

        /// <summary>
        ///     Converts the Date of the given DateTime to the correct short or text format.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="cultureInfo"></param>
        /// <param name="ignoreLocalTime"></param>
        /// <param name="dateFormat"></param>
        /// <returns></returns>
        public static string FormatDate(DateTime dateTime, CultureInfo cultureInfo, bool ignoreLocalTime = false,
            DateConverter.DateConverterFormat dateFormat = DateConverter.DateConverterFormat.Default)
        {
            return dateFormat switch
            {
                DateConverter.DateConverterFormat.Short => ConvertToDefaultDateTime(dateTime, cultureInfo,
                    ignoreLocalTime),
                DateConverter.DateConverterFormat.Text => ConvertDateTimeAsText(dateTime, cultureInfo, ignoreLocalTime),
                _ => string.Empty
            };
        }
        
        /// <summary>
        /// <see cref="FormatDate(System.DateTime,System.Globalization.CultureInfo,bool,DIPS.Xamarin.UI.Converters.ValueConverters.DateConverter.DateConverterFormat)"/> with <see cref="CultureInfo.CurrentCulture"/>
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="ignoreLocalTime"></param>
        /// <param name="dateFormat"></param>
        /// <returns></returns>
        public static string FormatDate(DateTime dateTime, bool ignoreLocalTime = false,
            DateConverter.DateConverterFormat dateFormat = DateConverter.DateConverterFormat.Default)
        {
            return FormatDate(dateTime, CultureInfo.CurrentCulture, ignoreLocalTime, dateFormat);
        }

        /// <summary>
        ///     Converts the given DateTime or TimeSpan to the correct default format.
        /// </summary>
        /// <param name="value">Must be either a <see cref="DateTime" /> or <see cref="TimeSpan" /></param>
        /// <param name="cultureInfo"></param>
        /// <param name="ignoreLocalTime"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string FormatTime(object value, CultureInfo cultureInfo, bool ignoreLocalTime = false,
            TimeConverter.TimeConverterFormat format = TimeConverter.TimeConverterFormat.Default)
        {
            if (value == null)
            {
                return string.Empty;
            }

            if (value is not DateTime && value is not TimeSpan)
            {
                throw new ArgumentException("The input has to be of type DateTime or TimeSpan");
            }

            var dateTime = DateTime.MinValue;
            switch (value)
            {
                case TimeSpan timeSpanInput:
                    dateTime += timeSpanInput;
                    break;
                case DateTime dateTimeValue:
                    dateTime = ignoreLocalTime ? dateTimeValue : dateTimeValue.ToLocalTime();
                    break;
            }

            return format switch
            {
                TimeConverter.TimeConverterFormat.Default => FormatTime(dateTime, cultureInfo),
                _ => string.Empty
            };
        }
        
        /// <summary>
        /// <see cref="FormatTime(object,System.Globalization.CultureInfo,bool,DIPS.Xamarin.UI.Converters.ValueConverters.TimeConverter.TimeConverterFormat)"/> with <see cref="CultureInfo.CurrentCulture"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ignoreLocalTime"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string FormatTime(object value,  bool ignoreLocalTime = false,
            TimeConverter.TimeConverterFormat format = TimeConverter.TimeConverterFormat.Default)
        {
            return FormatTime(value, CultureInfo.CurrentCulture, ignoreLocalTime, format);
        }

        /// <summary>
        ///     Converts the given DateTime to the correct time string.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string FormatTime(DateTime dateTime, CultureInfo culture)
        {
            var time = dateTime.ToString("HH:mm", culture);
            if (culture.IsNorwegian())
            {
                var hour = dateTime.ToString("HH", culture);
                var minutes = dateTime.ToString("mm", culture);
                time = $"{hour}:{minutes}";
            }

            if (culture.ThreeLetterWindowsLanguageName.Equals("ENU"))
            {
                time = dateTime.ToString("hh:mm tt", culture);
            }

            return time;
        }
        
        /// <summary>
        /// <see cref="FormatTime(object,System.Globalization.CultureInfo,bool,DIPS.Xamarin.UI.Converters.ValueConverters.TimeConverter.TimeConverterFormat)"/> with <see cref="CultureInfo.CurrentCulture"/>
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string FormatTime(DateTime dateTime)
        {
            var culture = CultureInfo.CurrentCulture;
            var time = dateTime.ToString("HH:mm", culture);
            if (culture.IsNorwegian())
            {
                var hour = dateTime.ToString("HH", culture);
                var minutes = dateTime.ToString("mm", culture);
                time = $"{hour}:{minutes}";
            }

            if (culture.ThreeLetterWindowsLanguageName.Equals("ENU"))
            {
                time = dateTime.ToString("hh:mm tt", culture);
            }

            return time;
        }

        private static string ConvertToDefaultDateTime(DateTime dateTime, CultureInfo culture, bool ignoreLocalTime)
        {
            if (!ignoreLocalTime)
            {
                dateTime = dateTime.ToLocalTime();
            }

            var day = GetDayBasedOnCulture(dateTime, culture);

            var month = GetMonthBasedOnCulture(dateTime, culture);
            var year = dateTime.ToString("yyyy", culture);
            if (culture.ThreeLetterWindowsLanguageName.Equals("ENU"))
            {
                return $"{month}{Space}{day}{Space}{year}";
            }

            return $"{day}{Space}{month}{Space}{year}";
        }

        private static string GetDayBasedOnCulture(DateTime dateTime, CultureInfo culture)
        {
            var day = dateTime.ToString("dd", culture);
            if (culture.TwoLetterISOLanguageName.Contains("en"))
            {
                day = day.TrimStart('0');
                day += dateTime.GetEnglishDaySuffix();
            }

            if (culture.ThreeLetterWindowsLanguageName.Equals("ENU"))
            {
                day += ",";
            }

            if (culture.IsNorwegian())
            {
                day += ".";
            }

            return day;
        }

        private static string ConvertDateTimeAsText(DateTime dateTime, CultureInfo culture, bool ignoreLocalTime)
        {
            if (!ignoreLocalTime)
            {
                dateTime = dateTime.ToLocalTime();
            }

            if (dateTime.IsToday())
            {
                return InternalLocalizedStrings.Today;
            }

            if (dateTime.IsYesterday())
            {
                return InternalLocalizedStrings.Yesterday;
            }

            if (dateTime.IsTomorrow())
            {
                return InternalLocalizedStrings.Tomorrow;
            }

            var month = GetMonthBasedOnCulture(dateTime, culture);
            var day = GetDayBasedOnCulture(dateTime, culture);

            if (culture.ThreeLetterWindowsLanguageName.Equals("ENU"))
            {
                return $"{month}{Space}{day}";
            }

            return $"{day}{Space}{month}";
        }

        private static string GetMonthBasedOnCulture(DateTime dateTime, CultureInfo culture)
        {
            var month = dateTime.ToString("MMM", culture);
            if (culture.TwoLetterISOLanguageName.Contains("en"))
            {
                month = month[0].ToString().ToUpper() + month.Substring(1);
            }

            return month;
        }

        private static string FormatTextDateAndTime(DateTime dateTimeInput, CultureInfo culture, bool ignoreLocalTime)
        {
            var date = FormatDate(dateTimeInput, culture, ignoreLocalTime, DateConverter.DateConverterFormat.Text);

            var time = FormatTime(dateTimeInput, culture, ignoreLocalTime);

            if (culture.IsNorwegian())
            {
                if (dateTimeInput.IsToday() || dateTimeInput.IsTomorrow() || dateTimeInput.IsYesterday())
                {
                    return $"{date},{Space}kl{Space}{time}";
                }

                return $"{date}{Space}kl{Space}{time}";
            }

            return $"{date}{Space}{time}";
        }

        private static string FormatShortDateAndTime(DateTime dateTimeInput, CultureInfo culture, bool ignoreLocalTime)
        {
            var date = FormatDate(dateTimeInput, culture, ignoreLocalTime);
            var time = FormatTime(dateTimeInput, culture, ignoreLocalTime);

            return culture.IsNorwegian() ? $"{date}{Space}kl{Space}{time}" : $"{date}{Space}{time}";
        }
    }
}