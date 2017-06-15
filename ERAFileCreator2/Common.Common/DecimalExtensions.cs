using System;

namespace Common.Common
{
    /// <summary>
    /// Truncates decimal to user defined digits without rounding up or down.
    /// </summary>
   public static class DecimalExtensions
    {
        public static decimal Truncated(this decimal value, int decimalPlaces)
        {
            decimal modifier = Convert.ToDecimal(0.5 / Math.Pow(10, decimalPlaces));
            return Math.Round(value >= 0 ? value - modifier : value + modifier, decimalPlaces);
        }
    }
}
