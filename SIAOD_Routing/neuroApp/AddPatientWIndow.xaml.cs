using neuroApp.Analyzes.Complaint;
using neuroApp.Analyzes.ObjectiveStatus;
using neuroApp.Analyzes.Tuberculosis;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public ObservableCollection<Complaint> Complaints { get; set; }
        public ObservableCollection<HealthState> HealthStates { get; set; }
        public ObservableCollection<ObjectiveStatusDisease> ObjectiveStatusDiseases { get; set; }
        public ObservableCollection<DrugResistance> DrugResistances { get; set; }
        public ObservableCollection<TuberculosisForm> TuberculosisForms { get; set; }

        public AddPatientWIndow()
        {
            InitializeComponent();
            GetComplaintsFromDB();

            ObjectiveStatus = new ObjectiveStatus();
            Patient patient = new Patient();

            this.DataContext = this;
        }

        private void GetComplaintsFromDB()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Complaints = new ObservableCollection<Complaint>(db
                    .Complaints
                    .ToList());
                HealthStates = new ObservableCollection<HealthState>(db
                    .HealthStates
                    .ToList());
                DrugResistances = new ObservableCollection<DrugResistance>(db
                    .DrugResistances
                    .ToList());
                ObjectiveStatusDiseases = new ObservableCollection<ObjectiveStatusDisease>(db
                    .ObjectiveStatusDiseases
                    .ToList());

            }

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
            label_age.Content = age.ToString() + " - возраст";
        }

        private void Button_addPatient_CheckIsEnabled()
        {
            if ((textBox_Family.Text != String.Empty)
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
            List<string> drugResistances = new List<string>();
            foreach (DrugResistance item in (sender as ListBox).SelectedItems)
            {
                drugResistances.Add(item.Name);
            }

            if (drugResistances.Count != 0)
            {
                if (drugResistances.Contains("H")
                && (drugResistances.Contains("R")
                || drugResistances.Contains("Rb")))
                {
                    if ((drugResistances.Contains("Mfx")
                        || drugResistances.Contains("Lfx")
                        || drugResistances.Contains("Ofx"))
                        && (drugResistances.Contains("Cm")
                        || drugResistances.Contains("Am")
                        || drugResistances.Contains("Km")))
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
            }        }

        private void ListtBox_medicamentResist_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}




