using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MimizationKvain_Maklaski
{
    class Program
    {
        private static int count = 0;

        static void Main(string[] args)
        {
            string result = "ERROR";
            string function;
            if (args.Count() > 0)
            {
                count = int.Parse(args[0]);
                function = args[1];
            }
            else
            {
                Console.WriteLine("Arguments incorrect!");
                return;
            }

            var minTerms = function
                .Select((x, index) => new { Term = x, Index = index })
                .Where(x => x.Term == '1')
                .Select(x => x.Index);

            var groupsByOneBits = minTerms
                .GroupBy(x => getOneBitCount(x, count));


            foreach (var group in groupsByOneBits)
            {
                Console.Write(group.Key + " - ");
                foreach (var term in group)
                {
                    Console.Write(Convert.ToString(term, 2) + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(new String('*', 50));

            var firstMerge = groupsByOneBits
                .SelectMany(group => group
                    .SelectMany(term1 => groupsByOneBits
                        .Where(x => (x.Key == (group.Key + 1)) && x != groupsByOneBits.Last())
                        .SelectMany(group2 => group2
                            .Where(term2 => getOneBitCount(getBitsDefferent(term1, term2), count) == 1)
                            .Select(term2 => new Term {
                                Value = term1,
                                DiffBit = getBitsDefferent(term1, term2)
                            })
                        )
                    )
                )
                .GroupBy(key => key.DiffBit);

            PrintTermEnumerable(firstMerge);

            List<IEnumerable<IGrouping<int, Term>>> termTables = new List<IEnumerable<IGrouping<int, Term>>>();

            var temp = firstMerge;
            while (true)
            {
                var terms = getEnumerableGroupTerms(temp);
                if (terms.Count() == 0)
                    break;
                temp = terms;
                termTables.Add(terms);
                break;
            }

            var terms2 = getEnumerableGroupTerms(termTables.First());
            foreach (var term in termTables)
                PrintTermEnumerable(term);
            
            Console.Read();

            Console.WriteLine(result);
        }

        public static int getOneBitCount(int data, int bitCount)
        {
            int oneBitCount = 0;
            while (bitCount-- != 0)
            {
                if ((data & 1) != 0)
                    oneBitCount++;
                data >>= 1;
            }
            return oneBitCount;
        }
        public static int getBitsDefferent(int leftData, int rightData)
        {
            return (leftData ^ rightData);
        }
        public static IEnumerable<IGrouping<int, Term>> getEnumerableGroupTerms(IEnumerable<IGrouping<int, Term>> enumerable)
        {
            return enumerable
                .SelectMany(group => group
                    .SelectMany(term1 => enumerable
                        .Where(group2 => group2.Key == group.Key)
                        .SelectMany(group2 => group2
                            .Where(term2 => getOneBitCount(getBitsDefferent(term1.Value | group.Key, term2.Value | group.Key), count) == 1)
                            .Select(term2 => new Term
                            {
                                Value = term1.Value | getBitsDefferent(term1.Value | group.Key, term2.Value | group.Key) | group.Key,
                                DiffBit = getBitsDefferent(term1.Value | group.Key, term2.Value | group.Key) | group.Key
                            }
                            )
                        )
                    )
                )
                .GroupBy(key => key.DiffBit)
                .Select(group3 => group3.First()
                    
                )
                .GroupBy(key => key.DiffBit);
        }
        public static void PrintTermEnumerable(IEnumerable<IGrouping<int, Term>> term)
        {
            foreach (var group in term)
            {
                Console.Write(Convert.ToString(group.Key, 16) + " - ");
                foreach (var item in group)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var valueBit = item.Value >> (count - 1 - i) & ~0xfffe;
                        var diffBit = item.DiffBit >> (count - 1 - i) & ~0xfffe;

                        if (diffBit == 1)
                            Console.Write("x");
                        else
                            Console.Write(Convert.ToString(valueBit, 2));
                    }
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
            Console.WriteLine(new String('*', 50));
        }
    }
    class Term
    {
        public int Value { get; set; }
        public int DiffBit { get; set; }
    }
}
