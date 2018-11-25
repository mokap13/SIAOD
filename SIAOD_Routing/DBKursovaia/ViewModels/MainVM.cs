using DBKursovaia.MVVMHelpers;
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

namespace DBKursovaia.ViewModels
{
    public class MainVM : ObservableObject
    {
        public MainVM()
        {
            this.Servers = new ObservableCollection<Server>();
            this.Bytes = new ObservableCollection<RichByte[]>();
            RichByte[] bytes = new RichByte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            this.Bytes_2 = new ObservableCollection<ObservableCollection<RichByte>>()
            {
                new ObservableCollection<RichByte>((RichByte[])bytes.Clone()),
                new ObservableCollection<RichByte>((RichByte[])bytes.Clone()),
                new ObservableCollection<RichByte>((RichByte[])bytes.Clone()),
                new ObservableCollection<RichByte>((RichByte[])bytes.Clone())
            };
            Bytes_2[0][0].IsSuspected = true;
            Bytes_2[1][0].IsSuspected = false;
            Bytes_2[2][0].IsSuspected = true;
        }

        private const string connectionString = "Host=localhost;Username=postgres;Password=freeman123;Database=demo";
        //private const string getAllServersOld = "select * from server";
        private const string getAllServers = @"select s.host, s.host_num, d.manufactory, d.sector
                                                from Server as s join 
                                                department as d on s.department_id = d.id;";
        public ObservableCollection<ObservableCollection<RichByte>> Bytes_2 { get; }
        public ObservableCollection<Server> Servers { get; }
        private ObservableCollection<RichByte[]> bytes;
        public ObservableCollection<RichByte[]> Bytes
        {
            get { return this.bytes; }
            set { SetProperty(ref bytes, value); }
        }

        public ICommand ReadServersCommand
        {
            get { return new DelegateCommand(ReadServers); }
        }
        public ICommand ReadBytesCommand
        {
            get { return new DelegateCommand(ReadBytes); }
        }

        private void ReadBytes()
        {
            Random rand = new Random();
            ObservableCollection<RichByte[]> newBuf = new ObservableCollection<RichByte[]>();
            for (int i = 0; i < 50; i++)
            {
                byte[] bytes = new byte[10];
                rand.NextBytes(bytes);
                newBuf.Add(bytes.Select(b => new RichByte() { Value = b }).ToArray());
            }
            this.Bytes = newBuf;
            Bytes[0][0].Value = 111;
            Bytes[1][1].Value = 111;
            Bytes[2][2].Value = 111;
            Bytes[3][3].IsSelected = true;
            Bytes[4][4].IsSuspected = true;
            Bytes_2[0][0] = 4;
            
            //Bytes_2[0][0] = 
        }

        private void ReadServers()
        {
            NpgsqlConnection psql = new NpgsqlConnection(connectionString);
            psql.Open();

            NpgsqlCommand command = new NpgsqlCommand(getAllServers, psql);

            NpgsqlDataReader dr = command.ExecuteReader();

            Servers.Clear();
            while (dr.Read())
                Servers.Add(new Server
                {
                    Manufactory = dr[3].ToString(),
                    Sector = dr[2].ToString(),
                    HostNum = dr[1].ToString(),
                    Host = dr[0].ToString()
                });

            psql.Close();
        }
    }
}
