using System.Globalization;
using System.Windows.Controls;

namespace Common.Common
{
    public class StringToDecimalValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {

            if (decimal.TryParse(value.ToString(), out decimal d))
            {
                return new ValidationResult(true, null);
            }

            else
            {
                return new ValidationResult(false, "Please enter a valid number");
            } 
        }
    }
}
