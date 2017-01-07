using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    public static class MeasuresManager
    {
        public static double? GetValidMeasure(int avalableDifference, params double[] measures)
        {
            List<double> valuesA = new List<double>();
            List<double> valuesB = new List<double>();
            valuesA.Add(measures[0]);

            for (int i = 1; i < measures.Length; i++)
            {
                if (Math.Abs(measures[0] - measures[i]) < avalableDifference)
                    valuesA.Add(measures[i]);
                else
                    valuesB.Add(measures[i]);
            }

            return valuesA.Count > valuesB.Count
                ? valuesA.Average()
                : valuesB.Average();
        }

        public static double? GetResultMeasure(double? measuredValue, double? validMeasure,int avalibleDifference)
        {
            int avalibleMeasure = 5;
            if (!measuredValue.HasValue 
                || measuredValue == 0
                || Math.Abs((double)measuredValue - (double)validMeasure) > avalibleMeasure)
                return null;
            return measuredValue;
        }
    }
}
