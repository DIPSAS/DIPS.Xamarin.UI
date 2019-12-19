using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DIPS.Xamarin.UI.Extensions
{
    /// <summary>
    /// Extensions class for DateTime
    /// </summary>
    public static class DateTimeExtensions
    {

        /// <summary>
        /// Checks if the date occured to day
        /// </summary>
        /// <param name="dateTime">The datetime to check</param>
        /// <returns>Boolean value</returns>
        public static bool IsToday(this DateTime dateTime) => dateTime.Date == DateTime.Today.Date;

        /// <summary>
        /// Checks if the date occured yesterday
        /// </summary>
        /// <param name="dateTime">The datetime to check</param>
        /// <returns>Boolean value</returns>
        public static bool IsYesterday(this DateTime dateTime)
        {
            return dateTime.Date == DateTime.Now.AddDays(-1).Date;
        }

        /// <summary>
        /// Checks if the date occurs tomorrow
        /// </summary>
        /// <param name="dateTime">The datetime to check</param>
        /// <returns>Boolean value</returns>
        public static bool IsTomorrow(this DateTime dateTime)
        {
            return dateTime.Date == DateTime.Now.AddDays(1).Date;
        }

        /// <summary>
        /// Gets the correct day suffix for a date
        /// </summary>
        /// <param name="dateTime">The datetime to get the suffix from</param>
        /// <returns>a string with the correct suffix</returns>
        public static string GetDaySuffix(this DateTime dateTime)
        {
            switch (dateTime.Day)
            {
                case 1:
                case 21:
                case 31:
                    return "st";
                case 2:
                case 22:
                    return "nd";
                case 3:
                case 23:
                    return "rd";
                default:
                    return "th";
            }
        }
    }
}
