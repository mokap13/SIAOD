using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuroApp.Analyzes.HIVAssociateDisease
{
    public class HIVAssociateDisease
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Patient> Patients { get; set; }
        public HIVAssociateDiseaseGroup HIVAssociateDiseaseGroup { get; set; }
    }
}
