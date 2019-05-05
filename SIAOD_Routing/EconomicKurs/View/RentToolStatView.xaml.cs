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

namespace EconomicKurs.View
{
    /// <summary>
    /// Логика взаимодействия для RentToolStatView.xaml
    /// </summary>
    public partial class RentToolStatView : Page
    {
        public RentToolStatView()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {

            Frame frame = sender as Frame;

            (frame.Content as RentToolStatView).DataContext = frame.DataContext;

        }
    }
}
