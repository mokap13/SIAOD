using neuroApp.Analyzes.ObjectiveStatus;
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
    /// Логика взаимодействия для SomaticStatusSpecials.xaml
    /// </summary>
    public partial class ObjectiveStatusTab : UserControl
    {
        TextBoxValid validText = Validator.TextValidationTextBox;
        TextBoxValid validNumber = Validator.NumberValidationTextBox;
        public ObjectiveStatus ObjectiveStatus { get; set; }
        public ObservableCollection<ObjectiveStatusDisease> ObjectiveStatusDiseases { get; set; }
        public ObservableCollection<HealthState> HealthStates { get; set; }
        public ObjectiveStatusTab()
        {
            InitializeComponent();
            this.DataContext = this;
            ObjectiveStatus = new ObjectiveStatus();
            ObjectiveStatusDiseases = new ObservableCollection<ObjectiveStatusDisease>();
            using (ApplicationContext db = new ApplicationContext())
            {
                ObjectiveStatusDiseases = new ObservableCollection<ObjectiveStatusDisease>(db
                    .ObjectiveStatusDiseases
                    .ToList());
                HealthStates = new ObservableCollection<HealthState>(db
                    .HealthStates
                    .ToList());
            }
            
        }
    }
}
