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
        private static List<int> bestRoute;

        public static List<int> getFullMatrix(float[][] srcMatrix)
        {
            #region beforWhile

            startMatrixSize = srcMatrix.Length;
            bestRoute = new List<int>();
            Smin = float.PositiveInfinity;

            Node headNode = new Node();

            for (int i = 0; i < srcMatrix.Length; i++)
            {
                headNode.matrix.Add(new List<float>());
                for (int j = 0; j < srcMatrix.Length; j++)
                {
                    headNode.matrix[i].Add(srcMatrix[i][j]);
                }
            }
            List<Node> nodes = new List<Node>();
            for (int i = 0; i < srcMatrix.Length; i++)
            {
                headNode.startTowns.Add(i);
            }
            for (int i = 0; i < srcMatrix.Length; i++)
            {
                headNode.finishTowns.Add(i);
            }

            nodes.Add(headNode);

            /*Пусть последовательный маршрут 1-2-3-4...-1 будет лучшим*/
            for (int i = 0; i < srcMatrix.Length; i++)
            {
                bestRoute.Add(i + 1);
            }
            Smin = 0;
            for (int i = 0; i < srcMatrix.Length - 1; i++)
            {
                Smin += srcMatrix[i][i + 1];
                if (i == srcMatrix.Length - 2)
                    Smin += srcMatrix[i + 1][i - (srcMatrix.Length - 2)];
            }

           
            
            #endregion

            while (nodes != null)
            {
                List<List<float>> matrix = nodes.First().matrix;
                /*Находим минимумы по строкам и вычитаем из матрицы*/
                for (int i = 0; i < matrix.Count; i++)
                {
                    float minimumInRow = matrix[i].Min();
                    nodes.First().FirstRaiting += minimumInRow;
                    for (int j = 0; j < matrix.Count; j++)
                    {
                        matrix[i][j] -= minimumInRow;
                    }
                }
                /*Находим минимумы по столбцам и вычитаем*/
                for (int i = 0; i < nodes.First().matrix.Count; i++)
                {
                    float minimumInColumn = nodes.First().GetMinInMatrixColumn(i);
                    nodes.First().FirstRaiting += minimumInColumn;
                    for (int j = 0; j < matrix.Count; j++)
                    {
                        matrix[j][i] -= minimumInColumn;
                    }
                }

                if (nodes.First().SecondRaiting < Smin)
                {

                    #region Редукция и оценка матрицы
                    matrix = nodes.First().matrix;
                   
                    /*Оценка нулевых элементов*/
                    for (int i = 0; i < matrix.Count; i++)
                    {
                        for (int j = 0; j < matrix.Count; j++)
                        {
                            if (matrix[i][j] == 0)
                            {
                                float minInRow = nodes.First().GetMinRowRaiting(j, i);
                                float minInColumn = nodes.First().GetMinColumnRaiting(i, j);
                                float sum = minInColumn + minInRow;
                                nodes.First().nullElements.Add(Tuple.Create(i, j, sum));
                            }
                        }
                    }
                    #endregion

                    Tuple<int, int, float> tuple = nodes.First().nullElements.Where(x => x.Item3 == nodes.First().nullElements.Max(y => y.Item3)).First();
                    nodes.First().SecondRaiting = tuple.Item3 + nodes.First().FirstRaiting;
                    nodes.First().Route[tuple.Item1] = tuple.Item2;
                    
                    if (nodes.First().SecondRaiting < Smin)
                    {
                        if (nodes.First().matrix.Count == 2)
                        {
                            
                        }
                        else if(nodes.Count == 2)
                        {
                            List<List<float>> newMatrix2 = new List<List<float>>();
                            for (int i = 0; i < matrix.Count - 1; i++)
                            {
                                newMatrix2.Add(new List<float>());
                            }
                            /*Удаление ребра из матрицы*/
                            List<int> newStartTowns = nodes.First().startTowns.Where(x => x != tuple.Item1).ToList();
                            List<int> newFinishTowns = nodes.First().finishTowns.Where(x => x != tuple.Item2).ToList();

                            List<List<float>> newMatrix = matrix
                                .Where(x => x != matrix[tuple.Item1])
                                .Select(x => x.Where(y => y != x[tuple.Item2]).ToList())
                                .ToList();
                            /*Присваиваем бесконечность обратному пути т.е если маршрут 1-2 то 2-1 не существует*/
                            int finishTownIndex = newStartTowns.FindIndex(x => x == tuple.Item2);
                            int startTownIndex = newFinishTowns.FindIndex(x => x == tuple.Item1);
                            
                            if(newMatrix.Count > startTownIndex || newMatrix.Count > finishTownIndex)
                                newMatrix[startTownIndex][finishTownIndex] = float.PositiveInfinity;
                           
                            float newRaiting = nodes.First().FirstRaiting;
                            
                            nodes.Insert(0, new Node()
                            {
                                startTowns = newStartTowns,
                                finishTowns = newFinishTowns,
                                matrix = newMatrix,
                                FirstRaiting = newRaiting
                            });
                            nodes.Remove(nodes.Last());
                        }
                        else
                        {

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
        //public int SizeMatrix { get; set; }
        public float FirstRaiting { get; set; }
        public float SecondRaiting { get; set; }
        public List<List<float>> matrix { get; set; }
        public List<int> startTowns { get; set; }
        public List<int> finishTowns { get; set; }
        public List<Tuple<int,int,float>> nullElements { get; set; }
        //private List<int> nullUnitInRow { get; set; }
        //private List<int> nullUnitInColumn { get; set; }
        //private int[] minimumUnitInRow { get; set; }
        //private int[] minimumUnitInColumn { get; set; }
        public int[] Route { get; set; }
        public Node()
        {
            
            matrix = new List<List<float>>();
            
            startTowns = new List<int>();
            finishTowns = new List<int>();
            //nullUnitInRow = new List<int>();
            //nullUnitInColumn = new List<int>();
            
            Route = new int[BranchAndBoundAlgorytm.startMatrixSize];

            //this.PrevNode = prevNode;
            FirstRaiting = 0;
            SecondRaiting = 0;
            
            nullElements = new List<Tuple<int, int, float>>();
        }

        public float GetMinInMatrixColumn(int columnIndex)
        {
            float minimum = float.PositiveInfinity;
            for (int j = 0; j < this.matrix.Count; j++)
            {
                if (matrix[j][columnIndex] < minimum)
                    minimum = matrix[j][columnIndex];
            }
            return minimum;
        }
        public float GetMinNotNullInMatrixColumn(int columnIndex)
        {
            float minimum = float.PositiveInfinity;
            for (int j = 0; j < this.matrix.Count; j++)
            {
                if (matrix[j][columnIndex] < minimum && matrix[j][columnIndex] != 0)
                    minimum = matrix[j][columnIndex];
            }
            return minimum;
        }
        public float GetMinRowRaiting(int colNumOfNull, int rowNum)
        {
            float minimum = float.PositiveInfinity;
            for (int j = 0; j < this.matrix.Count; j++)
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
            for (int i = 0; i < this.matrix.Count; i++)
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
