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
    }
}
