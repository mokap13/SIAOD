using neuroApp.Analyzes.AccompanyingIllness;
using neuroApp.Analyzes.ClinicalLaboratoryData;
using neuroApp.Analyzes.Complaint;
using neuroApp.Analyzes.HIV;
using neuroApp.Analyzes.HIVAssociateDisease;
using neuroApp.Analyzes.ObjectiveStatus;
using neuroApp.Analyzes.Tuberculosis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace neuroApp.Model
{
    public class Patient : INotifyPropertyChanged
    {
        private string name;
        private string family;
        private string otchestvo;
        private string birthday;

        private string criminalArticle;
        private string beginDate;
        private string endDate;
        private string address;

        public Patient()
        {
            Immunograms = new List<Immunogram>();
            TuberculosisStatuses = new List<TuberculosisStatus>();
            HIVStatuses = new List<HIVStatus>();
            AccompanyingIllnesses = new List<AccompanyingIllness>();
            BloodChemistries = new List<BloodChemistry>();
            CompleteBloodCount = new List<CompleteBloodCount>();
            Complaints = new List<Complaint>();
            DrugResistances = new List<DrugResistance>();
            HIVAssociateDiseases = new List<HIVAssociateDisease>();
            HIVs = new List<HIV>();
            ObjectiveStatuses = new List<ObjectiveStatus>();
            ObjectiveStatusDiseases = new List<ObjectiveStatusDisease>();
        }


        public int Id { get; set; }
        public int TuberculosisFormId { get; set; }
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

        public virtual TuberculosisForm TuberculosisForm { get; set; }
        public virtual ICollection<HIVStatus> HIVStatuses { get; set; }
        public virtual ICollection<BloodChemistry> BloodChemistries { get; set; }
        public virtual ICollection<CompleteBloodCount> CompleteBloodCount { get; set; }
        public virtual ICollection<Complaint> Complaints { get; set; }
        public virtual ICollection<DrugResistance> DrugResistances { get; set; }
        public virtual ICollection<HIVAssociateDisease> HIVAssociateDiseases { get; set; }
        public virtual ICollection<HIV> HIVs { get; set; }
        public virtual ICollection<TuberculosisStatus> TuberculosisStatuses { get; set; }
        public virtual ICollection<Immunogram> Immunograms { get; set; }
        public virtual ICollection<AccompanyingIllness> AccompanyingIllnesses { get; set; }
        public virtual ICollection<ObjectiveStatus> ObjectiveStatuses { get; set; }
        public virtual ICollection<ObjectiveStatusDisease> ObjectiveStatusDiseases { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
