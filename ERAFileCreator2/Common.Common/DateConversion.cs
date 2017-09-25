using System;

namespace Common.Common
{
    /// <summary>
    /// Tostring conversion for datepicker passed to datetime
    /// </summary>
    public static class DateConversion
    {
        public static string ConvertedDate(this DateTime date)
        {
            string a = date.ToString("yyyyMMdd");
            return a;
        }
    }
}
