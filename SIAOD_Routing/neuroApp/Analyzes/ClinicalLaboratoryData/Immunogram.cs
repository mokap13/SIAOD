using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace neuroApp.Analyzes.ClinicalLaboratoryData
{
    public class Immunogram:INotifyPropertyChanged
    {
        public int Id { get; set; }


        private double _CD4;
        public double CD4
        {
            get { return _CD4; }
            set
            {
                if (value != null || value != _CD4) _CD4 = value;
                OnPropertyChanged("CD4");
            }
        }

        private double _CD8;
        public double CD8
        {
            get { return _CD8; }
            set
            {
                if (value != null || value != _CD8) _CD8 = value;
                OnPropertyChanged("CD8");
            }
        }

        private double _ViralLoad;
        public double ViralLoad
        {
            get { return _ViralLoad; }
            set
            {
                if (value != null || value != _ViralLoad) _ViralLoad = value;
                OnPropertyChanged("ViralLoad");
            }
        }

        private string _AnalyzeDate;

        public event PropertyChangedEventHandler PropertyChanged;

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
        public int Patient_id { get; set; }

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
