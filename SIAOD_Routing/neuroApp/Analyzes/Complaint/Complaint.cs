using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuroApp.Analyzes.Complaint
{
    public class Complaint
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Patient> Patients{ get; set; }
    }
}
