using neuroApp.Analyzes.Complaint;
using neuroApp.ListItems;
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
        public ObservableCollection<CheckedListItem<Complaint>> Complaints { get; set; }

        public ComplaintsTab()
        {
            InitializeComponent();
            this.DataContext = this;
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Complaint> a = new List<Complaint>(db
                    .Complaints
                    .ToList());
                Complaints = new ObservableCollection<CheckedListItem<Complaint>>(a
                    .Select(s => new CheckedListItem<Complaint>(s))
                    .ToList());
                //Complaints = new ObservableCollection<CheckedListItem<Complaint>>(db
                //    .Complaints
                //    .Select(c => new CheckedListItem<Complaint>(c, false))
                //    .ToList());
            }
        }
    }
}
