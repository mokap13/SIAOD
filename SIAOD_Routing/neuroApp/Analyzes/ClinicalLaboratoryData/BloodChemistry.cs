using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuroApp.Analyzes.ClinicalLaboratoryData
{
    public class BloodChemistry
    {
        public int Id { get; set; }
        public int Patient_id { get; set; }
        public double ALT { get; set; }
        public double AST { get; set; }
        public double TotalBilirubin { get; set; }
        public double Creatinine { get; set; }
        public double Glucose { get; set; }
        public string AnalyzeData { get; set; }

        public Patient Patient { get; set; }
    }
}
