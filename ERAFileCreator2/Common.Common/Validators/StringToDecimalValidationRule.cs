using System.Globalization;
using System.Windows.Controls;

namespace Common.Common.Validators
{
    public class StringToDecimalValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return decimal.TryParse(value.ToString(), out decimal d) 
                ? new ValidationResult(true, null) 
                : new ValidationResult(false, "Please enter a valid number");
        }
    }
}
