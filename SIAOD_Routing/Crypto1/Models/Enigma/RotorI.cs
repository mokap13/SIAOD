using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto1.Models.Enigma
{
    class RotorI : Rotor
    {
        private readonly Dictionary<char, char> charPairDictionary = new Dictionary<char, char>
        {
            {'A', 'E'}, {'B', 'K'}, {'C', 'M'}, {'D', 'F'}, {'E', 'L'},
            {'F', 'G'}, {'G', 'D'}, {'H', 'Q'}, {'I', 'V'}, {'J', 'Z'},
            {'K', 'N'}, {'L', 'T'}, {'M', 'O'}, {'N', 'W'}, {'O', 'Y'},
            {'P', 'H'}, {'Q', 'X'}, {'R', 'U'}, {'S', 'S'}, {'T', 'P'},
            {'U', 'A'}, {'V', 'I'}, {'W', 'B'}, {'X', 'R'}, {'Y', 'C'}, {'Z', 'J'},
        };
        public override Dictionary<char, char> CharPairDictionary { get => charPairDictionary; }

        private readonly char charTrigger = 'R';
        public override bool IsCurrentPositionTriggerChar => base.Position == this.charTrigger;
    }
    class RotorII : Rotor
    {
        private readonly Dictionary<char, char> charPairDictionary = new Dictionary<char, char>
        {
            {'A', 'A'}, {'B', 'J'}, {'C', 'D'}, {'D', 'K'}, {'E', 'S'},
            {'F', 'I'}, {'G', 'R'}, {'H', 'U'}, {'I', 'X'}, {'J', 'B'},
            {'K', 'L'}, {'L', 'H'}, {'M', 'W'}, {'N', 'T'}, {'O', 'M'},
            {'P', 'C'}, {'Q', 'Q'}, {'R', 'G'}, {'S', 'Z'}, {'T', 'N'},
            {'U', 'P'}, {'V', 'Y'}, {'W', 'F'}, {'X', 'V'}, {'Y', 'O'}, {'Z', 'E'},
        };
        public override Dictionary<char, char> CharPairDictionary { get => charPairDictionary; }

        private readonly char charTrigger = 'F';
        public override bool IsCurrentPositionTriggerChar => base.Position == this.charTrigger;
    }
    class RotorIII : Rotor
    {
        private readonly Dictionary<char, char> charPairDictionary = new Dictionary<char, char>
        {
            {'A', 'B'}, {'B', 'D'}, {'C', 'F'}, {'D', 'H'}, {'E', 'J'},
            {'F', 'L'}, {'G', 'C'}, {'H', 'P'}, {'I', 'R'}, {'J', 'T'},
            {'K', 'X'}, {'L', 'V'}, {'M', 'Z'}, {'N', 'N'}, {'O', 'Y'},
            {'P', 'E'}, {'Q', 'I'}, {'R', 'W'}, {'S', 'G'}, {'T', 'A'},
            {'U', 'K'}, {'V', 'M'}, {'W', 'U'}, {'X', 'S'}, {'Y', 'Q'}, {'Z', 'O'},
        };
        public override Dictionary<char, char> CharPairDictionary { get => charPairDictionary; }

        private readonly char charTrigger = 'W';
        public override bool IsCurrentPositionTriggerChar => base.Position == this.charTrigger;
    }
    class RotorReflector : Rotor
    {
        private readonly Dictionary<char, char> charPairDictionary = new Dictionary<char, char>
        {
            {'A', 'Y'}, {'B', 'R'}, {'C', 'U'}, {'D', 'H'}, {'E', 'Q'},
            {'F', 'S'}, {'G', 'L'}, {'H', 'D'}, {'I', 'P'}, {'J', 'X'},
            {'K', 'N'}, {'L', 'G'}, {'M', 'O'}, {'N', 'K'}, {'O', 'M'},
            {'P', 'I'}, {'Q', 'E'}, {'R', 'B'}, {'S', 'F'}, {'T', 'Z'},
            {'U', 'C'}, {'V', 'W'}, {'W', 'V'}, {'X', 'J'}, {'Y', 'A'}, {'Z', 'T'},
        };
        public override Dictionary<char, char> CharPairDictionary => charPairDictionary;
        public override bool IsCurrentPositionTriggerChar => false;
    }
}
