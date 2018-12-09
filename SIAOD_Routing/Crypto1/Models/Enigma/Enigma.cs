using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto1.Models.Enigma
{
    public class Enigma
    {
        private readonly List<Rotor2> rotors;

        private readonly Dictionary<char, char> Rotor1Dictionary = new Dictionary<char, char>()
        {
            {'A', 'E'}, {'B', 'K'}, {'C', 'M'}, {'D', 'F'}, {'E', 'L'},
            {'F', 'G'}, {'G', 'D'}, {'H', 'Q'}, {'I', 'V'}, {'J', 'Z'},
            {'K', 'N'}, {'L', 'T'}, {'M', 'O'}, {'N', 'W'}, {'O', 'Y'},
            {'P', 'H'}, {'Q', 'X'}, {'R', 'U'}, {'S', 'S'}, {'T', 'P'},
            {'U', 'A'}, {'V', 'I'}, {'W', 'B'}, {'X', 'R'}, {'Y', 'C'}, {'Z', 'J'},
        };
        private readonly Dictionary<char, char> Rotor2Dictionary = new Dictionary<char, char>()
        {
            {'A', 'A'}, {'B', 'J'}, {'C', 'D'}, {'D', 'K'}, {'E', 'S'},
            {'F', 'I'}, {'G', 'R'}, {'H', 'U'}, {'I', 'X'}, {'J', 'B'},
            {'K', 'L'}, {'L', 'H'}, {'M', 'W'}, {'N', 'T'}, {'O', 'M'},
            {'P', 'C'}, {'Q', 'Q'}, {'R', 'G'}, {'S', 'Z'}, {'T', 'N'},
            {'U', 'P'}, {'V', 'Y'}, {'W', 'F'}, {'X', 'V'}, {'Y', 'O'}, {'Z', 'E'},
        };
        private readonly Dictionary<char, char> Rotor3Dictionary = new Dictionary<char, char>()
        {
            {'A', 'B'}, {'B', 'D'}, {'C', 'F'}, {'D', 'H'}, {'E', 'J'},
            {'F', 'L'}, {'G', 'C'}, {'H', 'P'}, {'I', 'R'}, {'J', 'T'},
            {'K', 'X'}, {'L', 'V'}, {'M', 'Z'}, {'N', 'N'}, {'O', 'Y'},
            {'P', 'E'}, {'Q', 'I'}, {'R', 'W'}, {'S', 'G'}, {'T', 'A'},
            {'U', 'K'}, {'V', 'M'}, {'W', 'U'}, {'X', 'S'}, {'Y', 'Q'}, {'Z', 'O'},
        };
        private readonly Dictionary<char, char> ReflectorBDictionary = new Dictionary<char, char>()
        {
            {'A', 'Y'}, {'B', 'R'}, {'C', 'U'}, {'D', 'H'}, {'E', 'Q'},
            {'F', 'S'}, {'G', 'L'}, {'H', 'D'}, {'I', 'P'}, {'J', 'X'},
            {'K', 'N'}, {'L', 'G'}, {'M', 'O'}, {'N', 'K'}, {'O', 'M'},
            {'P', 'I'}, {'Q', 'E'}, {'R', 'B'}, {'S', 'F'}, {'T', 'Z'},
            {'U', 'C'}, {'V', 'W'}, {'W', 'V'}, {'X', 'J'}, {'Y', 'A'}, {'Z', 'T'},
        };
        
        public Enigma()
        {
            rotors = new List<Rotor2>
            {
                new Rotor2(Rotor1Dictionary),
                new Rotor2(Rotor2Dictionary),
                new Rotor2(Rotor3Dictionary),
                new Rotor2(ReflectorBDictionary),
            };
        }
        public Enigma(List<Rotor2> rotors)
        {
            this.rotors = rotors;
        }
        public void SetPositionRotor(int index, char ch)
        {
            if (index > rotors.Count - 2)
                throw new ArgumentException("Указанный индек больше чем число роторов");
            this.rotors[index].SetPosition(ch);
        }
        
        public char CryptChar(char ch)
        {
            ch = rotors[0].Sum(ch, rotors[0].Position);
            ch = rotors[0].CryptForward(ch);

            ch = rotors[1].Sum(ch, rotors[1].Position);
            ch = rotors[1].Subcribe(ch, rotors[0].Position);
            ch = rotors[1].CryptForward(ch);

            ch = rotors[2].Sum(ch, rotors[2].Position);
            ch = rotors[2].Subcribe(ch, rotors[1].Position);
            ch = rotors[2].CryptForward(ch);

            //Рефлектор
            ch = rotors[2].Subcribe(ch, rotors[2].Position);
            ch = rotors[3].CryptForward(ch);
            ch = rotors[2].Sum(ch, rotors[2].Position);

            ch = rotors[2].CryptBack(ch);
            ch = rotors[2].Subcribe(ch, rotors[2].Position);
            ch = rotors[2].Sum(ch, rotors[1].Position);

            ch = rotors[1].CryptBack(ch);
            ch = rotors[1].Subcribe(ch, rotors[1].Position);
            ch = rotors[1].Sum(ch, rotors[0].Position);

            ch = rotors[0].CryptBack(ch);
            ch = rotors[0].Subcribe(ch, rotors[0].Position);
            //foreach (Rotor2 rotor in this.rotors)
            //    ch = rotor.CryptForward(ch);
            //foreach (Rotor2 rotor in this.rotors.Take(this.rotors.Count-1).Reverse())
            //    ch = rotor.CryptBack(ch);
            return ch;
        }
        public string CryptString(string source)
        {
            StringBuilder str = new StringBuilder();
            foreach (char item in source)
            {
                char ch = item;
                ch = this.rotors.First().CryptForward(ch);
                this.rotors.First().RotateForwardOneStep();
                foreach (Rotor2 rotor in this.rotors.Skip(1))
                    ch = rotor.CryptForward(ch);
                foreach (Rotor2 rotor in this.rotors.Take(this.rotors.Count - 1).Reverse())
                    ch = rotor.CryptBack(ch);
                str.Append(ch);
            }
            
            return str.ToString();
        }
    }
}
