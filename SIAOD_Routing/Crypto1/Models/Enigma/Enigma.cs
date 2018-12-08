using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto1.Models.Enigma
{
    public class Enigma
    {
        private readonly IReadOnlyDictionary<char, int> alphabet = new Dictionary<char, int>()
        {
            {'A', 0}, {'B', 1}, {'C', 2}, {'D', 3}, {'E', 4},
            {'F', 5}, {'G', 6}, {'H', 7}, {'I', 8}, {'J', 9},
            {'K', 10}, {'L', 11}, {'M', 12}, {'N', 13}, {'O', 14},
            {'P', 15}, {'Q', 16}, {'R', 17}, {'S', 18}, {'T', 19},
            {'U', 20}, {'V', 21}, {'W', 22}, {'X', 23}, {'Y', 24}, {'Z', 25},
        };
        //private readonly IReadOnlyDictionary<char, int> alphabet = new Dictionary<char>()
        //{
        //    'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
        //    'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
        //};
        private int[,] Rotor1CrossTable;
        private readonly Rotor<char, char> Rotor1Dictionary = new Rotor<char, char>()
        {
            {'A', 'E'}, {'B', 'K'}, {'C', 'M'}, {'D', 'F'}, {'E', 'L'},
            {'F', 'G'}, {'G', 'D'}, {'H', 'Q'}, {'I', 'V'}, {'J', 'Z'},
            {'K', 'N'}, {'L', 'T'}, {'M', 'O'}, {'N', 'W'}, {'O', 'Y'},
            {'P', 'H'}, {'Q', 'X'}, {'R', 'U'}, {'S', 'S'}, {'T', 'P'},
            {'U', 'A'}, {'V', 'I'}, {'W', 'B'}, {'X', 'R'}, {'Y', 'C'}, {'Z', 'J'},
        };
        private readonly Rotor<char, char> Rotor2Dictionary = new Rotor<char, char>()
        {
            {'A', 'A'}, {'B', 'J'}, {'C', 'D'}, {'D', 'K'}, {'E', 'S'},
            {'F', 'I'}, {'G', 'R'}, {'H', 'U'}, {'I', 'X'}, {'J', 'B'},
            {'K', 'L'}, {'L', 'H'}, {'M', 'W'}, {'N', 'T'}, {'O', 'M'},
            {'P', 'C'}, {'Q', 'Q'}, {'R', 'G'}, {'S', 'Z'}, {'T', 'N'},
            {'U', 'P'}, {'V', 'Y'}, {'W', 'F'}, {'X', 'V'}, {'Y', 'O'}, {'Z', 'E'},
        };
        private readonly Rotor<char, char> Rotor3Dictionary = new Rotor<char, char>()
        {
            {'A', 'B'}, {'B', 'D'}, {'C', 'F'}, {'D', 'H'}, {'E', 'J'},
            {'F', 'L'}, {'G', 'C'}, {'H', 'P'}, {'I', 'R'}, {'J', 'T'},
            {'K', 'X'}, {'L', 'V'}, {'M', 'Z'}, {'N', 'N'}, {'O', 'Y'},
            {'P', 'E'}, {'Q', 'I'}, {'R', 'W'}, {'S', 'G'}, {'T', 'A'},
            {'U', 'K'}, {'V', 'M'}, {'W', 'U'}, {'X', 'S'}, {'Y', 'Q'}, {'Z', 'O'},
        };

        private readonly Rotor<char, char> ReflectorBDictionary = new Rotor<char, char>()
        {
            {'A', 'Y'}, {'B', 'R'}, {'C', 'U'}, {'D', 'H'}, {'E', 'Q'},
            {'F', 'S'}, {'G', 'L'}, {'H', 'D'}, {'I', 'P'}, {'J', 'X'},
            {'K', 'N'}, {'L', 'G'}, {'M', 'O'}, {'N', 'K'}, {'O', 'M'},
            {'P', 'I'}, {'Q', 'E'}, {'R', 'B'}, {'S', 'F'}, {'T', 'Z'},
            {'U', 'C'}, {'V', 'W'}, {'W', 'V'}, {'X', 'J'}, {'Y', 'A'}, {'Z', 'T'},
        };
         
        public Enigma()
        {
            Rotor1CrossTable = new int[this.alphabet.Count, 2];

        }

        public Enigma(char Rotar1Position, char Rotar2Position, char Rotar3Position)
        {
            Rotor1Dictionary.SetPosition(Rotar1Position);
            Rotor2Dictionary.SetPosition(Rotar3Position);
            Rotor2Dictionary.SetPosition(Rotar3Position);
        }

        public char CryptChar(char ch)
        {
            char ch1 = Rotor1Dictionary.Forward[ch];
            char ch2 = Rotor2Dictionary.Forward[ch1];
            char ch3 = Rotor3Dictionary.Forward[ch2];
            char chReflector = ReflectorBDictionary.Forward[ch3];
            char ch3Reverse = Rotor3Dictionary.Reverse[chReflector];
            char ch2Reverse = Rotor2Dictionary.Reverse[ch3Reverse];
            char ch1Reverse = Rotor1Dictionary.Reverse[ch2Reverse];
            return ch1Reverse;
        }
        //public string CryptString(string source)
        //{
        //    char ch1 = Rotor1Dictionary.Forward[ch];
        //    char ch2 = Rotor2Dictionary.Forward[ch1];
        //    char ch3 = Rotor3Dictionary.Forward[ch2];
        //    char chReflector = ReflectorBDictionary.Forward[ch3];
        //    char ch3Reverse = Rotor3Dictionary.Reverse[chReflector];
        //    char ch2Reverse = Rotor2Dictionary.Reverse[ch3Reverse];
        //    char ch1Reverse = Rotor1Dictionary.Reverse[ch2Reverse];
        //    return ch1Reverse;
        //}

    }
}
