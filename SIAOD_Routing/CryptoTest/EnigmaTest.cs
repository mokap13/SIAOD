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
            char expected = 'F';
            char actual = enigma.CryptChar('A');

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestCryptChar_AnyPositions()
        {
            Enigma enigma = new Enigma();
            enigma.SetPositionRotor(0, 'R');
            enigma.SetPositionRotor(1, 'V');
            enigma.SetPositionRotor(2, 'C');
            char expected = 'T';
            char actual = enigma.CryptChar('A');

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestCryptChar_AnyPositionsLight()
        {
            Enigma enigma = new Enigma();
            enigma.SetPositionRotor(0, 'B');
            char expected = 'T';
            char actual = enigma.CryptChar('A');

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestCryptText_Alphabet()
        {
            Enigma enigma = new Enigma();
            string testString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string expected = "FUVEPUMWARVQKEFGHGDIJFMFXI";
            
            string actual = new string(testString.Select(c => enigma.CryptChar(c)).ToArray());

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestCryptText_AlphabetWithAnyPositions()
        {
            Enigma enigma = new Enigma();
            enigma.SetPositionRotor(0, 'J');
            enigma.SetPositionRotor(1, 'Z');
            enigma.SetPositionRotor(2, 'R');
            string testString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string expected = "LOBWTRKUFSICZJCFZKDMRSHWRM";

            string actual = new string(testString.Select(c => enigma.CryptChar(c)).ToArray());

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestCryptString_AnyRotorsPositions()
        {
            Enigma enigma = new Enigma();
            enigma.SetPositionRotor(0, 'J');
            enigma.SetPositionRotor(1, 'Z');
            enigma.SetPositionRotor(2, 'R');
            string testString = "ASDDD";

            string expected = "LHUWC";

            string actual = enigma.CryptString(testString);

            Assert.AreEqual(expected, actual);
        }
    }
}
