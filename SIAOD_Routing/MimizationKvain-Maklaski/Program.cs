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
        private static IEnumerable<int> minTerms;

        static void Main(string[] args)
        {
            string boolFunction;
            if (args.Count() > 0)
            {
                count = int.Parse(args[0]);
                boolFunction = args[1];
            }
            else
            {
                Console.WriteLine("Arguments incorrect!");
                return;
            }

            minTerms = boolFunction
                .Select((x, index) => new { Term = x, Index = index })
                .Where(x => x.Term == '1')
                .Select(x => x.Index);

            var groupsByOneBits = minTerms
                .GroupBy(x => getOneBitCount(x, count));

            //foreach (var group in groupsByOneBits)
            //{
            //    Console.Write(group.Key + " - ");
            //    foreach (var term in group)
            //    {
            //        Console.Write(Convert.ToString(term, 2) + " ");
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine(new String('*', 50));

            var firstMerge = groupsByOneBits
                .SelectMany(group => group
                    .SelectMany(term1 => groupsByOneBits
                        .Where(x => (x.Key == (group.Key + 1)) && x != groupsByOneBits.Last())
                        .SelectMany(group2 => group2
                            .Where(term2 => getOneBitCount(getBitsDefferent(term1, term2), count) == 1)
                            .Select(term2 => new Term {
                                Value = term1,
                                DiffBit = getBitsDefferent(term1, term2),
                                IsCore = true
                            })
                        )
                    )
                )
                .GroupBy(key => key.DiffBit);

            //PrintTermEnumerable(firstMerge);

            List<IEnumerable<IGrouping<int, Term>>> listGroupsOfTerms = new List<IEnumerable<IGrouping<int, Term>>>();
            listGroupsOfTerms.Add(firstMerge);

            while (true)
            {
                var terms = getMergeTerms(listGroupsOfTerms.Last());
                if (terms.Count() == 0)
                    break;
                listGroupsOfTerms.Add(terms);
            }

            //foreach (var term in listGroupsOfTerms)
            //    PrintTermEnumerable(term);

            var core = getNonAbsorbedTerms(listGroupsOfTerms);
            //PrintTermList(core);
            var result = getResult(core, minTerms.ToList());
            //PrintTermList(result);
            Console.WriteLine();
            PrintTermResult(result);
            Console.Read();
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
        public static IEnumerable<IGrouping<int, Term>> getMergeTerms(IEnumerable<IGrouping<int, Term>> groupsOfTerms) 
        {
            return groupsOfTerms
                .SelectMany(group => group
                    .SelectMany(term1 => groupsOfTerms
                        .Where(group2 => group2.Key == group.Key)
                        .SelectMany(group2 => group2
                            .Where(term2 => getOneBitCount(getBitsDefferent(term1.Value | group.Key, term2.Value | group.Key), count) == 1)
                            .Select(term2 => new Term
                                {
                                    Value = term1.Value | getBitsDefferent(term1.Value | group.Key, term2.Value | group.Key) | group.Key,
                                    DiffBit = getBitsDefferent(term1.Value | group.Key, term2.Value | group.Key) | group.Key,
                                    IsCore = true
                                }
                            )
                        )
                    )
                )
                .GroupBy(key => key.DiffBit)
                .SelectMany(group3 => group3
                    .GroupBy(term=>term.Value|term.DiffBit)
                    .Select(group4=>group4.First())
                )
                .GroupBy(key => key.DiffBit);
        }
        public static void PrintTermEnumerable(IEnumerable<IGrouping<int, Term>> groupsOfTerms)
        {
            foreach (var group in groupsOfTerms)
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
        public static void PrintTermList(List<Term> list)
        {
            foreach (var item in list)
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
        public static void PrintTermResult(List<Term> list)
        {
            foreach (var item in list)
            {
                for (int i = 0; i < count; i++)
                {
                    var valueBit = item.Value >> (count - 1 - i) & ~0xfffe;
                    var diffBit = item.DiffBit >> (count - 1 - i) & ~0xfffe;

                    if (diffBit != 1)
                    {
                        if (valueBit == 0)
                            Console.Write((char)(97+ i));
                        else
                            Console.Write(Char.ToUpperInvariant((char)(97 + i)));
                    }
                }
                if (item == list.Last())
                    break;
                Console.Write(" v ");
            }
        }
        public static void PrintIntAsBinary(int value)
        {
            for (int i = 0; i < minTerms.Count(); i++)
            {
                var valueBit = value >> (minTerms.Count() - 1 - i) & ~0xfffe;
                //var diffBit = item.DiffBit >> (count - 1 - i) & ~0xfffe;
                Console.Write(Convert.ToString(valueBit, 2));
            }
        }
        public static void PrintTerm(Term term)
        {
            for (int i = 0; i < count; i++)
            {
                var valueBit = term.Value >> (count - 1 - i) & ~0xfffe;
                var diffBit = term.DiffBit >> (count - 1 - i) & ~0xfffe;

                if (diffBit == 1)
                    Console.Write("x");
                else
                    Console.Write(Convert.ToString(valueBit, 2));
            }
            Console.Write(" ");
        }
        public static List<Term> getNonAbsorbedTerms(List<IEnumerable<IGrouping<int, Term>>> listGroupsOfTerms)
        {
            var listsOfTerms = listGroupsOfTerms
                .Select(en => en
                    .SelectMany(group => group
                        .Select(term => term)
                    ).ToList()
                )
                .Reverse()
                .ToList();

            for (int mainTerms = 0; mainTerms < listsOfTerms.Count; mainTerms++)
            {
                for (int mainTerm = 0; mainTerm < listsOfTerms[mainTerms].Count; mainTerm++)
                {
                    for (int subTerms = 1+ mainTerms; subTerms < listsOfTerms.Count; subTerms++)
                    {
                        for (int subTerm = 0; subTerm < listsOfTerms[subTerms].Count; subTerm++)
                        {
                            if ((listsOfTerms[mainTerms][mainTerm].Value | listsOfTerms[mainTerms][mainTerm].DiffBit) == (listsOfTerms[subTerms][subTerm].Value | listsOfTerms[mainTerms][mainTerm].DiffBit)
                                )
                                listsOfTerms[subTerms][subTerm].IsCore = false;
                        }
                    }
                }
            }

            var remainingTerms = listsOfTerms
                .SelectMany(li=>li
                    .Where(term => term.IsCore == true)
                );

            return remainingTerms.ToList();
        }
        public static List<Term> getResult(List<Term> listOfTerms,List<int> boolFunction)
        {
            List<Term> result = new List<Term>();
            int[,] Table = new int[listOfTerms.Count, boolFunction.Count()];
            List<int> commonCoverGrade = new List<int>();

            for (int i = 0; i < listOfTerms.Count; i++)
            {
                for (int j = 0; j < boolFunction.Count(); j++)
                {
                    if ((boolFunction[j] | listOfTerms[i].DiffBit) == (listOfTerms[i].Value | listOfTerms[i].DiffBit))
                    {
                        Table[i, j] = 1;
                        listOfTerms[i].minTerms.Add(boolFunction[j]);
                    }
                }
            }

            //Console.WriteLine();
            //Console.WriteLine();
            //for (int i = 0; i < listOfTerms.Count; i++)
            //{
            //    PrintTerm(listOfTerms[i]);
            //    Console.WriteLine();
            //}
            //Console.WriteLine();

            var coverValue = 0;
            for (int i = 0; i < boolFunction.Count(); i++)
            {
                var indexCover = 0;
                for (int j = 0; j < listOfTerms.Count; j++)
                {
                    coverValue += Table[j, i];
                    if (Table[j, i] == 1)
                        indexCover = j;
                }
                if (coverValue == 1)
                {
                    result.Add(listOfTerms[indexCover]);
                    result = result.Distinct().ToList();
                    commonCoverGrade.AddRange(listOfTerms[indexCover].minTerms);
                    commonCoverGrade = commonCoverGrade.Distinct().ToList();
                    List<int> tempTerms = new List<int>();

                    tempTerms.AddRange(listOfTerms[indexCover].minTerms);
                    foreach (var term in listOfTerms)
                        term.minTerms = term.minTerms
                            .Where(minTerm => !tempTerms.Contains(minTerm))
                            .ToList();
                }
                coverValue = 0;
            }

            while (!IsCovered(commonCoverGrade))
            {
                var max = listOfTerms
                    .GroupBy(term => term.minTerms.Count)
                    .OrderByDescending(g => g.Key)
                    .Select(x => x.First())
                    .First();
                result.Add(max);
                commonCoverGrade.AddRange(max.minTerms);
                commonCoverGrade = commonCoverGrade.Distinct().ToList();
                List<int> tempTerms = new List<int>();

                tempTerms.AddRange(max.minTerms);
                foreach (var term in listOfTerms)
                    term.minTerms = term.minTerms
                        .Where(minTerm => !tempTerms.Contains(minTerm))
                        .ToList();

            }



            return result;
        }
        public static bool IsFullCover(int commonCoverGrade)
        {
            for (int i = 0; i < minTerms.Count(); i++)
            {
                if ((commonCoverGrade & 1) == 0)
                    return false;
                commonCoverGrade >>= 1;
            }
            return true;
        }
        public static bool IsCovered(List<int> coveredCount)
        {
            return coveredCount.Count == minTerms.Count();
        }
    }

    class Term
    {
        public Term()
        {
            minTerms = new List<int>();
        }
        public int Value { get; set; }
        public int DiffBit { get; set; }
        public bool IsCore { get; set; }
        public List<int> minTerms { get; set; }
    }
}
