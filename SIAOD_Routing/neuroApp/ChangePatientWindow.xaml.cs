﻿using System;
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
using System.Windows.Shapes;

namespace neuroApp
{
    /// <summary>
    /// Логика взаимодействия для ChangePatientWindow.xaml
    /// </summary>
    public partial class ChangePatientWindow : Window
    {
        public ChangePatientWindow()
        {
            InitializeComponent();
        }

        private void Button_ChangePatient_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
