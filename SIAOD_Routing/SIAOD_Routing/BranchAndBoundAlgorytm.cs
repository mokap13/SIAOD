using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAOD_Routing
{

    class BranchAndBoundAlgorytm
    {
        private static float Smin;
        private static int startSize;
        private static int[] bestRoute;


        public static int[][] getFullMatrix(int[][] srcMatrix)
        {
            startSize = srcMatrix.Length;
            bestRoute = new int[startSize];
            return null;
        }
    }

    class Node
    {
        public Node PrevNode { get; set; }
        public int SizeMatrix { get; set; }
        public int FirstRaiting { get; set; }
        public int SecondRaiting { get; set; }
        public int[][] matrix { get; set; }
        public int[] startTowns { get; set; }
        public int[] finishTowns { get; set; }
        public int[] nullUnitInRow { get; set; }
        public int[] nullUnitInColumn { get; set; }
        public int[] minimumUnitInRow { get; set; }
        public int[] minimumUnitInColumn { get; set; }
        public Node()
        {
            matrix = new int[SizeMatrix][];
            for (int i = 0; i < SizeMatrix; i++)
            {
                matrix[i] = new int[SizeMatrix];
            }
            startTowns = new int[SizeMatrix];
            finishTowns = new int[SizeMatrix];
            nullUnitInRow = new int[SizeMatrix];
            nullUnitInColumn = new int[SizeMatrix];
            minimumUnitInRow = new int[SizeMatrix];
            minimumUnitInColumn = new int[SizeMatrix];
        }
    }
}
