using neuroApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace neuroApp.Analyzes.ClinicalLaboratoryData
{
    public class CompleteBloodCount:INotifyPropertyChanged
    {
        public Patient Patient { get; set; }

        public int Id { get; set; }
        public int PatientId { get; set; }

        private double esr;
        private double lymphocytes;
        private double platelets;
        private double erythrocytes;
        private double hemoglobin;
        private string analyzeDate;
        
        public double Hemoglobin
        {
            get { return hemoglobin; }
            set
            {
                hemoglobin = value;
                OnPropertyChanged("Hemoglobin");
            }
        }
        public double ESR
        {
            get { return esr; }
            set
            {
                esr = value;
                OnPropertyChanged("ESR");
            }
        }
        public double Lymphocytes
        {
            get { return lymphocytes; }
            set
            {
                lymphocytes = value;
                OnPropertyChanged("Lymphocytes");
            }
        }
        public double Platelets
        {
            get { return platelets; }
            set
            {
                platelets = value;
                OnPropertyChanged("Platelets");
            }
        }
        public double Erythrocytes
        {
            get { return erythrocytes; }
            set
            {
                erythrocytes = value;
                OnPropertyChanged("Erythrocytes");
            }
        }

        private double _Leukocytes;
        public double Leukocytes
        {
            get { return _Leukocytes; }
            set
            {
                if (value != _Leukocytes) _Leukocytes = value;
                OnPropertyChanged("Leukocytes");
            }
        }
        public string AnalyzeDate
        {
            get { return analyzeDate; }
            set
            {
                analyzeDate = value;
                OnPropertyChanged("AnalyzeDate");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
