using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace vaccine_appoint_report.Validations
{
    public class StringToNumeric : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex pattern = new("^[0-9]*$");
            if (value is not null && pattern.IsMatch(value.ToString()))
            {
                return new ValidationResult(true, null);
            }

            return new ValidationResult(false, "Please enter a valid integer value.");
        }
    }
}
