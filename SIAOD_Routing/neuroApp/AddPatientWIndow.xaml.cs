using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        private void ListBox_medicamentResist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var medicaments = (sender as ListBox).SelectedItems;

            if(medicaments.Count!=0)
            {
                if (medicaments.Contains("H")
                && (medicaments.Contains("R")
                || medicaments.Contains("Rb")))
                {
                    if ((medicaments.Contains("Mfx")
                        || medicaments.Contains("Lfx")
                        || medicaments.Contains("Ofx"))
                        && (medicaments.Contains("Cm")
                        || medicaments.Contains("Am")
                        || medicaments.Contains("Km")))
                    {
                        label_medicamentResist.Content = "ШЛУ";
                    }
                    else
                    {
                        label_medicamentResist.Content = "МЛУ";
                    }
                }
                else
                {
                    label_medicamentResist.Content = "ЛУ";
                }
            }
            else
            {
                label_medicamentResist.Content = "ЛУ отсутствует";
            }
        }

        private void ListtBox_medicamentResist_Loaded(object sender, RoutedEventArgs e)
        {
            medicamentResist.ItemsSource = new List<string>() {
                "H", "S", "R", "Rb", "E", "Pt",
                "Et", "Cm","Am", "Km",
                "PAS", "Cs", "Lnz", "Mfx",
                "Lfx", "Ofx", "Trd", "Bq",
                "Amx", "Imp", "Mp"
            };
        }

        private void TextBox_CHSS_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = (sender as TextBox).Text;
            if(!String.IsNullOrEmpty(text))
            {
                if (!Double.TryParse(text, out Double chss))
                {
                    chss = Double.MaxValue;
                    (sender as TextBox).Text = chss.ToString();
                }
                checkBox_tahicardia.IsChecked =
                    (chss > 85)
                    ? true
                    : false;
            }
            else
            {
                checkBox_lihoradka.IsChecked = false;
            }
        }

        private void TextBox_temperature_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = (sender as TextBox).Text;
            if (!String.IsNullOrEmpty(text))
            {
                if (!Double.TryParse(text, out Double temperature))
                {
                    temperature = Double.MaxValue;
                    (sender as TextBox).Text = temperature.ToString();
                }
                checkBox_lihoradka.IsChecked =
                    (temperature > 37)
                    ? true
                    : false;
            }
            else
            {
                checkBox_lihoradka.IsChecked = false;
            }
        }
    }                                                           
}                                                               




                                                                