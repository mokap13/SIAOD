using EconomicKurs.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EconomicKurs.ViewModel
{
    public class AddNameVM : ViewModelBase
    {
        public ObservableCollection<string> Names { get; set; }

        private string selectedName;

        public string SelectedName
        {
            get { return selectedName; }
            set { Set(ref selectedName, value); }
        }

        private string newName;

        public string NewName
        {
            get { return newName; }
            set { Set(ref newName, value); }
        }

        public AddNameVM()
        {
            Names = new ObservableCollection<string>();
        }

        private RelayCommand addNameCommand;

        public ICommand AddNameCommand => addNameCommand ?? (addNameCommand = new RelayCommand(() =>
        {
            if (this.Names.Contains(this.NewName))
            {
                MessageBox.Show("Такое имя уже есть", "Ошибка", MessageBoxButton.OK);
                return;
            }
            this.Names.Add(NewName);
        }));

        private RelayCommand removeNameCommand;

        public ICommand RemoveNameCommand => removeNameCommand ?? (removeNameCommand = new RelayCommand(() =>
        {
            this.Names.Remove(SelectedName);
        }));
    }
}
