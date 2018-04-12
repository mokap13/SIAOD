using neuroApp.Analyzes.Tuberculosis;
using neuroApp.ListItems;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace neuroApp.View.AddPatientTabs
{
    /// <summary>
    /// Логика взаимодействия для TuberculosisStatus.xaml
    /// </summary>
    public partial class TuberculosisStatusTab : UserControl, INotifyPropertyChanged
    {
        public ObservableCollection<CheckedListItem<DrugResistance>> DrugResistances { get; set; }

        public ObservableCollection<TuberculosisForm> TuberculosisForms { get; set; }
        private TuberculosisForm _TuberculosisForm;
        public TuberculosisForm TuberculosisForm
        {
            get { return _TuberculosisForm; }
            set
            {
                if (value != null || value != _TuberculosisForm) _TuberculosisForm = value;
                OnPropertyChanged("TuberculosisForm");
            }
        }


        public ObservableCollection<CheckedListItem<TuberculosisStatus>> TuberculosisStatuses { get; set; }
        public TuberculosisStatusTab()
        {
            InitializeComponent();
            this.DataContext = this;

            using (ApplicationContext db = new ApplicationContext())
            {
                TuberculosisForms = new ObservableCollection<TuberculosisForm>(db
                    .TuberculosisForms
                    .ToList());

                var queryDrugResistances = new ObservableCollection<DrugResistance>(db
                    .DrugResistances
                    .ToList());
                DrugResistances = new ObservableCollection<CheckedListItem<DrugResistance>>(queryDrugResistances
                    .Select(s => new CheckedListItem<DrugResistance>(s, false))
                    .ToList());

                var queryTuberculosisStatuses = new ObservableCollection<TuberculosisStatus>(db
                    .TuberculosisStatuses
                    .ToList());
                TuberculosisStatuses = new ObservableCollection<CheckedListItem<TuberculosisStatus>>(queryTuberculosisStatuses
                    .Select(s => new CheckedListItem<TuberculosisStatus>(s, false))
                    .ToList());
            }
        }

        private void ListBox_medicamentResist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> drugResistances = new List<string>();

            foreach (DrugResistance item in DrugResistances
                .Where(w => w.IsChecked == true)
                .Select(s => s.Item)
                .ToList())
            {
                drugResistances.Add(item.Name);
            }

            if (drugResistances.Count != 0)
            {
                if (drugResistances.Contains("H")
                && (drugResistances.Contains("R")
                || drugResistances.Contains("Rb")))
                {
                    if ((drugResistances.Contains("Mfx")
                        || drugResistances.Contains("Lfx")
                        || drugResistances.Contains("Ofx"))
                        && (drugResistances.Contains("Cm")
                        || drugResistances.Contains("Am")
                        || drugResistances.Contains("Km")))
                    {
                        label_medicamentResist.Content = "ШЛУ";
                    }
                    else
                    {
                        label_medicamentResist.Content = "МЛУ";
                    }
                }
                else
                {
                    label_medicamentResist.Content = "ЛУ";
                }
            }
            else
            {
                label_medicamentResist.Content = "ЛУ отсутствует";
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
