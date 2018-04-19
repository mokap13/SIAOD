using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuroApp.Analyzes.Parameters
{
    public class InversedTresholdMedicalParameter : MedicalParameter
    {
        public InversedTresholdMedicalParameter(double baseTreshold, decimal weight, double value) 
            : base(baseTreshold, weight, value) { }
        public override bool IsTrippedStandartTreshold
        {
            get { return Value < baseTreshold; }
        }
        public override bool IsTrippedCalculatedTreshold
        {
            get { return Value < CalculatedTreshold; }
        }
    }
}
