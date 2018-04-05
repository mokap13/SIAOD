using neuroApp.Analyzes.HIVAssociateDisease;
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
    /// Логика взаимодействия для HIVAssociateDisease.xaml
    /// </summary>
    public partial class HIVAssociateDiseaseTab : UserControl
    {
        public ObservableCollection<HIVAssociateDisease> HIVAssociateDiseases{ get; set; }

        public HIVAssociateDiseaseTab()
        {
            InitializeComponent();
            this.DataContext = this;
            using (ApplicationContext db = new ApplicationContext())
            {
                HIVAssociateDiseases = new ObservableCollection<HIVAssociateDisease>(db
                    .HIVAssociateDiseases
                    .ToList());
            }
        }
    }
}
