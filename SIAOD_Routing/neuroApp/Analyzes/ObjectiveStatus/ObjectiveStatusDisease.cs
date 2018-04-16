using neuroApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuroApp.Analyzes.ObjectiveStatus
{
    public class ObjectiveStatusDisease
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Patient> Patients { get; set; }
        public ObjectiveStatusDisease()
        {
            Patients = new List<Patient>();
        }
    }
}
