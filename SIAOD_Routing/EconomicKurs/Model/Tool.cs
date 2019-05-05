using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconomicKurs.Model
{
    public class Tool
    {
        public string Name { get; set; }
        public int InventoryNumber { get; set; }
        public double SourcePrice { get; set; }
        public double RentPrice { get; set; }
        public DateTime PurshaceDate { get; set; }
        public TimeSpan UsingDuration { get; set; }
        public bool IsRented { get; set; }
    }
}
