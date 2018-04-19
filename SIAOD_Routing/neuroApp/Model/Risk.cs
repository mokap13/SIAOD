using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuroApp.Model
{
    public class Risk
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public double CalculatedRisk { get; set; }
        private string analyzeDate;

        public string AnalyzeDate
        {
            get { return analyzeDate; }
            set { analyzeDate = value.Split(' ').First(); }
        }
    }
}
