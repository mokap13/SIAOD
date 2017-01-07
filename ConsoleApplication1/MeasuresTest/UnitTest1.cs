using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AbstractFactory;

namespace MeasuresTest
{
    [TestClass]
    public class ProgramTest
    {
        [TestMethod]
        public void TestGetMeasures()
        {
            int avalableDifference = 5;
            double[] measures = new[] { 26.0, 24.0, 26.0, 24.0, 35.0 };
            double? expectedResult = 25.0;

            double? actualResult = MeasuresManager.GetValidMeasure(avalableDifference, measures);

            Assert.AreEqual(actualResult, expectedResult);
        }
        [TestMethod]
        public void TestByCountParamsGetMeasures()
        {
            int avalableDifference = 5;
            double[] measures = new[] { 35.0, 26.0, 24.0 };
            double? expectedResult = 25.0;

            double? actualResult = MeasuresManager.GetValidMeasure(avalableDifference, measures);

            Assert.AreEqual(actualResult, expectedResult);

        }
    }
}
