using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuroApp.Analyzes.Tuberculosis
{
    public class TuberculosisForm
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Patient> Patients { get; set; }
        public TuberculosisForm()
        {
            Patients = new List<Patient>();
        }
    }
}
