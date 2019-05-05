using EconomicKurs.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace EconomicKurs.ViewModel
{
    public class RentToolStatVM : ViewModelBase
    {
        public class ChartModel
        {
            public double Value { get; set; }
            public DateTime DateTime { get; set; }
            public ChartModel(double value, DateTime date)
            {
                this.Value = value;
                this.DateTime = date;
            }
        }

        private DateTime startViewDate;

        public DateTime StartViewDate
        {
            get { return startViewDate; }
            set
            {
                Set(ref startViewDate, value);
                RaisePropertyChanged(nameof(Series));
            }
        }

        private DateTime endViewDate;

        public DateTime EndViewDate
        {
            get { return endViewDate; }
            set
            {
                Set(ref endViewDate, value);
                RaisePropertyChanged(nameof(Series));
            }
        }

        public CartesianMapper<ChartModel> ChartConfig { get; set; }

        public Func<double, string> Formatter { get; set; }

        public RentToolStatVM()
        {
            this.Formatter = (point) => "";// value => new DateTime((long)(value * TimeSpan.FromDays(1).Ticks)).ToString("dd.MM.yy");

            this.ChartConfig = Mappers.Xy<ChartModel>()
                                .X(dayModel => (double)dayModel.DateTime.Ticks / TimeSpan.FromDays(1).Ticks)
                                .Y(dayModel => dayModel.Value);
        }

        private Tool tool;

        public Tool Tool
        {
            get { return tool; }
            set { Set(ref tool, value); }
        }

        private List<Order> orders;

        public List<Order> Orders
        {
            get { return orders.OrderBy(o => o.EndDate).ToList(); }
            set { Set(ref orders, value); }
        }

        private SeriesCollection series;

        public SeriesCollection Series
        {
            get
            {
                ChartValues<ChartModel> chartValues = new ChartValues<ChartModel>();
                if (this.Orders.Count == 0)
                    return new SeriesCollection();
                var rangeOrders = this.Orders.Where(o => o.RentedTools.Contains(this.Tool)).OrderBy(o => o.EndDate)
                    .Where(o => o.StartDate > startViewDate && o.StartDate < endViewDate
                        || o.EndDate > startViewDate && o.EndDate < endViewDate);

                var firstOrderStartDate = rangeOrders.First().StartDate;
                if (firstOrderStartDate > StartViewDate)
                {
                    for (DateTime date = StartViewDate; date < firstOrderStartDate; date = date.AddDays(1))
                    {
                        chartValues.Add(new ChartModel(double.NaN, date));
                    }
                }

                foreach (var order in rangeOrders.Skip(1).Zip(rangeOrders, (n, p) => new { n, p }))
                {
                    for (DateTime date = order.p.StartDate; date.Date < order.p.EndDate; date = date.AddDays(1))
                    {
                        chartValues.Add(new ChartModel(0, date));
                    }

                    for (DateTime date = order.p.EndDate; date < order.n.StartDate; date = date.AddDays(1))
                    {
                        chartValues.Add(new ChartModel(double.NaN, date));
                    }
                }

                for (DateTime date = rangeOrders.Last().StartDate; date.Date < rangeOrders.Last().EndDate; date = date.AddDays(1))
                {
                    chartValues.Add(new ChartModel(0, date));
                }

                var lastOrderEndDate = rangeOrders.Last().EndDate;
                if (lastOrderEndDate < EndViewDate)
                {
                    for (DateTime date = lastOrderEndDate; date < EndViewDate; date = date.AddDays(1))
                    {
                        chartValues.Add(new ChartModel(double.NaN, date));
                    }
                }

                return new SeriesCollection(ChartConfig)
                {
                    new LineSeries
                    {
                        Values = chartValues,
                        StrokeThickness = 10,
                        LineSmoothness = 0,
                        LabelPoint = point => {
                            var datePoint = StartViewDate.AddDays(point.Key);
                            var order = orders.Find(o => datePoint >= o.StartDate && datePoint <= o.EndDate);
                            return $"{order.Tenant}\r\n{order.StartDate.ToString("dddd, dd MMMM yyyy")} - {order.EndDate.ToString("dddd, dd MMMM yyyy")}";
                        },
                        PointGeometrySize = 0
                    }
                };
            }
            set { Set(ref series, value); }
        }

        public void Refresh()
        {
            RaisePropertyChanged(nameof(this.Series));
            RaisePropertyChanged(nameof(this.TotalPrice));
        }

        private RelayCommand updateCommand;

        public ICommand UpdateCommand => updateCommand ?? (updateCommand = new RelayCommand(() =>
        {
            RaisePropertyChanged(nameof(Series));
        }));

        public double TotalPrice => this.orders.Sum(o => this.tool.RentPrice * o.Duration.TotalDays);
        public double TotalDays => this.orders.Sum(o => o.Duration.TotalDays);
    }
}
