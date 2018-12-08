using System;
using System.Collections.Generic;
using Crypto1.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptoTest
{
    [TestClass]
    public class ListExtensionTest
    {
        [TestMethod]
        public void TestMoverange()
        {
            List<int> ints = new List<int> { 1, 2, 3, 4, 5, 6 };

            List<int> expected = new List<int> { 1, 5, 6, 2, 3, 4 };
            List<int> actual = ints.MoveRange(5, 1);

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
