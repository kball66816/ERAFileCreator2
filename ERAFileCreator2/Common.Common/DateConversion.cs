using System;

namespace Common.Common
{
    /// <summary>
    ///Since we cannot be 100% certain how we will receive the DateTime this will allow us to cast the DateTime
    /// As needed
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
