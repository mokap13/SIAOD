using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconomicKurs.Model
{
    class Warehouse
    {
        public List<string> TenantNames { get; private set; }

        public List<string> ToolNames { get; private set; }

        public List<Tool> Tools { get; set; }

        public List<Order> Orders { get; private set; } = new List<Order>();

        private static string TenantJsonPath = @"./TenantNames.json";
        private static string ToolNamesJsonPath = @"./ToolNames.json";
        private static string OrdersJsonPath = @"./Orders.json";
        private static string RentedToolsJsonPath = @"./RentedTools.json";

        public Warehouse()
        {
            this.Tools = new List<Tool>();

            this.LoadContext();
        }

        public void OpenOrder(string tenant, List<Tool> requiredTools, DateTime startDate, int days)
        {
            requiredTools.ForEach(r => r.IsRented = true);
            Orders.Add(new Order { IsOpened = true, RentedTools = requiredTools, Tenant = tenant, StartDate = startDate, Duration = TimeSpan.FromDays(days) });
        }

        public void CloseOrder(Order order)
        {
            order.IsOpened = false;
            order.RentedTools.ForEach(t => t.IsRented = false);
        }

        public void SaveContext()
        {
            //this.Tools.Add(new Tool
            //{
            //    InventoryNumber = "#00123$123",
            //    Name = "Дрель",
            //    RentPrice = 300,
            //});

            //this.Orders = new List<Order>
            //{
            //    new Order
            //    {
            //        Duration = TimeSpan.FromDays(2),
            //        StartDate = DateTime.Parse("2019/04/10"),
            //        RentedTools = new List<Tool> { this.Tools.First() },
            //        Tenant = new Tenant{ Name=this.TenantNames[0]}
            //    },
            //    new Order
            //    {
            //        Duration = TimeSpan.FromDays(3),
            //        StartDate = DateTime.Parse("2019/04/15"),
            //        RentedTools = new List<Tool> { this.Tools.First() },
            //        Tenant = new Tenant{Name=this.TenantNames[1]}
            //    },
            //    new Order
            //    {
            //        Duration = TimeSpan.FromDays(2),
            //        StartDate = DateTime.Parse("2019/04/20"),
            //        RentedTools = new List<Tool> { this.Tools.First() },
            //        Tenant = new Tenant{Name=this.TenantNames[2]}
            //    }
            //};

            File.WriteAllText(TenantJsonPath, JsonConvert.SerializeObject(this.TenantNames));
            File.WriteAllText(ToolNamesJsonPath, JsonConvert.SerializeObject(this.ToolNames));
            File.WriteAllText(OrdersJsonPath, JsonConvert.SerializeObject(this.Orders));
            File.WriteAllText(RentedToolsJsonPath, JsonConvert.SerializeObject(this.Tools));
        }

        public void LoadContext()
        {
            this.TenantNames = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(TenantJsonPath));
            this.ToolNames = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(ToolNamesJsonPath));
            this.Tools = JsonConvert.DeserializeObject<List<Tool>>(File.ReadAllText(RentedToolsJsonPath));
            this.Orders = JsonConvert.DeserializeObject<List<Order>>(File.ReadAllText(OrdersJsonPath));
            
            foreach (var ord in this.Orders)
            {
                var tempTools = new List<Tool>();
                foreach (var rt in ord.RentedTools)
                {
                    tempTools.Add(this.Tools.Find(t => t.InventoryNumber == rt.InventoryNumber));
                }
                ord.RentedTools = tempTools;
            }
        }
    }
}
