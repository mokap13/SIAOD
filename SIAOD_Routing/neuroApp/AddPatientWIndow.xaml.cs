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
            }

        }

        private void Button_addPatient_Click(object sender, RoutedEventArgs e)
        {

            db.Patients.Add(this.DataContext as Patient);
            db.SaveChanges();
            this.DialogResult = true;
            this.Close();
        }

        

        private void ListtBox_medicamentResist_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}




