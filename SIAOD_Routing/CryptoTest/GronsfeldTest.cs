using System;
using Crypto1.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptoTest
{
    [TestClass]
    public class GronsfeldTest
    {
        [TestMethod]
        public void TestCryptChar_Simple()
        {
            Gronsfeld crypt = new Gronsfeld(new int[] { 1 });

            char expected = 'б';
            char actual = crypt.CryptChar('а');

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestCryptChar_OverflowKey()
        {
            Gronsfeld crypt = new Gronsfeld(new int[] { 33 });

            char expected = 'б';
            char actual = crypt.CryptChar('а');

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestCryptChar_OverflowChar()
        {
            Gronsfeld crypt = new Gronsfeld(new int[] { 2 });

            char expected = 'б';
            char actual = crypt.CryptChar('я');

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestCryptText_Simple()
        {
            Gronsfeld crypt = new Gronsfeld(new int[] { 1, 1 });

            string expected = "бв";
            string actual = crypt.CryptText("аб");

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestCryptText_Hard()
        {
            Gronsfeld crypt = new Gronsfeld(new int[] { 33, 0, -1 });

            string expected = "ааа";
            string actual = crypt.CryptText("яаб");

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestDeCryptText_FromRequiremens()
        {
            Gronsfeld crypt = new Gronsfeld(new int[] { 3, 1, 4 });

            string expected = "СОВЕРШЕННОЯСЕКРЕТНО".ToLower();
            string actual = crypt.DecryptText("ФПЖИСЬИОССАХИЛФИУСС");

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestCryptText_FromRequiremens()
        {
            Gronsfeld crypt = new Gronsfeld(new int[] { 3, 1, 4 });

            string expected = "ФПЖИСЬИОССАХИЛФИУСС".ToLower();
            string actual = crypt.CryptText("СОВЕРШЕННОЯСЕКРЕТНО");

            Assert.AreEqual(expected, actual);
        }
    }
}
