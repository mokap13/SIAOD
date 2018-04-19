using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace neuroApp
{
    public static class Validator
    {
        static public void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            //Regex regex = new Regex("^[0-9]*[.][0-9]*$");
            //Regex regex = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");

            e.Handled = false;//regex.IsMatch(e.Text);
        }
        static public void TextValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^а-яА-ЯёЁa-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
