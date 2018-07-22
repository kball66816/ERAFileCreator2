using System.Globalization;
using System.Windows.Controls;
using Common.Common.Extensions;

namespace Common.Common.Validators
{
    public class StringToDecimalValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null && decimal.TryParse(value.ToString(), out var d))
                return AreTwoDecimalsTheSame(d, d.Truncated(2));
            return new ValidationResult(false, "Please enter a valid number");
        }

        private static ValidationResult AreTwoDecimalsTheSame(decimal decimalOne, decimal decimalTwo)
        {
            var checkOne = decimalOne.ToString();
            var checkTwo = decimalTwo.ToString();
            if (checkOne.Length == checkTwo.Length)
                return new ValidationResult(true, null);
            return new ValidationResult(false, "Please enter a valid number");
        }
    }
}