using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication1;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGetValidMeasures()
        {
            int a = 30, b = 32, c = 22;
            int availableDifference = 5;

            int actualResult = Program.GetValidMeasures(a, b, c, availableDifference);
            int expectedResult = (a + b) / 2;
            Assert.AreEqual(actualResult, expectedResult);
        }
    }
}
