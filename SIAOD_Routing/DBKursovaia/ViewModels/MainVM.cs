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
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Configurations;
using LiveCharts.Helpers;

namespace DBKursovaia.ViewModels
{

    public class ChartModel
    {
        public DateTime DateTime { get; set; }
        public double Value { get; set; }
        public ChartModel(DateTime date, double value)
        {
            this.DateTime = date;
            this.Value = value;
        }
    }

    public class MainVM : ViewModelBase
    {
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        public MainVM()
        {
            this.CncMachines = new ObservableCollection<CncMachine>();
            this.Departments = new ObservableCollection<Department>();
            this.Sectors = new ObservableCollection<Sector>();
            this.MeasureSeries = new ObservableCollection<SeriesCollection>();
            this.MeasureDayRange = 1;

            this.Formatter = value => new DateTime((long)(value * TimeSpan.FromHours(4).Ticks)).ToString("G");
            
            this.ChartConfig = Mappers.Xy<ChartModel>()
                                .X(dayModel => (double)dayModel.DateTime.Ticks / TimeSpan.FromHours(4).Ticks)
                                .Y(dayModel => dayModel.Value);

            Series = new SeriesCollection(this.ChartConfig)
            {
                new LineSeries
                {
                    Values = new ChartValues<ChartModel> {
                        new ChartModel(DateTime.Now.AddHours(0), 2),
                        new ChartModel(DateTime.Now.AddHours(1), 4),
                        new ChartModel(DateTime.Now.AddHours(2), 8),
                        new ChartModel(DateTime.Now.AddHours(4), 16),
                        new ChartModel(DateTime.Now.AddHours(8), 32),
                        new ChartModel(DateTime.Now.AddHours(9), 64),
                        new ChartModel(DateTime.Now.AddHours(10), 60),
                        new ChartModel(DateTime.Now.AddHours(11), 56),
                        new ChartModel(DateTime.Now.AddHours(14), 52),
                        new ChartModel(DateTime.Now.AddHours(18), 50),
                        new ChartModel(DateTime.Now.AddHours(22), 48),
                    },
                    LabelPoint = point => $"{(point.Instance as ChartModel).Value}",
                    
                }
            };
            //MeasureSeries.Add(Series);
        }
        private readonly Dispatcher dispatcher = Dispatcher.CurrentDispatcher;

        private string ipAddress;
        public string IpAddress
        {
            get { return ipAddress; }
            set
            {
                Set(ref ipAddress, value);
                connectCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<Department> Departments { get; }
        public Department SelectedDepartment { get; set; }
        public ObservableCollection<Sector> Sectors { get; }
        public Sector SelectedSector { get; set; }
        public ObservableCollection<CncMachine> CncMachines { get; }
        public CncMachine SelectedCncMachine { get; set; }
        public ObservableCollection<SeriesCollection> MeasureSeries { get; }

        public SeriesCollection Series { get; set; }

        public CartesianMapper<ChartModel> ChartConfig { get; set; }

        public Func<double, string> Formatter { get; set; }
        //public Func<double, string> Formatter { get; set; }

        private int measureDayRange;
        public int MeasureDayRange
        {
            get { return measureDayRange; }
            set { Set(ref measureDayRange, value); }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set { Set(ref isBusy, value); }
        }

        #region Connection string property

        private string connDb = "Factory";

        public string ConnDb
        {
            get { return connDb; }
            set { Set(ref connDb, value); }
        }

        private string connIp = "localhost";

        public string ConnIp
        {
            get { return connIp; }
            set { Set(ref connIp, value); }
        }

        private string connLogin = "postgres";

        public string ConnLogin
        {
            get { return connLogin; }
            set { Set(ref connLogin, value); }
        }

        private string connPass = "freeman123";

        public string ConnPass
        {
            get { return connPass; }
            set { Set(ref connPass, value); }
        }

        private string ConnectionString => $"Host={this.connIp};Username={this.connLogin};Password={this.connPass};Database={this.connDb};";

        #endregion


        #region Commands

        private RelayCommand readDepartmentsCommand;
        public ICommand ReadDepartmentsCommand
        {
            get
            {
                return readDepartmentsCommand ?? (readDepartmentsCommand = new RelayCommand(() =>
                {
                    this.CncMachines.Clear();
                    this.Sectors.Clear();
                    this.Departments.Clear();
                    this.IsBusy = true;
                    Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            List<Department> departments = Db.Postgres.GetDepartments();
                            dispatcher.Invoke(() =>
                            {
                                departments.ForEach(d => this.Departments.Add(d));
                            });
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message, "Ошибка БД", MessageBoxButton.OK);
                        }
                        finally
                        {
                            this.IsBusy = false;
                        }
                    });
                }, () => Db.Postgres.IsConnected
                ));
            }
        }

        private RelayCommand<Department> readSectorsCommand;
        public ICommand ReadSectorsCommand
        {
            get
            {
                return readSectorsCommand ?? (readSectorsCommand = new RelayCommand<Department>((department) =>
                {
                    foreach (var series in this.MeasureSeries)
                        series.Clear();
                    this.MeasureSeries.Clear();
                    this.CncMachines.Clear();
                    this.Sectors.Clear();
                    this.IsBusy = true;
                    Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            List<Sector> sectors = Db.Postgres.GetSectors(department);
                            dispatcher.Invoke(() => sectors.ForEach(d => this.Sectors.Add(d)));
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message, "Ошибка БД", MessageBoxButton.OK);
                        }
                        finally
                        {
                            this.IsBusy = false;
                        }
                    });
                }, (department) => Db.Postgres.IsConnected
                ));
            }
        }

        private RelayCommand<Sector> readCncMachinesCommand;
        public ICommand ReadCncMachinesCommand
        {
            get
            {
                return readCncMachinesCommand ?? (readCncMachinesCommand = new RelayCommand<Sector>((sector) =>
                {
                    foreach (var series in this.MeasureSeries)
                        series.Clear();
                    this.MeasureSeries.Clear();
                    this.CncMachines.Clear();
                    this.IsBusy = true;
                    Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            List<CncMachine> machines = Db.Postgres.GetCncMachines(sector);
                            dispatcher.Invoke(() => machines.ForEach(d => this.CncMachines.Add(d)));
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message, "Ошибка БД", MessageBoxButton.OK);
                        }
                        finally
                        {
                            this.IsBusy = false;
                        }
                    });
                }, (sector) => Db.Postgres.IsConnected
                ));
            }
        }

        private RelayCommand<CncMachine> readMeasuresCommand;
        public ICommand ReadMeasuresCommand
        {
            get
            {
                return readMeasuresCommand ?? (readMeasuresCommand = new RelayCommand<CncMachine>((machine) =>
                {
                    foreach (var series in this.MeasureSeries)
                        series.Clear();
                    this.MeasureSeries.Clear();

                    this.IsBusy = true;
                    Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            DateTime startDate = DateTime.Now.Subtract(TimeSpan.FromDays(this.MeasureDayRange));
                            List<Measure> measures = Db.Postgres.GetMeasures(machine, startDate);

                            List<SeriesCollection> series = null;
                            dispatcher.Invoke(() =>
                            {
                                series = measures
                                    .OrderBy(m => m.TimeStamp)
                                    .GroupBy(m => m.IndicatorName)
                                    .Select(g => new SeriesCollection(this.ChartConfig)
                                    {
                                        new LineSeries
                                        {
                                            Title = g.Key,
                                            Values = new ChartValues<ChartModel>(g.Select(m => new ChartModel(m.TimeStamp, m.Value))),
                                            LabelPoint = point => $"{(point.Instance as ChartModel).Value}: {(point.Instance as ChartModel).DateTime.ToString()}"
                                        },
                                    })
                                    .ToList();
                                series.ForEach(s => this.MeasureSeries.Add(s));
                            });
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message, "Ошибка БД", MessageBoxButton.OK);
                        }
                        finally
                        {
                            this.IsBusy = false;
                        }
                    });
                }, (sector) => Db.Postgres.IsConnected
                ));
            }
        }

        private RelayCommand<string> connectCommand;
        public ICommand ConnectCommand
        {
            get
            {
                return connectCommand ?? (connectCommand = new RelayCommand<string>((ipAddress) =>
                {
                    this.IsBusy = true;
                    Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            Db.Postgres.Connect(this.ConnectionString);
                            //Thread.Sleep(1000);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message, "Ошибка подключения", MessageBoxButton.OK);
                        }
                        finally
                        {
                            this.IsBusy = false;
                            dispatcher.Invoke(() => this.readDepartmentsCommand.RaiseCanExecuteChanged());
                        }
                    });

                }));
            }
        }

        #endregion
    }
}
