﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace neuroApp.Analyzes.ObjectiveStatus
{
    public class ObjectiveStatus : INotifyPropertyChanged
    {
        private int heartRate;
        private int arterialPressure;
        private int frequencyOfResperatoryMovements;
        private double temperature;
        private int growth;
        private int weight;
        public ObjectiveStatus()
        {
            Patients = new List<Patient>();
        }
        public int HeartRate
        {
            get { return heartRate; }
            set
            {
                heartRate = value;
                OnPropertyChanged("HeartRate");
                OnPropertyChanged("Tachycardia");
            }
        }
        public int ArterialPressure
        {
            get { return arterialPressure; }
            set
            {
                arterialPressure = value;
                OnPropertyChanged("ArterialPressure");
            }
        }
        public int FrequencyOfResperatoryMovements
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
        public int Growth
        {
            get { return growth; }
            set
            {
                growth = value;
                OnPropertyChanged("Growth");
                OnPropertyChanged("IMT");
                OnPropertyChanged("DefecitMassiTela");
            }
        }
        public int Weight
        {
            get { return weight; }
            set
            {
                weight = value;
                OnPropertyChanged("Weight");
                OnPropertyChanged("IMT");
                OnPropertyChanged("DefecitMassiTela");
            }
        }

        public int Id { get; set; }

        public int Patient_id { get; set; }
        public int HealthState_id { get; set; }
        public List<Patient> Patients { get; set; }
        public HealthState HealthState { get; set; }
        public List<ObjectiveStatusDisease> ObjectiveStatusDiseases { get; set; }

        public double IMT => Math.Round(((double)weight / (((double)growth * (double)growth) / 10_000)), 2);
        public bool Tachycardia => HeartRate > 85;
        public bool Pirexia => Temperature > 37;
        public bool BodyWeightDefecit => IMT < 18.5;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}