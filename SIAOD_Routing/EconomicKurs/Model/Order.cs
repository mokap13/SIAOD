using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconomicKurs.Model
{
    public class Order:IEquatable<Order>
    {
        public string Tenant { get; set; }
        public List<Tool> RentedTools { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime EndDate => StartDate.Add(Duration);
        public bool IsOpened { get; set; }
        public double TotalPrice => this.RentedTools.Sum(t => t.RentPrice * this.Duration.TotalDays);

        public bool Equals(Order other)
        {
            return this.Tenant == other.Tenant && this.StartDate == other.StartDate && this.TotalPrice == other.TotalPrice;
        }
    }
}
