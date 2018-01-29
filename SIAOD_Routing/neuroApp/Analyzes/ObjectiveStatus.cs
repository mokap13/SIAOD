using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace neuroApp.Analyzes
{
    public class ObjectiveStatus : INotifyPropertyChanged
    {
        private KeySostoianie sostoianie;
        private int chss;
        private int arterialnoeDavlenie;
        private int chdd;
        private double temperature;
        private bool limfadenatopia;
        private bool arythmia;
        private bool splenomegalia;
        private bool gepatomegalia;
        private int growth;
        private int weight;
        public ObjectiveStatus()
        {
            Patients = new List<Patient>();
        }
        public KeySostoianie Sostoianie
        {
            get { return sostoianie; }
            set
            {
                sostoianie = value;
                OnPropertyChanged("Sostoianie");
            }
        }
        public int CHSS
        {
            get { return chss; }
            set
            {
                chss = value;
                OnPropertyChanged("CHSS");
                OnPropertyChanged("Tahicardia");
            }
        }
        public int ArterialnoeDavlenie
        {
            get { return arterialnoeDavlenie; }
            set
            {
                arterialnoeDavlenie = value;
                OnPropertyChanged("ArterialnoeDavlenie");
            }
        }
        public int CHDD
        {
            get { return chdd; }
            set
            {
                chdd = value;
                OnPropertyChanged("CHDD");
            }
        }
        public double Temperature
        {
            get { return temperature; }
            set
            {
                temperature = value;
                OnPropertyChanged("Temperature");
                OnPropertyChanged("Lihoradka");
            }
        }
        public bool Limfadenatopia
        {
            get { return limfadenatopia; }
            set
            {
                limfadenatopia = value;
                OnPropertyChanged("Limfadenatopia");
            }
        }
        public bool Arythmia
        {
            get { return arythmia; }
            set
            {
                arythmia = value;
                OnPropertyChanged("Arythmia");
            }
        }
        public bool Splenomegalia
        {
            get { return splenomegalia; }
            set
            {
                splenomegalia = value;
                OnPropertyChanged("Splenomegalia");
            }
        }
        public bool Gepatomegalia
        {
            get { return gepatomegalia; }
            set
            {
                gepatomegalia = value;
                OnPropertyChanged("Gepatomegalia");
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
        public List<Patient> Patients { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public double IMT => Math.Round(((double)weight / (((double)growth * (double)growth) / 10_000)), 2);
        public bool Tahicardia => CHSS > 85;
        public bool Lihoradka => Temperature > 37;
        public bool DefecitMassiTela => IMT < 18.5;
    }
    public enum KeySostoianie
    {
        light,
        mddle,
        hard
    }
}
