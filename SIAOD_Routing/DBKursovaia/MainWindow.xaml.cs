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
using Npgsql;

namespace DBKursovaia
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string connectionString = "Host=localhost;Username=postgres;Password=freeman123;Database=demo";
        private const string getAllServers = "select * from server";

        public ObservableCollection<Server> Servers { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Servers = new ObservableCollection<Server>();
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            NpgsqlConnection psql = new NpgsqlConnection(connectionString);
            psql.Open();

            NpgsqlCommand command = new NpgsqlCommand(getAllServers, psql);

            NpgsqlDataReader dr = command.ExecuteReader();

            Servers.Clear();
            while (dr.Read())
                Servers.Add(new Server
                {
                    Id = (int)dr[0],
                    Manufatory = dr[1].ToString(),
                    Sector = dr[2].ToString(),
                    HostNum = dr[3].ToString(),
                    Host = dr[4].ToString()
                });

            psql.Close();
        }
    }

}
