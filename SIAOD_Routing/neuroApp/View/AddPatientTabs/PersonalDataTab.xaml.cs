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
    public delegate void TextBoxValid(object sender, TextCompositionEventArgs e);
    
    /// <summary>
    /// Логика взаимодействия для PersonalData.xaml
    /// </summary>
    public partial class PersonalDataTab : UserControl
    {
        TextBoxValid validText = Validator.TextValidationTextBox;
        TextBoxValid validNumber = Validator.NumberValidationTextBox;

        public PersonalDataTab()
        {
            InitializeComponent();
        }

        private void DatePicker_Birthday_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Button_addPatient_CheckIsEnabled();
            var age = (DateTime.Now.Year - datePicker_Birthday.SelectedDate.Value.Year);
            if (datePicker_Birthday.SelectedDate > DateTime.Now.AddYears(-age))
                age--;
            label_age.Content = age.ToString() + " - возраст";
        }

        private void Button_addPatient_CheckIsEnabled()
        {
            //if ((textBox_Family.Text != String.Empty)
            //    && (textBox_Name.Text != String.Empty)
            //    && (textBox_Otchestvo.Text != String.Empty)
            //    && (datePicker_Birthday.SelectedDate != null))
            //{
            //    button_addPatient.IsEnabled = true;
            //}
        }

        private void DatePicker_Birthday_Loaded(object sender, RoutedEventArgs e)
        {
            const int middleAge = 35;
            datePicker_Birthday.SelectedDate = new DateTime(DateTime.Now.Year - middleAge, 1, 1);
        }
    }
}
