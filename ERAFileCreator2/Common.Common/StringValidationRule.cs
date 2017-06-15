using System.Globalization;
using System.Windows.Controls;

namespace Common.Common
{
    public class StringValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(value.ToString()==null)
            {
                
                return new ValidationResult(false, null);
            }

            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
