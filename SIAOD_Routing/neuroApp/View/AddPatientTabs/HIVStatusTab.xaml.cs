using neuroApp.Analyzes.HIV;
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
using System.Data.Entity;

namespace neuroApp.View.AddPatientTabs
{
    /// <summary>
    /// Логика взаимодействия для HIVStatus.xaml
    /// </summary>
    public partial class HIVStatusTab : UserControl, INotifyPropertyChanged
    {
        TextBoxValid validText = Validator.TextValidationTextBox;
        TextBoxValid validNumber = Validator.NumberValidationTextBox;

        private int _HIVInfectionDuration;
        public int HIVInfectionDuration
        {
            get { return _HIVInfectionDuration; }
            set
            {
                if (value != _HIVInfectionDuration) _HIVInfectionDuration = value;
                OnPropertyChanged("HIVInfectionDuration");
            }
        }

        public ObservableCollection<HIVStage> HIVStages { get; set; }
        private HIVStage _HIVStage;
        public HIVStage HIVStage
        {
            get { return _HIVStage; }
            set
            {
                if (value != null || value != _HIVStage) _HIVStage = value;
                OnPropertyChanged("HIVStage");
            }
        }

        public ObservableCollection<HIVPhase> HIVPhases { get; set; }

        private HIVPhase _HIVPhase;
        public HIVPhase HIVPhase
        {
            get { return _HIVPhase; }
            set
            {
                if (value != null || value != _HIVPhase) _HIVPhase = value;
                OnPropertyChanged("HIVPhase");
            }
        }
        public ObservableCollection<CheckedListItem<HIVStatus>> HIVStatuses { get; set; }

        public HIVStatusTab()
        {
            InitializeComponent();
            this.DataContext = this;

            using (ApplicationContext db = new ApplicationContext())
            {
                HIVPhases = new ObservableCollection<HIVPhase>(db
                    .HIVPhases
                    .ToList());
                HIVStages = new ObservableCollection<HIVStage>(db
                    .HIVStages
                    .ToList());


                var queryHIVStatuses = new ObservableCollection<HIVStatus>(db
                    .HIVStatuses
                    .ToList());
                HIVStatuses = new ObservableCollection<CheckedListItem<HIVStatus>>(queryHIVStatuses
                    .Select(s => new CheckedListItem<HIVStatus>(s, false))
                    .ToList());
            }
            HIVPhase = HIVPhases.First();
            HIVStage = HIVStages.First();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
