using neuroApp.Analyzes.ClinicalLaboratoryData;
using neuroApp.Analyzes.Complaint;
using neuroApp.Analyzes.HIV;
using neuroApp.Analyzes.HIVAssociateDisease;
using neuroApp.Analyzes.ObjectiveStatus;
using neuroApp.Analyzes.Tuberculosis;
using neuroApp.ListItems;
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
        //// создаем привязку команды
        //CommandBinding commandBinding = new CommandBinding();
        //// устанавливаем команду
        //commandBinding.Command = ApplicationCommands.Help;
        //// устанавливаем метод, который будет выполняться при вызове команды
        //commandBinding.Executed += CommandBinding_Executed;
        //// добавляем привязку к коллекции привязок элемента Button
        //helpButton.CommandBindings.Add(commandBinding);
        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Справка по приложению");
        }

        TextBoxValid validText = Validator.TextValidationTextBox;
        TextBoxValid validNumber = Validator.NumberValidationTextBox;

        public AddPatientWIndow()
        {
            InitializeComponent();

            Patient patient = new Patient();

            this.DataContext = this;
        }

        private void Button_addPatient_Click(object sender, RoutedEventArgs e)
        {

            BloodChemistry bloodChemistry = clinicalLaboratoryDataControl.FindResource("BloodChemistry") as BloodChemistry;
            Immunogram immunogram = clinicalLaboratoryDataControl.FindResource("Immunogram") as Immunogram;
            CompleteBloodCount completeBloodCount = clinicalLaboratoryDataControl.FindResource("CompleteBloodCount") as CompleteBloodCount;

            List<Complaint> complaints = complaintsControl.Complaints
                .Where(c => c.IsChecked == true)
                .Select(c => c.Item)
                .ToList();
            List<HIVAssociateDisease> hivAssociated = hivAssociateDiseaseControl.HIVAssociateDiseases
                .Where(c => c.IsChecked == true)
                .Select(c => c.Item)
                .ToList();
            HIVStage hivStage = hivStatusControl.HIVStage;
            HIVPhase hivPhase = hivStatusControl.HIVPhase;
            List<HIVStatus> hivStatuses = hivStatusControl.HIVStatuses
                .Where(w => w.IsChecked == true)
                .Select(s => s.Item)
                .ToList();
            int hivInfectionDuration = hivStatusControl.HIVInfectionDuration;

            Patient patient = new Patient
            {
                BloodChemistries = new List<BloodChemistry>() { bloodChemistry },
                Immunogramms = new List<Immunogram>() { immunogram },
                CompleteBloodCount = new List<CompleteBloodCount> { completeBloodCount },
                Complaints = complaints
            };



            using (ApplicationContext db = new ApplicationContext())
            {
                db.Patients.Add(this.DataContext as Patient);
                db.SaveChanges();
                this.DialogResult = true;
                this.Close();
            }
        }
    }
}




