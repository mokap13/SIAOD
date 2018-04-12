using neuroApp.Analyzes.AccompanyingIllness;
using neuroApp.ListItems;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace neuroApp.View.AddPatientTabs
{
    /// <summary>
    /// Логика взаимодействия для AccompanyingIllnesses.xaml
    /// </summary>
    public partial class AccompanyingIllnessesTab : UserControl
    {
        public ObservableCollection<CheckedListItem<AccompanyingIllness>> AccompanyingIllnesses { get; set; }
        public AccompanyingIllnessesTab()
        {
            InitializeComponent();
            this.DataContext = this;
            using (ApplicationContext db = new ApplicationContext())
            {
                var queryAccompanyingIllnesses = new ObservableCollection<AccompanyingIllness>(db
                    .AccompanyingIllnesses
                    .ToList());
                AccompanyingIllnesses = new ObservableCollection<CheckedListItem<AccompanyingIllness>>(queryAccompanyingIllnesses
                    .Select(s => new CheckedListItem<AccompanyingIllness>(s, false))
                    .ToList());
            }
        }
    }
}
