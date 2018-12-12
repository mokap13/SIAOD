using System.Collections.Generic;

namespace DBKursovaia.Models
{
    public class Device
    {
        public Device(string name)
        {
            this.Name = name;
        }
        public string Name { get; set; }
        public List<Indicator> Indicators { get; set; }
    }
}