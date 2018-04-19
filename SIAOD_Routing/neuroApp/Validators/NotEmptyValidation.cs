using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace neuroApp.Validators
{
    class NotEmptyValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            var result = new ValidationResult(false, "Не допустима пустая строка");
            if (value is string && !string.IsNullOrWhiteSpace(value.ToString()))
            {
                result = new ValidationResult(true, null);
            }
            return result;
        }
    }
}
