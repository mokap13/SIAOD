using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBKursovaia.Models
{
    public class Server : ObservableObject, IDataErrorInfo
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { base.Set(ref id, value); }
        }

        private string manufactory;

        public string Manufactory
        {
            get { return manufactory; }
            set { base.Set(ref manufactory, value); }
        }

        private string sector;

        public string Sector
        {
            get { return sector; }
            set { base.Set(ref sector, value); }
        }

        private string hostNum;

        public string HostNum
        {
            get { return hostNum; }
            set { base.Set(ref hostNum, value); }
        }

        private string host;

        public string Host
        {
            get { return host; }
            set { base.Set(ref host, value); }
        }

        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                string errorMsg = String.Empty;
                if (columnName.Equals("Manufactory"))
                {
                    if (String.IsNullOrEmpty(this.Manufactory))
                        errorMsg = "Manufactory Name is a mandatory field";
                    else if (this.Manufactory.Length < 6)
                        errorMsg = "Manufactory Name must contain at least 6 characters";
                }
                //else if (columnName.Equals("EmpSalary"))
                //{
                //    int salary;
                //    if (string.IsNullOrEmpty(this.empSalary))
                //        errorMsg = "Salary is mandatory field";
                //    else if (Int32.TryParse(this.empSalary, out salary))
                //        errorMsg = "Salary must only contain numbers";
                //}
                else
                    errorMsg = String.Empty;

                return errorMsg;
            }
        }
    }
}
