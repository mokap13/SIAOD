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
using DBKursovaia.Models;
using DBKursovaia.ViewModels;
using MahApps.Metro.Controls;
using Npgsql;

namespace DBKursovaia
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private MainVM vm;
        public MainWindow()
        {
            InitializeComponent();
            this.vm = (MainVM)this.DataContext;
        }

        private void ListBox_Department_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (vm.SelectedDepartment != null)
                vm.ReadSectorsCommand.Execute(vm.SelectedDepartment);
        }

        private void ListBox_Sector_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (vm.SelectedSector != null)
                vm.ReadCncMachinesCommand.Execute(vm.SelectedSector);
        }

        private void ListBox_CncMachine_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (vm.SelectedCncMachine != null)
                vm.ReadMeasuresCommand.Execute(vm.SelectedCncMachine);
        }

        private void Button_UpdateCharts(object sender, RoutedEventArgs e)
        {
            if (vm.SelectedCncMachine != null)
                vm.ReadMeasuresCommand.Execute(vm.SelectedCncMachine);
        }
    }
}
