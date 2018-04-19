using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuroApp.Analyzes.Parameters
{
    public class MedicalParameter
    {
        protected double baseTreshold;
        protected decimal weight;
        protected double value;
        public MedicalParameter(double baseTreshold, decimal weight, double value)
        {
            this.baseTreshold = baseTreshold;
            this.weight = weight;
            this.value = value;
        }
        public double CalculatedTreshold { get; set; }
        public double Value
        {
            get { return value; }
        }
        public virtual bool IsTrippedStandartTreshold
        {
            get { return Value > baseTreshold; }
        }
        public virtual bool IsTrippedCalculatedTreshold
        {
            get { return Value > CalculatedTreshold; }
        }
    }
}
