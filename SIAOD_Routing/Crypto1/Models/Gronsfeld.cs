using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto1.Models
{
    public class Gronsfeld
    {
        //private readonly static Dictionary<char, int> alphabetDictionary = new Dictionary<char, int>(){
        //    {'а', 0}, {'б', 1}, {'в', 2}, {'г', 3}, {'д', 4}, {'е', 5},
        //    {'ё', 6}, {'ж', 7}, {'з', 8}, {'и', 9}, {'й', 10}, {'к', 11},
        //    {'л', 12}, {'м', 13}, {'н', 14}, {'о', 15}, {'п', 16}, {'р', 17},
        //    {'с', 18}, {'т', 19}, {'у', 20}, {'ф', 21}, {'х', 22}, {'ц', 23},
        //    {'ч', 24}, {'ш', 25}, {'щ', 26}, {'ъ', 27}, {'ы', 28}, {'ь', 29},
        //    {'э', 30}, {'ю', 31}, {'я', 32}};
        private readonly static Dictionary<char, int> alphabetDictionary = new Dictionary<char, int>(){
            {'а', 0}, {'б', 1}, {'в', 2}, {'г', 3}, {'д', 4}, {'е', 5},
            { 'ж', 6}, {'з', 7}, {'и', 8}, {'й', 9}, {'к', 10},
            {'л', 11}, {'м', 12}, {'н', 13}, {'о', 14}, {'п', 15}, {'р', 16},
            {'с', 17}, {'т', 18}, {'у', 19}, {'ф', 20}, {'х', 21}, {'ц', 22},
            {'ч', 23}, {'ш', 24}, {'щ', 25}, {'ъ', 26}, {'ы', 27}, {'ь', 28},
            {'э', 29}, {'ю', 30}, {'я', 31}};
        //private readonly static char[] alphabet = {
        //    'а', 'б', 'в', 'г', 'д', 'е',
        //    'ё', 'ж', 'з', 'и', 'й', 'к',
        //    'л', 'м', 'н', 'о', 'п', 'р',
        //    'с', 'т', 'у', 'ф', 'х', 'ц',
        //    'ч', 'ш', 'щ', 'ъ', 'ы', 'ь',
        //    'э', 'ю', 'я' };
        private readonly static char[] alphabet = {
            'а', 'б', 'в', 'г', 'д', 'е',
            'ж', 'з', 'и', 'й', 'к',
            'л', 'м', 'н', 'о', 'п', 'р',
            'с', 'т', 'у', 'ф', 'х', 'ц',
            'ч', 'ш', 'щ', 'ъ', 'ы', 'ь',
            'э', 'ю', 'я' };

        private int[] key;

        public Gronsfeld(int[] key)
        {
            this.key = key;
        }
        public char CryptChar(char ch)
        {
            if (!alphabetDictionary.ContainsKey(ch))
                throw new Exception("Символ недопустим");
            return alphabet[(alphabetDictionary[ch] + this.key[0]) % alphabet.Length];
        }
        public char DeсryptChar(char ch)
        {
            if (!alphabetDictionary.ContainsKey(ch))
                throw new Exception("Символ недопустим");
            return alphabet[(alphabetDictionary[ch] - this.key[0]) % alphabet.Length];
        }
        public string CryptText(string text)
        {
            text = text.ToLower();
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                if (!char.IsWhiteSpace(text[i]))
                {
                    if (!alphabetDictionary.ContainsKey(text[i]))
                        throw new Exception("Символ недопустим");
                    int currentCharPosition = alphabetDictionary[text[i]];
                    int newCharPosition = (currentCharPosition + this.key[i % key.Length]) %
                        alphabet.Length;
                    str.Append(alphabet[newCharPosition]);
                }
            }
            return str.ToString();
        }

        public string DecryptText(string text)
        {
            text = text.ToLower();
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                if (!char.IsWhiteSpace(text[i]))
                {
                    if (!alphabetDictionary.ContainsKey(text[i]))
                        throw new Exception("Символ недопустим");
                    int currentCharPosition = alphabetDictionary[text[i]];
                    int newCharPosition = 
                        ((currentCharPosition - this.key[i % key.Length]) %
                        alphabet.Length + alphabet.Length) % alphabet.Length;

                    str.Append(alphabet[newCharPosition]);
                }
            }
            return str.ToString();
        }
    }
}
