using neuroApp.Model;
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

        public ICollection<Patient> Patients{ get; set; }
        public Complaint()
        {
            Patients = new List<Patient>();
        }
    }
}
