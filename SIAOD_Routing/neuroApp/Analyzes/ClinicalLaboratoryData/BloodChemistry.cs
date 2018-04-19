using neuroApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace neuroApp.Analyzes.ClinicalLaboratoryData
{
    public class BloodChemistry:INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int PatientId { get; set; }

        private double _ALT;
        public double ALT
        {
            get { return _ALT; }
            set
            {
                if ( value != _ALT) _ALT = value;
                OnPropertyChanged("ALT");
            }
        }
        public double AltTreshold
        {
            get { return 57.0; }
        }
        
        private double _AST;
        public double AST
        {
            get { return _AST; }
            set
            {
                if (value != _AST) _AST = value;
                OnPropertyChanged("AST");
            }
        }
        public double AstTreshold
        {
            get { return 192; }
        }
        private double _TotalBilirubin;
        public double TotalBilirubin
        {
            get { return _TotalBilirubin; }
            set
            {
                if (value != _TotalBilirubin) _TotalBilirubin = value;
                OnPropertyChanged("TotalBilitubin");
            }
        }
        public double TotalBilirubinTreshold
        {
            get { return 25.5; }
        }
        private double _Creatinine;
        public double Creatinine
        {
            get { return _Creatinine; }
            set
            {
                if ( value != _Creatinine) _Creatinine = value;
                OnPropertyChanged("Creatinine");
            }
        }
        public double CreatinineTreshold
        {
            get { return 180.6; }
        }
        private double _Glucose;
        public double Glucose
        {
            get { return _Glucose; }
            set
            {
                if (value != _Glucose) _Glucose = value;
                OnPropertyChanged("Glucose");
            }
        }

        private string _AnalyzeDate;
        public string AnalyzeDate
        {
            get { return _AnalyzeDate; }
            set
            {
                if (value != null || value != _AnalyzeDate) _AnalyzeDate = value;
                OnPropertyChanged("AnalyzeDate");
            }
        }
        public Patient Patient { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
    
}
