using MahApps.Metro.Controls;
using neuroApp.Model;
using System;
using System.Collections;
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
using System.Data.Entity;
using neuroApp.Analyzes.ObjectiveStatus;

namespace neuroApp
{
    class DataGridPatient
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string Otchestvo { get; set; }
        public string Birthday { get; set; }
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
        public string ResearchDate { get; set; }
        public string TuberculosisForm { get; set; }
        public string TuberculosisPhase { get; set; }
        public double Risk { get; set; }
        public int Id { get; set; }
    }
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        ApplicationContext db = new ApplicationContext();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_AddPatient_Click(object sender, RoutedEventArgs e)
        {
            AddPatientWIndow addPatientWindow = new AddPatientWIndow();
            if (addPatientWindow.ShowDialog() == true)
            {
                var patients = db.Patients.ToList();
                RefreshDataGridPatient();
                dataGrid_Patients.Items.Refresh();
            }
        }

        private void DataGrid_Patients_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dataGrid_Patients.SetValue(
                    DataGridUtilities.ColumnHeadersProperty,
                    new Dictionary<string, string> {
                        { "Name", "Имя" },
                        { "Family", "Фамилия" },
                        { "Otchestvo", "Отчество" },
                        { "Birthday", "Дата рождения" },
                        { "BeginDate", "Начало срока" },
                        { "EndDate", "Конец срока" },
                        { "TuberculosisForm", "Форма туберкулеза" },
                        { "TuberculosisPhase", "Фаза туберкулеза" },
                        { "ResearchDate", "Дата исследования" },
                        { "Risk", "Риск" },
                        { "Id", "ID" },
                    });
                RefreshDataGridPatient();

            }

            catch (Exception exception)
            {
                string message = null;
                message = $"\nНе удалось открыть базу данных\n {exception.Message}\n";

                if (exception.InnerException != null)
                    message += $"\n{exception.InnerException.Message}";
                MessageBox.Show(message);
            }
        }

        private void RefreshDataGridPatient()
        {
            dataGrid_Patients.ItemsSource = db.Patients
                                .Select(s => new DataGridPatient()
                                {
                                    Name = s.Name,
                                    Family = s.Family,
                                    Otchestvo = s.Otchestvo,
                                    Birthday = s.Birthday,
                                    BeginDate = s.BeginDate,
                                    EndDate = s.EndDate,
                                    TuberculosisForm = s.TuberculosisForm.Name,
                                    TuberculosisPhase = s.TuberculosisPhase.Name,
                                    Risk = s.Risks.FirstOrDefault().CalculatedRisk,
                                    ResearchDate = s.Risks.FirstOrDefault().AnalyzeDate,
                                    Id = s.Id,
                                })
                                .ToList();
        }

        private void Button_DeletePatient_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid_Patients.SelectedItems.Count != 0)
            {
                AnswerWindow answerWindow = new AnswerWindow("Вы уверены, что хотите удалить");
                if (answerWindow.ShowDialog() == true)
                {
                    foreach (DataGridPatient dataGridPatient in dataGrid_Patients.ItemsSource)
                    {
                        Patient patient = db
                            .Patients
                            .FirstOrDefault(p => p.Id == dataGridPatient.Id);
                        db.Patients.Remove(patient);
                    }
                    db.SaveChanges();
                    var patients = db.Patients.ToList();
                    dataGrid_Patients.ItemsSource = patients;
                    RefreshDataGridPatient();
                }
            }
        }

        private void Button_RefreshDataGrid_Click(object sender, RoutedEventArgs e)
        {
            var patients = db.Patients.ToList();
            dataGrid_Patients.ItemsSource = patients;
            dataGrid_Patients.Items.Refresh();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            foreach (Window window in App.Current.Windows)
            {
                window.Close();
            }
        }

        private void Button_ChangePatient_Click(object sender, RoutedEventArgs e)
        {
            ChangePatientWindow changePatientWindow = new ChangePatientWindow();
            if (changePatientWindow.ShowDialog() == true)
            {

            }
        }
    }
}
