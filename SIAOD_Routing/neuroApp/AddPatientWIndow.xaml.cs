using neuroApp.Analyzes;
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
        private ApplicationContext db = new ApplicationContext();
        public ObjectiveStatus ObjectiveStatus { get; set; }

        public AddPatientWIndow()
        {
            InitializeComponent();

            ObjectiveStatus = new ObjectiveStatus();
            Patient patient = new Patient();
            patient.ObjectiveStatuses.Add(ObjectiveStatus);

            this.DataContext = patient;
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

        private void Button_addPatient_Click(object sender, RoutedEventArgs e)
        {
            
            db.Patients.Add(this.DataContext as Patient);
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
    }                                                           
}                                                               




                                                                