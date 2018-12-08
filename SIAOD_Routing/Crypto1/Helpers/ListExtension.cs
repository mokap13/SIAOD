using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto1.Helpers
{
    public static class ListExtension
    {
        public static List<T> MoveRange<T>(this List<T> list, T firstElement, int index)
        {
            var tempList = list.ToList();
            int indexFirstElement = tempList.IndexOf(firstElement);
            List<T> range = tempList.GetRange(indexFirstElement, list.Count - indexFirstElement);
            tempList.RemoveRange(indexFirstElement, list.Count - indexFirstElement);
            tempList.InsertRange(index, range);

            return tempList;
        }

        //public static void Swap<T>(this List<T> list, int i, int j)
        //{
        //    var elem1 = list[i];
        //    var elem2 = list[j];

        //    list[i] = elem2;
        //    list[j] = elem1;
        //}
    }
}
