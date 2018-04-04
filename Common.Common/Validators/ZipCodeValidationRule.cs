using System.Globalization;
using System.Windows.Controls;

namespace Common.Common.Validators
{
    public class ZipCodeValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (ParseZipCodeLength(value.ToString()))
                return new ValidationResult(true, null);

            return new ValidationResult(false, null);
        }


        private bool ParseZipCodeLength(string zipCode)
        {
            var validLength = false;
            if (zipCode.Length == 5 || zipCode.Length == 9)
            {
                validLength = true;
                return validLength;
            }

            return validLength;
        }
    }
}