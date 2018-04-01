using neuroApp.Analyzes.Complaint;
using neuroApp.Analyzes.ObjectiveStatus;
using neuroApp.Analyzes.Tuberculosis;
using neuroApp.View.AddPatientTabs;
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
        
        public ObservableCollection<Complaint> Complaints { get; set; }
        
        
        public ObservableCollection<DrugResistance> DrugResistances { get; set; }
        public ObservableCollection<TuberculosisForm> TuberculosisForms { get; set; }

        TextBoxValid validText = Validator.TextValidationTextBox;
        TextBoxValid validNumber = Validator.NumberValidationTextBox;

        public AddPatientWIndow()
        {
            InitializeComponent();
            GetComplaintsFromDB();

            
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
                
                DrugResistances = new ObservableCollection<DrugResistance>(db
                    .DrugResistances
                    .ToList());
            }

        }
        

        private void Button_addPatient_Click(object sender, RoutedEventArgs e)
        {

            db.Patients.Add(this.DataContext as Patient);
            db.SaveChanges();
            this.DialogResult = true;
            this.Close();
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




