﻿using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace Common.Common.Validators
{
    public class NpivalidationRule : ValidationRule
    {
        public bool InvalidNpi { get; private set; }

        /// <summary>
        ///     This method will first add the provided numbers to a list and then parse them as individual characters because the
        ///     Luhn algorithm requires
        ///     That the last digit be calculated from the previous 9 digits. As a result, the Provided number can be verified by
        ///     using the Luhn algorithm
        ///     On the previous 9 digits to make sure the last digit matches. If the provided and calculated Check digit do not
        ///     match it is invalid.
        /// </summary>
        /// <param name="npi"></param>
        public void ParseNpi(string npi)
        {
            var number = new List<int>();

            AddNpiDigitsToList(npi, number);

            if (number.Count == 10)
            {
                var sum = this.PreSumCheckDigit(number);
                sum = sum % 10 != 0 ? CheckIfSumCanBeDividedBy10(sum) : 0;
                this.InvalidNpi = sum != number[9];
            }

            else
            {
                this.InvalidNpi = true;
            }
        }

        private static int CheckIfSumCanBeDividedBy10(int sum)
        {
            sum = sum + (10 - sum % 10) - sum;
            return sum;
        }

        private int PreSumCheckDigit(List<int> number)
        {
            var sum = SumDigits(number[8] * 2) + SumDigits(number[6] * 2) + SumDigits(number[4] * 2) +
                      SumDigits(number[2] * 2) + SumDigits(number[0] * 2) + 24;
            sum += number[1] + number[3] + number[5] + number[7];
            return sum;
        }

        private static void AddNpiDigitsToList(string npi, List<int> number)
        {
            number.AddRange(npi.Select(a => (int) char.GetNumericValue(a)));
        }

        private static int SumDigits(int number)
        {
            var sum = 0;
            while (number > 0)
            {
                sum += number % 10;
                number = number / 10;
            }

            return sum;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null) this.ParseNpi(value.ToString());
            return this.InvalidNpi ? new ValidationResult(false, null) : new ValidationResult(true, null);
        }
    }
}