using neuroApp.Analyzes.Tuberculosis;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    public partial class TuberculosisStatusTab : UserControl
    {
        public ObservableCollection<DrugResistance> DrugResistances { get; set; }
        public ObservableCollection<TuberculosisForm> TuberculosisForms { get; set; }
        public ObservableCollection<TuberculosisStatus> TuberculosisStatuses { get; set; }
        public TuberculosisStatusTab()
        {
            InitializeComponent();
            this.DataContext = this;

            using (ApplicationContext db = new ApplicationContext())
            {
                DrugResistances = new ObservableCollection<DrugResistance>(db
                        .DrugResistances
                        .ToList());
                TuberculosisForms = new ObservableCollection<TuberculosisForm>(db
                    .TuberculosisForms
                    .ToList());
                TuberculosisStatuses = new ObservableCollection<TuberculosisStatus>(db
                    .TuberculosisStatuses
                    .ToList());
            }
        }

        private void ListBox_medicamentResist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> drugResistances = new List<string>();
            foreach (DrugResistance item in (sender as ListBox).SelectedItems)
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
    }
}
