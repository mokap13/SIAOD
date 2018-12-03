using DBKursovaia.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight;
using System.Threading;
using System.Windows.Threading;

namespace DBKursovaia.ViewModels
{
    public class MainVM : ViewModelBase
    {
        public MainVM()
        {
            this.Servers = new ObservableCollection<Server>();
        }
        private readonly Dispatcher dispatcher = Dispatcher.CurrentDispatcher;
        private const string connectionString = "Host=localhost;Username=postgres;Password=freeman123;Database=demo";
        private const string getAllServers = @"select s.host, s.host_num, d.manufactory, d.sector
                                                from Server as s join 
                                                department as d on s.department_id = d.id;";
        private NpgsqlConnection psql;
        private string ipAddress;
        public string IpAddress
        {
            get { return ipAddress; }
            set
            {

                Set(ref ipAddress, value);
                connectServersCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<Server> Servers { get; }
        private int selectedIndexCollectionBytes;

        public int SelectedIndexCollectionBytes
        {
            get { return selectedIndexCollectionBytes; }
            set { Set(ref this.selectedIndexCollectionBytes, value); }
        }
        private int selectedIndexByte;

        public int SelectedIndexByte
        {
            get { return selectedIndexByte; }
            set { Set(ref this.selectedIndexByte, value); }
        }
        private bool isBusy;

        public bool IsBusy
        {
            get { return isBusy; }
            set { Set(ref isBusy, value); }
        }
        private RelayCommand readServersCommand;
        public ICommand ReadServersCommand
        {
            get
            {
                return readServersCommand ?? (readServersCommand = new RelayCommand(() =>
                 {
                     Task.Factory.StartNew(() =>
                     {
                         this.IsBusy = true;
                         dispatcher.Invoke(() => { this.Servers.Clear(); });

                         if (psql == null)
                             psql = new NpgsqlConnection(connectionString);

                         lock (psql)
                         {
                             psql.Open();

                             using (NpgsqlCommand command = new NpgsqlCommand(getAllServers, psql))
                             {
                                 NpgsqlDataReader dbData = command.ExecuteReader();

                                 dispatcher.Invoke(() =>
                                 {
                                     while (dbData.Read())
                                     {
                                         Servers.Add(new Server
                                         {
                                             Manufactory = dbData[3].ToString(),
                                             Sector = dbData[2].ToString(),
                                             HostNum = dbData[1].ToString(),
                                             Host = dbData[0].ToString()
                                         });
                                     }
                                 });
                             };

                             psql.Close();
                             this.IsBusy = false;
                         }
                     });
                 }));
            }
        }
        private RelayCommand<string> connectServersCommand;
        public ICommand ConnectServersCommand
        {
            get
            {
                return connectServersCommand ?? (connectServersCommand = new RelayCommand<string>((ipAddress) =>
                {
                    this.IpAddress = "123";
                }, (ipAddress) =>
                {
                    if (string.IsNullOrEmpty(ipAddress))
                        return false;
                    return true;
                }));
            }
        }
    }
}
