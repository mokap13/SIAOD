using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public partial class AddPatientWIndow
    {
        ApplicationContext db = new ApplicationContext();
        public AddPatientWIndow()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void TextValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^а-яА-ЯёЁa-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void TextBox_patientHeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox_patientWeigth_TextChanged(sender,e);
        }
        private void TextBox_patientWeigth_TextChanged(object sender, TextChangedEventArgs e)
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

        private void Button_addPatient_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = new Patient()
            {
                Name = textBox_Name.Text,
                Family = textBox_Family.Text,
                Otchestvo = textBox_Otchestvo.Text,
                Birthday = datePicker_Birthday.SelectedDate.Value.ToString("dd.MM.yyyy")
            };
            db.Patients.Add(patient);
            db.SaveChanges();
            this.DialogResult = true;
            this.Close();
        }

        private void TextBox_Family_TextChanged(object sender, TextChangedEventArgs e)
        {
            Button_addPatient_CheckIsEnabled();
        }

        private void TextBox_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            Button_addPatient_CheckIsEnabled();
        }

        private void TextBox_Otchestvo_TextChanged(object sender, TextChangedEventArgs e)
        {
            Button_addPatient_CheckIsEnabled();
        }

        private void DatePicker_Birthday_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Button_addPatient_CheckIsEnabled();
            var age = (DateTime.Now.Year - datePicker_Birthday.SelectedDate.Value.Year);
            if (datePicker_Birthday.SelectedDate > DateTime.Now.AddYears(-age))
                age--;
            label_age.Content = age.ToString()+" - возраст";
        }

        private void Button_addPatient_CheckIsEnabled()
        {
            if( (textBox_Family.Text!=String.Empty)
                && (textBox_Name.Text != String.Empty)
                && (textBox_Otchestvo.Text != String.Empty)
                && (datePicker_Birthday.SelectedDate != null))
            {
                button_addPatient.IsEnabled = true;
            }
        }

        private void DatePicker_Birthday_Loaded(object sender, RoutedEventArgs e)
        {
            const int middleAge = 35;
            datePicker_Birthday.SelectedDate = new DateTime(DateTime.Now.Year - middleAge, 1, 1);
        }
    }
}
