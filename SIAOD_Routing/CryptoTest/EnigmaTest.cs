using System;
using Crypto1.Models.Enigma;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CryptoTest
{
    [TestClass]
    public class EnigmaTest
    {
        [TestMethod]
        public void TestCryptChar()
        {
            Enigma enigma = new Enigma();
            char expected = 'N';
            char actual = enigma.CryptChar('A');

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestCryptText_Alphabet()
        {
            Enigma enigma = new Enigma();
            string testString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            string expected = "NFXUHBJERGOPWAKLSIQVDTMCZY";
            
            string actual = new string(testString.Select(c => enigma.CryptChar(c)).ToArray());

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestCryptText_AnyRotorsPositions()
        {
            Enigma enigma = new Enigma('R','Z','J');
            string testString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            string expected = "NVZMQPTIHKJRDASFELOGWBUYXC";

            string actual = new string(testString.Select(c => enigma.CryptChar(c)).ToArray());

            Assert.AreEqual(expected, actual);
        }
    }
}
