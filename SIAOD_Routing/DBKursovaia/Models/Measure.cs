using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBKursovaia.Models
{
    public class Measure
    {
        public string IndicatorName { get; set; }
        public DateTime TimeStamp { get; set; }
        public double Value { get; set; }
    }
}
