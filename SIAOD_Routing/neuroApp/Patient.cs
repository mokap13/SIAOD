using neuroApp.Analyzes;
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

        private bool priemARVT;
        private int formaTuberkuleza;
        private bool defecitMassTela;
        private bool recediveTuberkulezProcess;
        private bool destructionLegochTkan;
        private int dlitelnVichInfection;
        private int narkomania;
        private bool tahicardia;
        private bool arithmia;
        private bool povishTempTela;
        private bool gepotamegalia;
        private bool splenomegalia;
        private bool limfodenatopia;
        private int virusNagruzka;
        private int CD4;
        private int gemoglobin;
        private int SOE;
        private double leikozit;
        private double limfozit;
        private double trombozit;
        private double ALT;
        private double AST;
        private double obhiBilirubin;
        private double kreatenin;
        private int bacteriaVidel;
        private int paranteralGepatit;
        private bool saharDiabet;
        private bool vichAssotiationZabol;
        private TuberculosisForm tuberculosisForm;

        public Patient()
        {
        }


        public int Id { get; set; }
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
        public int TuberculosisForm_id { get; set; }
        public TuberculosisForm TuberculosisForm
        {
            get { return tuberculosisForm; }
            set
            {
                tuberculosisForm = value;
                OnPropertyChanged("TuberculosisForm");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
