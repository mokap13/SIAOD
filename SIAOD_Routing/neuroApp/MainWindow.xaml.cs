using System;
using System.Collections.Generic;
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

namespace neuroApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationContext db;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddPatientWIndow addPatientWindow = new AddPatientWIndow();
            addPatientWindow.Show();
        }

        private void dataGrid_Patients_Loaded(object sender, RoutedEventArgs e)
        {
            db = new ApplicationContext();

            var patients = db.Patients.ToList();
            dataGrid_Patients.ItemsSource = patients;
        }
    }
}
