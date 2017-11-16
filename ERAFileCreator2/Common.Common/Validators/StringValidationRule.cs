using System.Globalization;
using System.Windows.Controls;

namespace Common.Common.Validators
{
    public class StringValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return value.ToString()==null 
                ? new ValidationResult(false, null) 
                : new ValidationResult(true, null);
        }
    }
}
