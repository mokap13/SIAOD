using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace neuroApp
{
    /// <summary>
    /// Логика взаимодействия для AddPatientWIndow.xaml
    /// </summary>
    public partial class AddPatientWIndow : Window
    {
        public AddPatientWIndow()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void textBox_patientHeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            textBox_patientWeigth_TextChanged(sender,e);
        }
        private void textBox_patientWeigth_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((textBox_patientHeight.Text != String.Empty)
                && textBox_patientWeigth.Text != String.Empty)
            {
                var weight = double.Parse(textBox_patientWeigth.Text);
                var heigth = double.Parse(textBox_patientHeight.Text);
                var IMT = Math.Round((weight / ((heigth * heigth) / 10_000)), 2);
                label_IMT.Content = string.Format(IMT.ToString(), "000");
            }
        }
    }
}
