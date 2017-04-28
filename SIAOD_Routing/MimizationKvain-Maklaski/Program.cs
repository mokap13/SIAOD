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

            var miniTerms = function
                .Select((x, index) => new { Item = x, Index = index })
                .Where(x => x.Item == '1')
                .Select(x => x.Index);

            var groupsByOneBits = miniTerms
                .GroupBy(x => getOneBitCount(x, count));


            foreach (var group in groupsByOneBits)
            {
                Console.Write(group.Key + " - ");
                foreach (var item in group)
                {
                    Console.Write(Convert.ToString(item, 2)+" ");
                }
                Console.WriteLine();
            }
            var a = getDifferentBits(0x2, 0xa);

            Console.WriteLine(new String('*',30));
            //var asd = groupsByOneBits
            //    .SelectMany(group => group
            //        .SelectMany(term => groupsByOneBits.Where(x => (x.Key == (group.Key + 1)) && x != groupsByOneBits.Last())
            //            .SelectMany(group2 => group2
            //                .Select(term2 => getDifferentBits(term,term2))
            //                .Where(x=>getOneBitCount(x,count)==1)
            //                ))).ToList();

            var asd = groupsByOneBits
                .SelectMany(group => group
                    .SelectMany(term => groupsByOneBits.Where(x => (x.Key == (group.Key + 1)) && x != groupsByOneBits.Last())
                        .SelectMany(group2 => group2
                            .Select(term2 => term2)
                            .Where(x => getOneBitCount(getDifferentBits(term,x), count) == 1)
                            .GroupBy(g => getDifferentBits(term, g))
                            .OrderBy(x=>x.Key)
                            )));

            foreach (var group in asd)
            {
                Console.Write(group.Key + " - ");
                foreach (var term in group)
                {
                    Console.Write(Convert.ToString(term, 2) + " ");
                }
            }

            //var g = groupsByOneBits.Where(x=>x.Key == 2).SelectMany(x=>x.Select(y=>y)).ToList();

            foreach (var group in groupsByOneBits)
            {
                foreach (var item in group)
                {
                    foreach (var group2 in groupsByOneBits.Where(x=>x.Key == group.Key+1 && x!= groupsByOneBits.Last()))
                    {
                        foreach (var item2 in group2)
                        {
                            var temp = getDifferentBits(item, item2);
                            if(getOneBitCount(temp,count)==1)
                                Console.Write(Convert.ToString(getDifferentBits(item, item2), 16) + " ");
                        }
                    }
                }
                Console.WriteLine();
            }


            Console.Read();

            Console.WriteLine(result);
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
        public static int getDifferentBits(int leftData,int rightData)
        {
            //Console.WriteLine();
            //Console.WriteLine($"{Convert.ToString(leftData,2)}   {Convert.ToString(rightData,2)}");
            //Console.WriteLine($"Diff ^   {Convert.ToString(leftData^rightData,2)}");
            return (leftData ^ rightData);
        }
    }
}
