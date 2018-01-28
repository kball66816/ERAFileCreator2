using System;

namespace Common.Common.Extensions
{
    /// <summary>
    ///     Truncates decimal to user defined digits without rounding up or down.
    /// </summary>
    public static class DecimalExtensions
    {
        /// <summary>
        ///     This method deals with currency in particular, by using the below formula we can cut the decimal place to no more
        ///     than
        ///     2 digits without rounding up or down in the case of a typo.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="decimalPlaces"></param>
        /// <returns></returns>
        public static decimal Truncated(this decimal value, int decimalPlaces)
        {
            var modifier = Convert.ToDecimal(0.5 / Math.Pow(10, decimalPlaces));
            return Math.Round(value >= 0 ? value - modifier : value + modifier, decimalPlaces);
        }
    }
}