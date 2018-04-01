using neuroApp.Analyzes.Complaint;
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
    /// Логика взаимодействия для Complaints.xaml
    /// </summary>
    public partial class ComplaintsTab : UserControl
    {
        public ObservableCollection<Complaint> Complaints { get; set; }

        public ComplaintsTab()
        {
            InitializeComponent();

            using (ApplicationContext db = new ApplicationContext())
            {
                Complaints = new ObservableCollection<Complaint>(db
                    .Complaints
                    .ToList());
            }
        }
    }
}
