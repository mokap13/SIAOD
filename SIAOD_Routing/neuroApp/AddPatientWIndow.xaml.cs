using neuroApp.Analyzes.AccompanyingIllness;
using neuroApp.Analyzes.ClinicalLaboratoryData;
using neuroApp.Analyzes.Complaint;
using neuroApp.Analyzes.HIV;
using neuroApp.Analyzes.HIVAssociateDisease;
using neuroApp.Analyzes.ObjectiveStatus;
using neuroApp.Analyzes.Tuberculosis;
using neuroApp.Commands;
using neuroApp.ListItems;
using neuroApp.Model;
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

                        List<HIVStatus> hivStatuses = hivStatusControl.HIVStatuses
                            .Where(w => w.IsChecked == true)
                            .Select(s => s.Item)
                            .ToList();

                        ObjectiveStatus objectiveStatus = objectiveStatusControl.FindResource("ObjectiveStatus") as ObjectiveStatus;
                        List<ObjectiveStatusDisease> objectiveStatusDisease = objectiveStatusControl.ObjectiveStatusDiseases
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

                        using (ApplicationContext db = new ApplicationContext())
                        {
                            HIV hiv = new HIV()
                            {
                                Stage = db.HIVStages
                                    .AsEnumerable()
                                    .First(f => f.Name == hivStatusControl.HIVStage.Name),
                                Phase = db.HIVPhases
                                    .AsEnumerable()
                                    .First(f => f.Name == hivStatusControl.HIVPhase.Name),
                                Duration = hivStatusControl.HIVInfectionDuration
                            };


                            objectiveStatus.HealthState = db
                                .HealthStates
                                .AsEnumerable()
                                .First(f => f.Name == objectiveStatusControl.HealthState.Name);

                            Patient patient = new Patient()
                            {
                                Name =            personalDataControl.PatientName,
                                Family =          personalDataControl.Family,
                                Otchestvo =       personalDataControl.Otchestvo,
                                Birthday =        personalDataControl.Birthday,
                                CriminalArticle = personalDataControl.CriminalArticle,
                                BeginDate =       personalDataControl.BeginDate,
                                EndDate =         personalDataControl.EndDate,
                                Address =         personalDataControl.Address,

                                BloodChemistries = new List<BloodChemistry> { bloodChemistry },
                                Immunograms = new List<Immunogram>() { immunogram },
                                CompleteBloodCount = new List<CompleteBloodCount> { completeBloodCount },
                                ObjectiveStatuses = new List<ObjectiveStatus> { objectiveStatus },

                                HIVs = new List<HIV> { hiv },
                            };
                            patient.ObjectiveStatuses.Add(objectiveStatus);

                            foreach (ObjectiveStatusDisease item in objectiveStatusDisease)
                            {
                                patient.ObjectiveStatusDiseases.Add(db
                                    .ObjectiveStatusDiseases
                                    .AsEnumerable()
                                    .First(f => f.Name == item.Name));
                            }
                            foreach (HIVAssociateDisease item in hivAssociateDisease)
                            {
                                patient.HIVAssociateDiseases.Add(db
                                    .HIVAssociateDiseases
                                    .AsEnumerable()
                                    .First(f => f.Name == item.Name));
                            }
                            foreach (TuberculosisStatus item in tuberculosisStatuses)
                            {
                                patient.TuberculosisStatuses.Add(db
                                    .TuberculosisStatuses
                                    .AsEnumerable()
                                    .First(f => f.Name == item.Name));
                            }
                            foreach (HIVStatus item in hivStatuses)
                            {
                                patient.HIVStatuses.Add(db
                                    .HIVStatuses
                                    .AsEnumerable()
                                    .First(f => f.Name == item.Name));
                            }
                            foreach (DrugResistance item in drugResistances)
                            {
                                patient.DrugResistances.Add(db
                                    .DrugResistances
                                    .AsEnumerable()
                                    .First(f => f.Name == item.Name));
                            }
                            foreach (Complaint item in complaints)
                            {
                                patient.Complaints.Add(db
                                    .Complaints
                                    .AsEnumerable()
                                    .First(f => f.Name == item.Name));
                            }
                            patient.TuberculosisForm = db
                                .TuberculosisForms
                                .AsEnumerable()
                                .First(f => f.Name == tuberculosisForm.Name);
                            foreach (AccompanyingIllness item in accompanyingIllness)
                            {
                                patient.AccompanyingIllnesses.Add(db
                                    .AccompanyingIllnesses
                                    .AsEnumerable()
                                    .First(f => f.Name == item.Name));
                            }

                            db.Patients.Add(patient);
                            db.SaveChanges();
                            this.DialogResult = true;
                            this.Close();
                        }
                    }));
            }
        }
        private RelayCommand _setDefaultDate;

        /// <summary>
        /// Gets the SetDefautDate.
        /// </summary>
        public RelayCommand SetDefautDate
        {
            get
            {
                return _setDefaultDate
                    ?? (_setDefaultDate = new RelayCommand(
                    (obj) =>
                    {
                        (obj as DatePicker).SelectedDate = DateTime.Now;
                    }));
            }
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
