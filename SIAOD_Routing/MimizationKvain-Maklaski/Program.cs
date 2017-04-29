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
        static void Main(string[] args)
        {
            string result = "ERROR";
            int count;
            string function;
            if(args.Count() > 0)
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
                    Console.Write(Convert.ToString(term, 2)+" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(new String('*',30));

            var firstMerge = groupsByOneBits
                .SelectMany(group => group
                    .SelectMany(term1 => groupsByOneBits
                        .Where(x => (x.Key == (group.Key + 1)) && x != groupsByOneBits.Last())
                        .SelectMany(group2 => group2
                            .Where(term2 => getOneBitCount(getBitsDefferent(term1, term2), count) == 1)
                            .Select(term2 => new{
                                Value = term1,
                                DiffBit = getBitsDefferent(term1, term2)
                                })
                        )
                    )
                )
                .GroupBy(key => key.DiffBit)
                .OrderByDescending(group=>group.Key);

            foreach (var group in firstMerge)
            {
                Console.Write(Convert.ToString(group.Key, 16) + " - ");
                foreach (var term in group)
                {
                    Console.Write(Convert.ToString(term.Value, 2) + " ");
                }
            }
            Console.WriteLine();
            Console.WriteLine(new String('*',50));

            //IEnumerable<IGrouping<int,void>> a;
            var secondMerge = firstMerge
                .SelectMany(group => group
                    .SelectMany(term1 => firstMerge
                        .Where(group2 => group2.Key == group.Key)
                        .SelectMany(group2 => group2
                            .Where(term2 => getOneBitCount(getBitsDefferent(term1.Value | group.Key, term2.Value | group.Key), count) == 1)
                            .Select(term2 => new {
                                Value = term1.Value | getBitsDefferent(term1.Value | group.Key, term2.Value | group.Key) | group.Key,
                                DiffBit = getBitsDefferent(term1.Value | group.Key, term2.Value | group.Key) | group.Key
                                }
                            )
                        )
                    )
                )
                .GroupBy(key => key.DiffBit)
                .Select(group3 => group3.First())
                .GroupBy(key => key.DiffBit);

            while (true)
            {

            }

            foreach (var group in secondMerge)
            {
                Console.Write(Convert.ToString(group.Key,16) + " - ");
                foreach (var item in group)
                {
                    Console.Write(Convert.ToString(item.Value, 2) + " ");
                }
            }
            Console.WriteLine();
            Console.WriteLine(new String('*', 50));
            Console.Read();

            Console.WriteLine(result);
        }
        public static bool comporator()
        {

            return true;
        }
        public static int getOneBitCount(int data,int bitCount)
        {
            int oneBitCount = 0;
            while (bitCount--!=0)
            {
                if ((data & 1) != 0)
                    oneBitCount++;
                data >>= 1;
            }
            return oneBitCount;
        }
        public static int getBitsDefferent(int leftData,int rightData)
        {
            return (leftData ^ rightData);
        }
    }
}
