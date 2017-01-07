using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAOD_Routing
{

    public static class BranchAndBoundAlgorytm
    {
        private static float Smin;
        public static int startMatrixSize;
        private static int[] bestRoute;

        public static int[] getFullMatrix(float[][] srcMatrix)
        {
            startMatrixSize = srcMatrix.Length;
            bestRoute = new int[startMatrixSize];
            Smin = float.PositiveInfinity;

            Node headNode = new Node(startMatrixSize, null);
            headNode.matrix = srcMatrix;
            List<Node> nodes = new List<Node>();
            nodes.Add(headNode);

            /*Пусть последовательный маршрут 1-2-3-4...-1 будет лучшим*/
            for (int i = 0; i < srcMatrix.Length; i++)
            {
                bestRoute[i] = i + 1;
            }
            Smin = 0;
            for (int i = 0; i < srcMatrix.Length-1; i++)
            {
                Smin += srcMatrix[i][i + 1];
                if (i == srcMatrix.Length - 2)
                    Smin += srcMatrix[i + 1][i-(srcMatrix.Length-2)];
            }

            while (nodes != null)
            {
                if (nodes.First().SecondRaiting < Smin)
                {
                    float[][] matrix = nodes.First().matrix;
                    /*Находим минимумы по строкам и вычитаем из матрицы*/
                    for (int i = 0; i < matrix.Length; i++)
                    {
                        float minimumInRow = matrix[i].Min();
                        nodes.First().FirstRaiting += minimumInRow;
                        for (int j = 0; j < matrix.Length; j++)
                        {
                            matrix[i][j] -= minimumInRow;
                        }
                    }
                    /*Находим минимумы по столбцам и вычитаем*/
                    for (int i = 0; i < nodes.First().matrix.Length; i++)
                    {
                        float minimumInColumn = nodes.First().GetMinInMatrixColumn(i);
                        nodes.First().FirstRaiting += minimumInColumn;
                        for (int j = 0; j < matrix.Length; j++)
                        {
                            matrix[j][i] -= minimumInColumn;
                        }
                    }
                    nodes.First().SecondRaiting = nodes.First().FirstRaiting;
                    /*Оценка нулевых элементов*/
                    float maxRaiting = 0;
                    for (int i = 0; i < matrix.Length; i++)
                    {
                        for (int j = 0; j < matrix.Length; j++)
                        {
                            if(matrix[i][j] == 0)
                            {
                                float minInRow = nodes.First().GetMinRowRaiting(j, i);
                                float minInColumn = nodes.First().GetMinColumnRaiting(i, j);
                                float sum = minInColumn + minInRow;
                                nodes.First().nullElements.Add(Tuple.Create(i, j, sum));
                            }
                        }
                    }
                    Tuple<int, int, float> tuple = nodes.First().nullElements.Max(x => x);
                    maxRaiting = tuple.Item3;
                    nodes.First().SecondRaiting += maxRaiting;
                    nodes.First().Route[tuple.Item1] = tuple.Item2; 

                    if (nodes.First().SecondRaiting < Smin)
                    {
                        if (nodes.First().SizeMatrix == 2)
                        {

                        }
                        else
                        {
                            float[][] newMatrix = new float[matrix.Length - 1][];
                            for (int i = 0; i < matrix.Length-1; i++)
                            {
                                newMatrix[i] = new float[matrix.Length];
                            }


                        }
                    }
                    else
                    {
                        nodes.Remove(nodes.First());
                    }
                }
                
            } 
            
            return bestRoute;
        }
    }

    class Node
    {
        public Node PrevNode { get; set; }
        public int SizeMatrix { get; set; }
        public float FirstRaiting { get; set; }
        public float SecondRaiting { get; set; }
        public float[][] matrix { get; set; }
        public int[] startTowns { get; set; }
        public int[] finishTowns { get; set; }
        public List<Tuple<int,int,float>> nullElements { get; set; }
        private List<int> nullUnitInRow { get; set; }
        private List<int> nullUnitInColumn { get; set; }
        private int[] minimumUnitInRow { get; set; }
        private int[] minimumUnitInColumn { get; set; }
        public int[] Route { get; set; }
        public Node(int size,Node prevNode)
        {
            this.SizeMatrix = size;
            matrix = new float[SizeMatrix][];
            for (int i = 0; i < SizeMatrix; i++)
            {
                matrix[i] = new float[SizeMatrix];
            }
            startTowns = new int[SizeMatrix];
            finishTowns = new int[SizeMatrix];
            nullUnitInRow = new List<int>();
            nullUnitInColumn = new List<int>();
            minimumUnitInRow = new int[SizeMatrix];
            minimumUnitInColumn = new int[SizeMatrix];
            Route = new int[BranchAndBoundAlgorytm.startMatrixSize];

            this.PrevNode = prevNode;
            FirstRaiting = 0;
            SecondRaiting = 0;
            for (int i = 0; i < startTowns.Length; i++)
            {
                startTowns[i] = i;
            }
            for (int i = 0; i < finishTowns.Length; i++)
            {
                finishTowns[i] = i;
            }
            nullElements = new List<Tuple<int, int, float>>();
        }

        public float GetMinInMatrixColumn(int columnIndex)
        {
            float minimum = float.PositiveInfinity;
            for (int j = 0; j < this.matrix.Length; j++)
            {
                if (matrix[j][columnIndex] < minimum)
                    minimum = matrix[j][columnIndex];
            }
            return minimum;
        }
        public float GetMinNotNullInMatrixColumn(int columnIndex)
        {
            float minimum = float.PositiveInfinity;
            for (int j = 0; j < this.matrix.Length; j++)
            {
                if (matrix[j][columnIndex] < minimum && matrix[j][columnIndex] != 0)
                    minimum = matrix[j][columnIndex];
            }
            return minimum;
        }
        public float GetMinRowRaiting(int colNumOfNull, int rowNum)
        {
            float minimum = float.PositiveInfinity;
            for (int j = 0; j < this.matrix.Length; j++)
            {
                if (j == colNumOfNull)
                    continue;
                if (matrix[rowNum][j] < minimum)
                    minimum = matrix[rowNum][j];
            }
            return minimum;
        }
        public float GetMinColumnRaiting(int rowNumOfNull, int colNum)
        {
            float minimum = float.PositiveInfinity;
            for (int i = 0; i < this.matrix.Length; i++)
            {
                if (i == rowNumOfNull)
                    continue;
                if (matrix[i][colNum] < minimum)
                    minimum = matrix[i][colNum];
            }
            return minimum;
        }
    }
}
