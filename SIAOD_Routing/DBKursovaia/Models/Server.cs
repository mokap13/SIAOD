using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBKursovaia.Models
{
    public class Server
    {
        public int Id { get; set; }
        public string Manufatory { get; set; }
        public string Sector { get; set; }
        public string HostNum { get; set; }
        public string Host { get; set; }
    }
}
