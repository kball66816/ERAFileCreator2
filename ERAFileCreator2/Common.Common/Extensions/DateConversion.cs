using System;

namespace Common.Common.Extensions
{
    /// <summary>
    ///     Since we cannot be 100% certain how we will receive the DateTime this will allow us to cast the DateTime
    ///     As needed to specified formats
    /// </summary>
    public static class DateConversion
    {
        public static string DateToYearFirstShortString(this DateTime date)
        {
            var dateAsString = date.ToString("yyyyMMdd");
            return dateAsString;
        }
    }
}