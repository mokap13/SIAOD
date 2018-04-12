using neuroApp.Analyzes.ObjectiveStatus;
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
    /// Логика взаимодействия для SomaticStatusSpecials.xaml
    /// </summary>
    public partial class ObjectiveStatusTab : UserControl, INotifyPropertyChanged
    {
        TextBoxValid validText = Validator.TextValidationTextBox;
        TextBoxValid validNumber = Validator.NumberValidationTextBox;


        private HealthState _HealthState;
        public HealthState HealthState
        {
            get { return _HealthState; }
            set
            {
                if (value != null || value != _HealthState) _HealthState = value;
                OnPropertyChanged("HealthState");
            }
        }

        public ObjectiveStatus ObjectiveStatus { get; set; }
        public ObservableCollection<CheckedListItem<ObjectiveStatusDisease>> ObjectiveStatusDiseases { get; set; }
        public ObservableCollection<HealthState> HealthStates { get; set; }
        public ObjectiveStatusTab()
        {
            InitializeComponent();
            this.DataContext = this;
            using (ApplicationContext db = new ApplicationContext())
            {
                var queryObjectiveStatusDisease = new ObservableCollection<ObjectiveStatusDisease>(db
                    .ObjectiveStatusDiseases
                    .ToList());
                ObjectiveStatusDiseases = new ObservableCollection<CheckedListItem<ObjectiveStatusDisease>>(queryObjectiveStatusDisease
                    .Select(s => new CheckedListItem<ObjectiveStatusDisease>(s,false))
                    .ToList());

                HealthStates = new ObservableCollection<HealthState>(db
                    .HealthStates
                    .ToList());
            }
            
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
