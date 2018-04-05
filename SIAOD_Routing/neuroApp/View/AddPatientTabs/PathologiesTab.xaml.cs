using neuroApp.Analyzes.AccompanyingIllness;
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
    /// Логика взаимодействия для Pathologies.xaml
    /// </summary>
    public partial class PathologiesTab : UserControl
    {
        public ObservableCollection<AccompanyingIllness> AccompanyingIllnesses { get; set; }
        public PathologiesTab()
        {
            InitializeComponent();
            this.DataContext = this;
            using (ApplicationContext db = new ApplicationContext())
            {
                AccompanyingIllnesses = new ObservableCollection<AccompanyingIllness>(db
                    .AccompanyingIllnesses
                    .ToList());
            }
        }
    }
}
