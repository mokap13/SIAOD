using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuroApp.Analyzes.Tuberculosis
{
    public class DrugResistance
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public List<Patient> Patients { get; set; }
        public DrugResistance()
        {   
            Patients = new List<Patient>();
        }
    }
}
