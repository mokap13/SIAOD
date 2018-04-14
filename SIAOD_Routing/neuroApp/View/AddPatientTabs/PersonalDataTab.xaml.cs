using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class PersonalDataTab : UserControl, INotifyPropertyChanged
    {
        TextBoxValid validText = Validator.TextValidationTextBox;
        TextBoxValid validNumber = Validator.NumberValidationTextBox;

        private string _PatientName;
        public string PatientName
        {
            get { return _PatientName; }
            set
            {
                if (value != null || value != _PatientName) _PatientName = value;
                OnPropertyChanged("Name");
            }
        }

        private string _Family;
        public string Family
        {
            get { return _Family; }
            set
            {
                if (value != null || value != _Family) _Family = value;
                OnPropertyChanged("Family");
            }
        }

        private string _Otchestvo;
        public string Otchestvo
        {
            get { return _Otchestvo; }
            set
            {
                if (value != null || value != _Otchestvo) _Otchestvo = value;
                OnPropertyChanged("Otchestvo");
            }
        }
        private string _Birthday;
        public string Birthday
        {
            get { return _Birthday; }
            set
            {
                if (value != null || value != _Birthday) _Birthday = value;
                OnPropertyChanged("Birthday");
            }
        }

        private string _CriminalArticle;
        public string CriminalArticle
        {
            get { return _CriminalArticle; }
            set
            {
                if (value != null || value != _CriminalArticle) _CriminalArticle = value;
                OnPropertyChanged("CriminalArticle");
            }
        }

        private string _BeginDate;
        public string BeginDate
        {
            get { return _BeginDate; }
            set
            {
                if (value != null || value != _BeginDate) _BeginDate = value;
                OnPropertyChanged("BeginDate");
            }
        }

        private string _EndDate;
        public string EndDate
        {
            get { return _EndDate; }
            set
            {
                if (value != null || value != _EndDate) _EndDate = value;
                OnPropertyChanged("EndDate");
            }
        }

        private string _Address;
        public string Address
        {
            get { return _Address; }
            set
            {
                if (value != null || value != _Address) _Address = value;
                OnPropertyChanged("Address");
            }
        }

        public PersonalDataTab()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void DatePicker_Birthday_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var age = (DateTime.Now.Year - datePicker_Birthday.SelectedDate.Value.Year);
            if (datePicker_Birthday.SelectedDate > DateTime.Now.AddYears(-age))
                age--;
            label_age.Content = age.ToString() + " - возраст";
        }

        private void DatePicker_Birthday_Loaded(object sender, RoutedEventArgs e)
        {
            const int middleAge = 35;
            datePicker_Birthday.SelectedDate = new DateTime(DateTime.Now.Year - middleAge, 1, 1);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
