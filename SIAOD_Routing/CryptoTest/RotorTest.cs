using System;
using System.Collections.Generic;
using Crypto1.Models.Enigma;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptoTest
{
    [TestClass]
    public class RotorTest
    {
        private readonly Dictionary<char, char> rotorDictionary = new Dictionary<char, char>()
        {
            {'A', 'E'}, {'B', 'K'}, {'C', 'M'}, {'D', 'F'}, {'E', 'L'},
            {'F', 'G'}, {'G', 'D'}, {'H', 'Q'}, {'I', 'V'}, {'J', 'Z'},
            {'K', 'N'}, {'L', 'T'}, {'M', 'O'}, {'N', 'W'}, {'O', 'Y'},
            {'P', 'H'}, {'Q', 'X'}, {'R', 'U'}, {'S', 'S'}, {'T', 'P'},
            {'U', 'A'}, {'V', 'I'}, {'W', 'B'}, {'X', 'R'}, {'Y', 'C'}, {'Z', 'J'},
        };
        [TestMethod]
        public void TestSetPosition()
        {
            Rotor2 rotor = new Rotor2(rotorDictionary);

            Assert.AreEqual('E', rotor.SymbolPairs['A']);

            rotor.SetPosition('R');
            Assert.AreEqual('U', rotor.CryptForward(rotor.Sum('A',rotor.Position)));
            rotor.SetPosition('B');
            Assert.AreEqual('K', rotor.CryptForward(rotor.Sum('A', rotor.Position)));
            rotor.SetPosition('Z');
            Assert.AreEqual('J', rotor.CryptForward(rotor.Sum('A', rotor.Position)));
        }
        [TestMethod]
        public void TestCryptForward()
        {
            Rotor2 rotor = new Rotor2(rotorDictionary);

            Assert.AreEqual('E', rotor.CryptForward('A'));
            Assert.AreEqual('J', rotor.CryptForward('Z'));
        }
        [TestMethod]
        public void TestCryptBack()
        {
            Rotor2 rotor = new Rotor2(rotorDictionary);

            Assert.AreEqual('A', rotor.CryptBack('E'));
            Assert.AreEqual('Z', rotor.CryptBack('J'));
        }
        [TestMethod]
        public void TestSetPositionsWithCryptBack()
        {
            Rotor2 rotor = new Rotor2(rotorDictionary);

            Assert.AreEqual('R', rotor.Subcribe(rotor.CryptBack('U'), rotor.Position));
            rotor.SetPosition('R');

            Assert.AreEqual('W', rotor.Subcribe(rotor.CryptBack('W'), rotor.Position));
        }
        [TestMethod]
        public void TestSum()
        {
            Rotor2 rotor = new Rotor2(rotorDictionary);
            char expected = 'D';
            char actual = rotor.Sum('B', 'C');

            Assert.AreEqual(expected, actual);

            expected = 'A';
            actual = rotor.Sum('Z', 'B');

            Assert.AreEqual(expected, actual);

            expected = 'Z';
            actual = rotor.Sum('Y', 'B');

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestSubscribe()
        {
            Rotor2 rotor = new Rotor2(rotorDictionary);

            char expected = 'Y';
            char actual = rotor.Subcribe('Z', 'B');

            Assert.AreEqual(expected, actual);

            expected = 'Z';
            actual = rotor.Subcribe('A', 'B');
            Assert.AreEqual(expected, actual);

            expected = 'W';
            actual = rotor.Subcribe('N', 'R');
            Assert.AreEqual(expected, actual);
        }
    }
}
