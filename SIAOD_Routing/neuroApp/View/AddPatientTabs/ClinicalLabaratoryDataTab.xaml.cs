using neuroApp.Analyzes.ClinicalLaboratoryData;
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

namespace neuroApp.View.AddPatientTabs
{
    /// <summary>
    /// Логика взаимодействия для ClinicalLabaratoryData.xaml
    /// </summary>
    public partial class ClinicalLabaratoryDataTab : UserControl
    {
        TextBoxValid validText = Validator.TextValidationTextBox;
        TextBoxValid validNumber = Validator.NumberValidationTextBox;

        public BloodChemistry BloodChemistry { get; set; }
        public CompleteBloodCount CompleteBloodCount { get; set; }
        public Immunogram Immunogram { get; set; }

        public ClinicalLabaratoryDataTab()
        {
            InitializeComponent();
            this.DataContext = this;
            BloodChemestry_AnalyzeDate.SelectedDate = DateTime.Now;
            CompleteBloodCount_AnalyzeDate.SelectedDate = DateTime.Now;
            Immunograms_AnalyzeDate.SelectedDate = DateTime.Now;
        }
    }
}
