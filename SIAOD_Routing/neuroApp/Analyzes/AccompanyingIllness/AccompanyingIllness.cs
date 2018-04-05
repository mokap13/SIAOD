using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuroApp.Analyzes.AccompanyingIllness
{
    public class AccompanyingIllness
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Patient> Patients { get; set; }
        public AccompanyingIllness()
        {
            Patients = new List<Patient>();
        }
    }
}
