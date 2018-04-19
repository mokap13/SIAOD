using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace neuroApp.Validators
{
    class IsDoubleValidation:ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            var result = new ValidationResult(false, "Введите число");
            if (value is string && Double.TryParse((value as string),out double var))
            {
                result = new ValidationResult(true, null);
            }
            return result;
        }
    }
}
