using DBKursovaia.MVVMHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBKursovaia.Models
{
    public class Server : ObservableObject
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        private string manufactory;

        public string Manufactory
        {
            get { return manufactory; }
            set { SetProperty(ref manufactory, value); }
        }

        private string sector;

        public string Sector
        {
            get { return sector; }
            set { SetProperty(ref sector, value); }
        }

        private string hostNum;

        public string HostNum
        {
            get { return hostNum; }
            set { SetProperty(ref hostNum, value); }
        }

        private string host;

        public string Host
        {
            get { return host; }
            set { SetProperty(ref host, value); }
        }

        public IReadOnlyList<string> ColumnHeaderNames { get; }
    }
}
