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
        
        private static void ConsolePrintMatrix(Node node)
        {
            for (int i = 0; i < node.matrix.Count; i++)
            {
                Console.CursorLeft = i*4 + 4;
                Console.Write(node.finishTowns[i]+1);
            }
            Console.WriteLine();
            for (int i = 0; i < node.matrix.Count; i++)
            {
                Console.Write(node.startTowns[i]+1);
                for (int j = 0; j < node.matrix.Count; j++)
                {
                    Console.CursorLeft = j*4+4;
                    Console.Write(node.matrix[i][j]);
                    
                }
                Console.WriteLine();
            }
            Console.WriteLine(node.SecondRaiting);
            Console.WriteLine(new String('*', node.matrix.Count*4));
            Console.WriteLine();
        }

        public static List<int> getFullMatrix(float[][] srcMatrix)
        {
            #region beforeWhile

            startMatrixSize = srcMatrix.Length;
            bestRoute = new List<int>();
            Smin = float.PositiveInfinity;

            Node headNode = new Node();
            /*Преобразуем матрицу в двумерный список*/
            for (int i = 0; i < srcMatrix.Length; i++)
            {
                headNode.matrix.Add(new List<float>());
                for (int j = 0; j < srcMatrix.Length; j++)
                {
                    headNode.matrix[i].Add(srcMatrix[i][j]);
                }
            }
            List<Node> nodes = new List<Node>();
            /*Массив начальных и конечных городов (названия городов)*/
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

            List<List<float>> matrix = nodes.First().matrix;

            #region Вычет по строкам и столбцам

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
            #endregion
            while (nodes != null)
            {
                
                Tuple<int, int, float> tuple = Tuple.Create(0,0,0f);
             
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

                tuple = nodes.First().nullElements.Where(x => x.Item3 == nodes.First().nullElements.Max(y => y.Item3)).First();
                
                nodes.First().SecondRaiting = nodes.First().FirstRaiting;
                //nodes.First().Route[tuple.Item1] = tuple.Item2;

                matrix[tuple.Item1][tuple.Item2] = float.PositiveInfinity;

                #region Вычет по строкам и столбцам

                /*Находим минимумы по строкам и вычитаем из матрицы*/
                for (int i = 0; i < matrix.Count; i++)
                {
                    float minimumInRow = matrix[i].Min();
                    nodes.First().SecondRaiting += minimumInRow;
                    for (int j = 0; j < matrix.Count; j++)
                    {
                        matrix[i][j] -= minimumInRow;
                    }
                }
                /*Находим минимумы по столбцам и вычитаем*/
                for (int i = 0; i < nodes.First().matrix.Count; i++)
                {
                    float minimumInColumn = nodes.First().GetMinInMatrixColumn(i);
                    nodes.First().SecondRaiting += minimumInColumn;
                    for (int j = 0; j < matrix.Count; j++)
                    {
                        matrix[j][i] -= minimumInColumn;
                    }
                }
                #endregion

                ConsolePrintMatrix(nodes.First());

                /*Удаление ребра из матрицы(Включение маршрута)*/
                List<int> newStartTowns = nodes.First().startTowns.Where(x => x != tuple.Item1).ToList();
                List<int> newFinishTowns = nodes.First().finishTowns.Where(x => x != tuple.Item2).ToList();
                //List<List<float>> newMatrix = matrix
                //    .Where(x => x != matrix[tuple.Item1])
                //    .Select(x => x.Where(y => y != x[tuple.Item2]).ToList())
                //    .ToList();

                #region Удаление Ребра
                List<List<float>> newMatrix = new List<List<float>>();
                for (int i = 0; i < matrix.Count; i++)
                {
                    newMatrix.Add(new List<float>());
                }
                for (int i = 0; i < matrix.Count; i++)
                {
                    for (int j = 0; j < matrix.Count; j++)
                    {
                        newMatrix[i].Add(matrix[i][j]);
                    }
                }

                for (int i = 0; i < newMatrix.Count; i++)
                {
                    newMatrix[i].RemoveAt(tuple.Item2);
                }
                newMatrix.RemoveAt(tuple.Item1); 
                #endregion

                /*Присваиваем бесконечность обратному пути т.е если маршрут 1-2 то 2-1 не существует*/
                int startTownIndex = newStartTowns.FindIndex(x => x == tuple.Item2);
                int finishTownIndex = newFinishTowns.FindIndex(x => x == tuple.Item1);

                if (startTownIndex != -1 && finishTownIndex != -1)
                    newMatrix[startTownIndex][finishTownIndex] = float.PositiveInfinity;

                float newRaiting = nodes.First().FirstRaiting;

                if (nodes.Count == 2)
                {
                    if (nodes.First().FirstRaiting < nodes.Last().SecondRaiting)
                        nodes.Remove(nodes.Last());
                }

                nodes.Insert(0, new Node()
                {
                    startTowns = newStartTowns,
                    finishTowns = newFinishTowns,
                    matrix = newMatrix,
                    FirstRaiting = newRaiting
                });                
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
