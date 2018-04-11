using neuroApp.Analyzes.AccompanyingIllness;
using neuroApp.Analyzes.ClinicalLaboratoryData;
using neuroApp.Analyzes.Complaint;
using neuroApp.Analyzes.HIV;
using neuroApp.Analyzes.HIVAssociateDisease;
using neuroApp.Analyzes.Tuberculosis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace neuroApp
{
    public class Patient : INotifyPropertyChanged
    {
        private string name;
        private string family;
        private string otchestvo;
        private string birthday;

        private TuberculosisForm tuberculosisForm;
        private ICollection<HIVStatus> hivStatuses;
        private ICollection<BloodChemistry> bloodChemisrties;
        private ICollection<CompleteBloodCount> completeBloodCount;
        private ICollection<Complaint> complaints;
        private ICollection<DrugResistance> drugResistances;
        private ICollection<HIVAssociateDisease> hivAssociateDiseases;
        private ICollection<HIV> hivs;
        private ICollection<TuberculosisStatus> tuberculosisStatuses;
        private ICollection<Immunogram> immunograms;
        private string criminalArticle;
        private string beginDate;
        private string endDate;
        private string address;

        public Patient()
        {
            //immunograms = new List<Immunogram>();
            //tuberculosisStatuses = new List<TuberculosisStatus>();
            //hivStatuses = new List<HIVStatus>();
        }


        public int Id { get; set; }
        public int TuberculosisForm_id { get; set; }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Family
        {
            get { return family; }
            set
            {
                family = value;
                OnPropertyChanged("Family");
            }
        }
        public string Otchestvo
        {
            get { return otchestvo; }
            set
            {
                otchestvo = value;
                OnPropertyChanged("Otchestvo");
            }
        }

        public string Birthday
        {
            get { return birthday; }
            set
            {
                birthday = value;
                OnPropertyChanged("Birthday");
            }
        }

        public string CriminalArticle
        {
            get { return criminalArticle; }
            set
            {
                criminalArticle = value;
                OnPropertyChanged("CriminalArticle");
            }
        }

        public string BeginDate
        {
            get { return beginDate; }
            set
            {
                beginDate = value;
                OnPropertyChanged("BeginDate");
            }
        }

        public string EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                OnPropertyChanged("EndDate");
            }
        }

        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }

        public TuberculosisForm TuberculosisForm
        {
            get { return tuberculosisForm; }
            set
            {
                tuberculosisForm = value;
                OnPropertyChanged("TuberculosisForm");
            }
        }
        public ICollection<BloodChemistry> BloodChemistries
        {
            get { return bloodChemisrties; }
            set
            {
                bloodChemisrties = value;
                OnPropertyChanged("BloodChemistries");
            }
        }
        public ICollection<CompleteBloodCount> CompleteBloodCount
        {
            get { return completeBloodCount; }
            set
            {
                completeBloodCount = value;
                OnPropertyChanged("CompleteBloodCount");
            }
        }
        public ICollection<Complaint> Complaints
        {
            get { return complaints; }
            set
            {
                complaints = value;
                OnPropertyChanged("Complaints");
            }
        }
        public ICollection<DrugResistance> DrugResistances
        {
            get { return drugResistances; }
            set
            {
                drugResistances = value;
                OnPropertyChanged("DrugResistances");
            }
        }
        public ICollection<HIV> HIVs
        {
            get { return hivs; }
            set
            {
                hivs = value;
                OnPropertyChanged("HIV");
            }
        }
        public ICollection<HIVAssociateDisease> HIVAssociateDiseases
        {
            get { return hivAssociateDiseases; }
            set
            {
                hivAssociateDiseases = value;
                OnPropertyChanged("HIVAssociateDiseases");
            }
        }
        public ICollection<Immunogram> Immunogramms
        {
            get { return immunograms; }
            set
            {
                immunograms = value;
                OnPropertyChanged("Immunogramms");
            }
        }
        public ICollection<TuberculosisStatus> TuberculosisStatuses
        {
            get { return tuberculosisStatuses; }
            set
            {
                tuberculosisStatuses = value;
                OnPropertyChanged("TuberculosisStatuses");
            }
        }

        
        public ICollection<HIVStatus> HIVStatuses
        {
            get { return hivStatuses; }
            set
            {
                if (value != null || value != hivStatuses) hivStatuses = value;
                OnPropertyChanged("HIVStatuses");
            }
        }


        private ICollection<AccompanyingIllness> _AccompanyingUllnesses;
        public ICollection<AccompanyingIllness> AccompanyingUllnesses
        {
            get { return _AccompanyingUllnesses; }
            set
            {
                if (value != null || value != _AccompanyingUllnesses) _AccompanyingUllnesses = value;
                OnPropertyChanged("AccompanyingUllnesses");
            }
        }

        private int _HIVInfectionDuration;
        public int HIVInfectionDuration
        {
            get { return _HIVInfectionDuration; }
            set
            {
                if (value != null || value != _HIVInfectionDuration) _HIVInfectionDuration = value;
                OnPropertyChanged("HIVInfectionDuration");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
