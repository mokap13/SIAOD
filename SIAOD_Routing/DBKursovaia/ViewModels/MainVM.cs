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
            RichByte[] bytes = new RichByte[] { 34, 2, 3, 4, 46, 6, 126, 8, 9, 0 };
            this.Bytes = new ObservableCollection<ObservableCollection<RichByte>>()
            {
                new ObservableCollection<RichByte>((RichByte[])bytes.Clone()),
                new ObservableCollection<RichByte>((RichByte[])bytes.Clone()),
                new ObservableCollection<RichByte>((RichByte[])bytes.Clone()),
                new ObservableCollection<RichByte>((RichByte[])bytes.Clone())
            };
        }

        private const string connectionString = "Host=localhost;Username=postgres;Password=freeman123;Database=demo";
        //private const string getAllServersOld = "select * from server";
        private const string getAllServers = @"select s.host, s.host_num, d.manufactory, d.sector
                                                from Server as s join 
                                                department as d on s.department_id = d.id;";
        public ObservableCollection<ObservableCollection<RichByte>> Bytes { get; }
        public ObservableCollection<Server> Servers { get; }

        private RichByte selectedRichByte;

        public RichByte SelectedRichByte
        {
            get { return selectedRichByte; }
            set { SetProperty(ref this.selectedRichByte, value); }
        }

        private int selectedIndexCollectionBytes;

        public int SelectedIndexCollectionBytes
        {
            get { return selectedIndexCollectionBytes; }
            set { SetProperty(ref this.selectedIndexCollectionBytes, value); }
        }
        private int selectedIndexByte;

        public int SelectedIndexByte
        {
            get { return selectedIndexByte; }
            set { SetProperty(ref this.selectedIndexByte, value); }
        }



        public ICommand ReadServersCommand
        {
            get
            {
                return new DelegateCommand(() =>
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
                });
            }
        }
        public ICommand ReadBytesCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Random rand = new Random();
                    ObservableCollection<RichByte[]> newBuf = new ObservableCollection<RichByte[]>();
                    for (int i = 0; i < 50; i++)
                    {
                        byte[] bytes = new byte[10];
                        rand.NextBytes(bytes);
                        newBuf.Add(bytes.Select(b => new RichByte() { Value = b }).ToArray());
                    }
                    Bytes[0][0] = 4;
                    Bytes[0][0].IsSuspected = true;
                    Bytes[1][1] = 4;
                    Bytes[1][1].IsSuspected = true;
                });
            }
        }
        public ICommand MouseEnterToRichByteCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    foreach (var item in this.Bytes)
                    {
                        //item.First()
                    }
                });
            }
        }
    }
}
