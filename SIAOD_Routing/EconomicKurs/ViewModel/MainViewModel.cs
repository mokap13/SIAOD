using EconomicKurs.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace EconomicKurs.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<RentToolStatVM> RentToolStats { get; set; } = new ObservableCollection<RentToolStatVM>();
        //public AddNameVM AddNameToolVM { get; set; } = new AddNameVM();
        //public AddNameVM AddNameTenantVM { get; set; } = new AddNameVM();

        private Warehouse warehouse { get; } = new Warehouse();


        public ObservableCollection<Tool> AvalableTools { get; set; } = new ObservableCollection<Tool>();
        public ObservableCollection<Tool> OrderTools { get; set; } = new ObservableCollection<Tool>();
        public ObservableCollection<Tool> AllTools { get; set; } = new ObservableCollection<Tool>();
        public ObservableCollection<Order> AllOrders { get; set; } = new ObservableCollection<Order>();
        public ObservableCollection<string> Tenants { get; set; } = new ObservableCollection<string>();
        public Order SelectedOrder { get; set; }
        public string TenantName { get; set; }

        public double TotalBenefit => RentToolStats.Sum(rts => rts.TotalPrice);


        private DateTime orderStartDate = DateTime.Now;

        public DateTime OrderStartDate
        {
            get { return orderStartDate; }
            set { Set(ref orderStartDate, value); }
        }

        private DateTime orderEndDate = DateTime.Now.AddDays(1);

        public DateTime OrderEndDate
        {
            get { return orderEndDate; }
            set { Set(ref orderEndDate, value); }
        }


        private Tool selectedOrderTool;

        public Tool SelectedOrderTool
        {
            get { return selectedOrderTool; }
            set { Set(ref selectedOrderTool, value); }
        }

        private Tool selectedWareHouseTool;

        public Tool SelectedWareHouseTool
        {
            get { return selectedWareHouseTool; }
            set { Set(ref selectedWareHouseTool, value); }
        }

        private int orderDays;

        public int OrderDays
        {
            get { return orderDays; }
            set { Set(ref orderDays, value); }
        }

        private DateTime startDateSeries = DateTime.Now - TimeSpan.FromDays(30);
        public DateTime StartDateSeries
        {
            get { return startDateSeries; }
            set
            {
                Set(ref startDateSeries, value);
                RefreshData();
            }
        }

        private DateTime endDateSeries = DateTime.Now;
        public DateTime EndDateSeries
        {
            get { return endDateSeries; }
            set
            {
                Set(ref endDateSeries, value);
                RefreshData();
            }
        }

        private void RefreshData()
        {
            this.AllTools.Clear();
            foreach (var tool in warehouse.Tools)
            {
                this.AllTools.Add(tool);
            }

            this.AvalableTools.Clear();
            this.OrderTools.Clear();
            this.warehouse.Tools.Where(t => !t.IsRented).ToList().ForEach(t => AvalableTools.Add(t));

            this.Tenants.Clear();
            this.warehouse.TenantNames.ForEach(n => this.Tenants.Add(n));

            this.AllOrders.Clear();
            this.warehouse.Orders.ForEach(o => this.AllOrders.Add(o));

            this.RentToolStats.Clear();
            warehouse.Tools.ForEach(t => RentToolStats.Add(new RentToolStatVM()
            {
                Orders = warehouse.Orders.Where(o => o.RentedTools.Any(tt => tt.InventoryNumber == t.InventoryNumber)).ToList(),
                Tool = t,
                StartViewDate = this.StartDateSeries,
                EndViewDate = this.EndDateSeries
            }));

            RaisePropertyChanged(nameof(TotalBenefit));
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            this.RefreshData();
            this.TenantName = this.Tenants.First();
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }

        private RelayCommand saveDataCommand;

        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public RelayCommand SaveDataCommand
        {
            get
            {
                return saveDataCommand
                    ?? (saveDataCommand = new RelayCommand(
                    () =>
                    {
                        warehouse.Tools = this.AllTools.ToList();
                        warehouse.SaveContext();
                    }));
            }
        }

        private RelayCommand cancelDataCommand;

        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public RelayCommand CancelDataCommand
        {
            get
            {
                return cancelDataCommand
                    ?? (cancelDataCommand = new RelayCommand(
                    () =>
                    {
                        RefreshData();
                        warehouse.LoadContext();
                    }));
            }
        }

        private RelayCommand moveToolToWareHouseCommand;

        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public RelayCommand MoveToolToWareHouseCommand
        {
            get
            {
                return moveToolToWareHouseCommand
                    ?? (moveToolToWareHouseCommand = new RelayCommand(
                    () =>
                    {
                        if (SelectedOrderTool != null)
                        {
                            this.AvalableTools.Add(this.SelectedOrderTool);
                            this.OrderTools.Remove(this.SelectedOrderTool);
                        }
                    }));
            }
        }

        private RelayCommand moveToolToOrderCommand;

        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public RelayCommand MoveToolToOrderCommand
        {
            get
            {
                return moveToolToOrderCommand
                    ?? (moveToolToOrderCommand = new RelayCommand(
                    () =>
                    {
                        if (SelectedWareHouseTool != null)
                        {
                            this.OrderTools.Add(this.SelectedWareHouseTool);
                            this.AvalableTools.Remove(this.SelectedWareHouseTool);
                        }
                    }));
            }
        }

        private RelayCommand resetOrderCommand;

        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public RelayCommand ResetOrderCommand
        {
            get
            {
                return resetOrderCommand
                    ?? (resetOrderCommand = new RelayCommand(
                    () =>
                    {
                        this.OrderTools.Clear();
                        this.AvalableTools.Clear();
                        this.warehouse.Tools.Where(t => !t.IsRented).ToList().ForEach(t => AvalableTools.Add(t));
                    }));
            }
        }

        private RelayCommand createOrderCommand;

        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public RelayCommand CreateOrderCommand
        {
            get
            {
                return createOrderCommand
                    ?? (createOrderCommand = new RelayCommand(
                    () =>
                    {
                        if (string.IsNullOrEmpty(this.TenantName))
                        {
                            MessageBox.Show("Нужно указать арендатора", "Ошибка создания заказа", MessageBoxButton.OK);
                            return;
                        }
                        if (this.OrderTools.Count == 0)
                        {
                            MessageBox.Show("Нужно указать оборудование", "Ошибка создания заказа", MessageBoxButton.OK);
                            return;
                        }
                        if (!warehouse.TenantNames.Contains(this.TenantName))
                        {
                            warehouse.TenantNames.Add(this.TenantName);
                        }
                        warehouse.OpenOrder(
                            this.TenantName, 
                            this.OrderTools.ToList(), 
                            this.OrderStartDate, 
                            (int)this.OrderEndDate.Subtract(this.OrderStartDate).TotalDays);
                        warehouse.SaveContext();
                        RefreshData();
                    }));
            }
        }

        private RelayCommand closeOrderCommand;

        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public RelayCommand CloseOrderCommand
        {
            get
            {
                return closeOrderCommand
                    ?? (closeOrderCommand = new RelayCommand(
                    () =>
                    {
                        warehouse.CloseOrder(warehouse.Orders.Find(o => o.Equals(SelectedOrder)));
                        warehouse.SaveContext();
                        RefreshData();
                    }));
            }
        }
        private RelayCommand deleteOrderCommand;

        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public RelayCommand DeleteOrderCommand
        {
            get
            {
                return deleteOrderCommand
                    ?? (deleteOrderCommand = new RelayCommand(
                    () =>
                    {
                        Order ord = warehouse.Orders.Find(o => o.Equals(SelectedOrder));
                        warehouse.CloseOrder(ord);
                        warehouse.Orders.Remove(ord);
                        warehouse.SaveContext();
                        RefreshData();
                    }));
            }
        }
    }
}