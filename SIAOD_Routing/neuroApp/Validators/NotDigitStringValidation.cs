using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace neuroApp.Validators
{
    class NotDigitStringValidation:ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            var result = new ValidationResult(false, "Нелзья вводить цифры");
            if (value is string && (value as string).All(a=>!char.IsDigit(a)))
            {
                result = new ValidationResult(true, null);
            }
            return result;
        }
    }
}
