using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuroApp.Analyzes.ClinicalLaboratoryData
{
    public class CompleteBloodCount
    {
        public Patient Patients { get; set; }

        public int Id { get; set; }
        public int Patient_id { get; set; }
        public double ESR { get; set; }
        public double Lymphocytes { get; set; }
        public double Platelets { get; set; }
        public double Erythrocytes { get; set; }
        public double Hemoglobin { get; set; }
        public string AnalyzeDate { get; set; }
    }
}
