using neuroApp.Analyzes.AccompanyingIllness;
using neuroApp.Analyzes.ClinicalLaboratoryData;
using neuroApp.Analyzes.Complaint;
using neuroApp.Analyzes.HIV;
using neuroApp.Analyzes.HIVAssociateDisease;
using neuroApp.Analyzes.ObjectiveStatus;
using neuroApp.Analyzes.Tuberculosis;
using neuroApp.Commands;
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
        private RelayCommand _addPatient;

        /// <summary>
        /// Gets the AddPatient.
        /// </summary>
        public RelayCommand AddPatient
        {
            get
            {
                return _addPatient
                    ?? (_addPatient = new RelayCommand(obj =>
                    {
                        BloodChemistry bloodChemistry = clinicalLaboratoryDataControl.FindResource("BloodChemistry") as BloodChemistry;
                        Immunogram immunogram = clinicalLaboratoryDataControl.FindResource("Immunogram") as Immunogram;
                        CompleteBloodCount completeBloodCount = clinicalLaboratoryDataControl.FindResource("CompleteBloodCount") as CompleteBloodCount;

                        List<Complaint> complaints = complaintsControl.Complaints
                            .Where(c => c.IsChecked == true)
                            .Select(c => c.Item)
                            .ToList();
                        List<HIVAssociateDisease> hivAssociateDisease = hivAssociateDiseaseControl.HIVAssociateDiseases
                            .Where(c => c.IsChecked == true)
                            .Select(c => c.Item)
                            .ToList();
                        HIV hiv = new HIV()
                        {
                            HIVStage = hivStatusControl.HIVStage,
                            HIVPhase = hivStatusControl.HIVPhase,
                            Duration = hivStatusControl.HIVInfectionDuration
                        };
                        
                        List<HIVStatus> hivStatuses = hivStatusControl.HIVStatuses
                            .Where(w => w.IsChecked == true)
                            .Select(s => s.Item)
                            .ToList();

                        ObjectiveStatus objectiveStatus = objectiveStatusControl.FindResource("ObjectiveStatus") as ObjectiveStatus;
                        List<ObjectiveStatusDisease> objectiveStatusDiseases = objectiveStatusControl.ObjectiveStatusDiseases
                            .Where(w => w.IsChecked == true)
                            .Select(s => s.Item)
                            .ToList();

                        TuberculosisForm tuberculosisForm = tuberculosisStatusControl.TuberculosisForm;
                        List<DrugResistance> drugResistances = tuberculosisStatusControl.DrugResistances
                            .Where(w => w.IsChecked == true)
                            .Select(s => s.Item)
                            .ToList();
                        List<TuberculosisStatus> tuberculosisStatuses = tuberculosisStatusControl.TuberculosisStatuses
                            .Where(w => w.IsChecked == true)
                            .Select(s => s.Item)
                            .ToList();

                        List<AccompanyingIllness> accompanyingIllness = accompanyingIllnessControl.AccompanyingIllnesses
                            .Where(w => w.IsChecked == true)
                            .Select(s => s.Item)
                            .ToList();

                        Patient patient = new Patient
                        {
                            BloodChemistries = new List<BloodChemistry>() { bloodChemistry },
                            Immunogramms = new List<Immunogram>() { immunogram },
                            CompleteBloodCount = new List<CompleteBloodCount> { completeBloodCount },
                            ObjectiveStatus = new List<ObjectiveStatus> { objectiveStatus },
                            Complaints = complaints,
                            HIVAssociateDiseases = hivAssociateDisease,
                            HIVs = new List<HIV> { hiv },
                            HIVStatuses = hivStatuses,
                            TuberculosisForm = tuberculosisForm,
                            TuberculosisStatuses = tuberculosisStatuses,
                            AccompanyingIllnesses = accompanyingIllness
                        };

                        using (ApplicationContext db = new ApplicationContext())
                        {
                            db.Patients.Add(patient);
                            db.SaveChanges();
                            this.DialogResult = true;
                            this.Close();
                        }
                    }));
            }
        }

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
    }
}




