using neuroApp.Analyzes.HIV;
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
    /// Логика взаимодействия для HIVStatus.xaml
    /// </summary>
    public partial class HIVStatusTab : UserControl
    {
        TextBoxValid validText = Validator.TextValidationTextBox;
        TextBoxValid validNumber = Validator.NumberValidationTextBox;

        public ObservableCollection<HIVStage> HIVStages { get; set; }
        public ObservableCollection<HIVPhase> HIVPhases { get; set; }
        public ObservableCollection<HIVStatus> HIVStatuses { get; set; }

        public HIVStatusTab()
        {
            InitializeComponent();
            this.DataContext = this;

            using (ApplicationContext db = new ApplicationContext())
            {
                HIVStages = new ObservableCollection<HIVStage>(db
                    .HIVStages
                    .ToList());
                HIVPhases = new ObservableCollection<HIVPhase>(db
                    .HIVPhases
                    .ToList());
                HIVStatuses = new ObservableCollection<HIVStatus>(db
                    .HIVStatuses
                    .ToList());
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
