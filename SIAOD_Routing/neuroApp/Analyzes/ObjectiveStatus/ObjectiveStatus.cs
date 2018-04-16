using neuroApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace neuroApp.Analyzes.ObjectiveStatus
{
    public class ObjectiveStatus : INotifyPropertyChanged
    {
        private double heartRate;
        private double arterialPressure;
        private double frequencyOfResperatoryMovements;
        private double temperature; 
        private double growth;
        private double weight;
        
        public double HeartRate
        {
            get { return heartRate; }
            set
            {
                heartRate = value;
                OnPropertyChanged("HeartRate");
                OnPropertyChanged("Tachycardia");
            }
        }
        public double ArterialPressure
        {
            get { return arterialPressure; }
            set
            {
                arterialPressure = value;
                OnPropertyChanged("ArterialPressure");
            }
        }
        public double FrequencyOfResperatoryMovements
        {
            get { return frequencyOfResperatoryMovements; }
            set
            {
                frequencyOfResperatoryMovements = value;
                OnPropertyChanged("FrequencyOfResperatoryMovements");
            }
        }
        public double Temperature
        {
            get { return temperature; }
            set
            {
                temperature = value;
                OnPropertyChanged("Temperature");
                OnPropertyChanged("Pirexia");
            }
        }
        public double Growth
        {
            get { return growth; }
            set
            {
                growth = value;
                OnPropertyChanged("Growth");
                OnPropertyChanged("IMT");
                OnPropertyChanged("BodyWeightDefecit");
            }
        }
        public double Weight
        {
            get { return weight; }
            set
            {
                weight = value;
                OnPropertyChanged("Weight");
                OnPropertyChanged("IMT");
                OnPropertyChanged("BodyWeightDefecit");
            }
        }

        public int Id { get; set; }

        public Patient Patient { get; set; }
        public int PatientId { get; set; }
        public HealthState HealthState { get; set; }
        public int HealthStateId { get; set; }

        public double IMT => Math.Round(((double)weight / (((double)growth * (double)growth) / 10_000)), 2);
        public bool Tachycardia => HeartRate > 85;
        public bool Pirexia => Temperature > 37;
        public bool BodyWeightDefecit => IMT < 18.5;


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
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
