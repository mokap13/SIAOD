﻿using MahApps.Metro.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace neuroApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        ApplicationContext db = new ApplicationContext();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_AddPatient_Click(object sender, RoutedEventArgs e)
        {
            AddPatientWIndow addPatientWindow = new AddPatientWIndow();
            if (addPatientWindow.ShowDialog() == true)
            {
                var patients = db.Patients.ToList();
                dataGrid_Patients.ItemsSource = patients;
                dataGrid_Patients.Items.Refresh();
            }
        }

        private void DataGrid_Patients_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //dataGrid_Patients.ItemsSource = db.Patients.ToList();
                dataGrid_Patients.ItemsSource = db.Patients.Join(db.TuberculosisForms,
                    patient => patient.TuberculosisForm_id,
                    t => t.Id,
                    (p, t) => new
                    {
                        Name = p.Name,
                        TuberculosisForm = t.Name
                    }).ToList();
            }
            
            catch (Exception exception)
            {
                string message = null;
                message = $"\nНе удалось открыть базу данных\n {exception.Message}\n";
                
                if (exception.InnerException != null)
                    message += $"\n{exception.InnerException.Message}";
                MessageBox.Show(message);
            }  
        }

        private void Button_DeletePatient_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid_Patients.SelectedItems.Count != 0)
            {
                AnswerWindow answerWindow = new AnswerWindow("Вы уверены, что хотите удалить");
                if (answerWindow.ShowDialog() == true)
                {
                    foreach (Patient patient in dataGrid_Patients.SelectedItems)
                    {
                        db.Patients.Remove(patient);
                    }
                    db.SaveChanges();
                    var patients = db.Patients.ToList();
                    dataGrid_Patients.ItemsSource = patients;
                    dataGrid_Patients.Items.Refresh();
                }
            }
        }

        private void Button_RefreshDataGrid_Click(object sender, RoutedEventArgs e)
        {
            var patients = db.Patients.ToList();
            dataGrid_Patients.ItemsSource = patients;
            dataGrid_Patients.Items.Refresh();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            foreach (Window window in App.Current.Windows)
            {
                window.Close();
            }
        }

        private void Button_ChangePatient_Click(object sender, RoutedEventArgs e)
        {
            ChangePatientWindow changePatientWindow = new ChangePatientWindow();
            if(changePatientWindow.ShowDialog() == true)
            {

            }
        }
    }
}
