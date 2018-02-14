using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuroApp.Analyzes.ClinicalLaboratoryData
{
    public class Immunogram
    {
        public int Id { get; set; }
        public double CD4 { get; set; }
        public double CD8 { get; set; }
        public double ViralLoad { get; set; }
        public string AnalyzeDate { get; set; }

        public ICollection<Patient> Patients { get; set; }
        public Immunogram()
        {
            Patients = new List<Patient>();
        }
    }
}
