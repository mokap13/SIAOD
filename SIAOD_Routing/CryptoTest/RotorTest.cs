using System;
using Crypto1.Models.Enigma;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptoTest
{
    [TestClass]
    public class RotorTest
    {
        [TestMethod]
        public void TestSetPosition()
        {
            Rotor<char, char> actual = new Rotor<char, char>
            {
                {'A','X' },
                {'B','Y' },
                {'C','Z' },
                {'D','W' },
            };
            Rotor<char, char> expected = new Rotor<char, char>
            {
                {'C','X' },
                {'D','Y' },
                {'A','Z' },
                {'B','W' },
            };
            actual.SetPosition('C');

            Assert.AreEqual(expected.Forward['A'], actual.Forward['A']);
            //rotor.
        }
    }
}
