using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crypto1.Helpers;

namespace Crypto1.Models.Enigma
{
    public class Rotor
    {
        private static readonly IReadOnlyDictionary<char, int> alphabet = new Dictionary<char, int>()
        {
            {'A', 0}, {'B', 1}, {'C', 2}, {'D', 3}, {'E', 4},
            {'F', 5}, {'G', 6}, {'H', 7}, {'I', 8}, {'J', 9},
            {'K', 10}, {'L', 11}, {'M', 12}, {'N', 13}, {'O', 14},
            {'P', 15}, {'Q', 16}, {'R', 17}, {'S', 18}, {'T', 19},
            {'U', 20}, {'V', 21}, {'W', 22}, {'X', 23}, {'Y', 24}, {'Z', 25},
        };
        private static readonly IReadOnlyDictionary<int, char> alphabetReverse = new Dictionary<int, char>()
        {
            {0, 'A'}, {1, 'B'}, {2, 'C'}, {3, 'D'}, {4, 'E'},
            {5, 'F'}, {6, 'G'}, {7, 'H'}, {8, 'I'}, {9, 'J'},
            {10, 'K'}, {11, 'L'}, {12, 'M'}, {13, 'N'}, {14, 'O'},
            {15, 'P'}, {16, 'Q'}, {17, 'R'}, {18, 'S'}, {19, 'T'},
            {20, 'U'}, {21, 'V'}, {22, 'W'}, {23, 'X'}, {24, 'Y'}, {25, 'Z'},
        };
        public static Dictionary<char, int> Alphabet => alphabet as Dictionary<char, int>;
        public char Position { get; set; }
        private int[] symbolIndexes;
        public Dictionary<char, char> SymbolPairs
        {
            get
            {
                Dictionary<char, char> dictionary = new Dictionary<char, char>();
                for (int i = 0; i < symbolIndexes.Length; i++)
                {
                    dictionary.Add(alphabetReverse[i], alphabetReverse[symbolIndexes[i]]);
                }
                return dictionary;
            }
        }
        public Rotor(Dictionary<char, char> symbolPairs)
        {
            Position = 'A';
            symbolIndexes = symbolPairs
                .OrderBy(o => o.Key)
                .Select(d => alphabet[d.Value])
                .ToArray();
        }
        public void SetPosition(char firstSymbol)
        {
            Position = firstSymbol;
        }
        public void RotateForwardOneStep()
        {
            int index = alphabet[this.Position];
            index++;
            Position = alphabetReverse[index % alphabet.Count];
        }
        public char CryptForward(char ch)
        {
            return alphabetReverse[symbolIndexes[alphabet[ch]]];
        }
        public char CryptBack(char ch)
        {
            int index = symbolIndexes.ToList().IndexOf(alphabet[ch]);
            return alphabetReverse[index];
        }
        public char Sum(char ch1, char ch2)
        {
            return alphabetReverse[(alphabet[ch1] + alphabet[ch2]) % (alphabet.Count)];
        }
        public char Subcribe(char ch1, char ch2)
        {
            return alphabetReverse[(alphabet[ch1] - alphabet[ch2] + (alphabet.Count)) % alphabet.Count];
        }
    }
}
