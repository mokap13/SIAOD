using neuroApp.Analyzes.HIVAssociateDisease;
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
    /// Логика взаимодействия для HIVAssociateDisease.xaml
    /// </summary>
    public partial class HIVAssociateDiseaseTab : UserControl
    {
        public ObservableCollection<CheckedListItem<HIVAssociateDisease>> HIVAssociateDiseases{ get; set; }

        public HIVAssociateDiseaseTab()
        {
            InitializeComponent();
            this.DataContext = this;
            using (ApplicationContext db = new ApplicationContext())
            {
                var test = db.HIVAssociateDiseases.Join(db.HIVAssociateDiseaseGroups,
                    hiv => hiv.HIVAssociateDiseaseGroupId,
                    g => g.Id,
                    (hiv, g) => new
                    {
                        hiv.Id,
                        hiv.Name,
                        hiv.HIVAssociateDiseaseGroupId,
                        hiv.Patients,
                        g
                    })
                    .ToList();
                //var queryExecute = new ObservableCollection<HIVAssociateDisease>(db
                //    .HIVAssociateDiseases
                //    .ToList());
                //HIVAssociateDiseases = new ObservableCollection<CheckedListItem<HIVAssociateDisease>>(queryExecute
                //    .Select(s => new CheckedListItem<HIVAssociateDisease>(s, false))
                //    .ToList());
                var queryExecute = new ObservableCollection<HIVAssociateDisease>(db
                    .HIVAssociateDiseases
                    .ToList());
                HIVAssociateDiseases = new ObservableCollection<CheckedListItem<HIVAssociateDisease>>(test
                    .Select(s => new CheckedListItem<HIVAssociateDisease>(new HIVAssociateDisease() {
                        HIVAssociateDiseaseGroup = s.g,
                        HIVAssociateDiseaseGroupId = s.Id,
                        Id = s.Id,
                        Patients = s.Patients,
                        Name = s.Name
                    }, false))
                    .ToList());
            }
        }
    }
}
