using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBKursovaia.Models
{
    public class CncMachine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string InventaryNumber { get; set; }
        public List<Indicator> Indicators { get; set; }
    }
}
