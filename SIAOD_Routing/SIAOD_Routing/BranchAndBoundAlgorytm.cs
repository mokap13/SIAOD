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
        private static List<int> firstBestRoute;
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
            #region Инициализация перед циклом

            startMatrixSize = srcMatrix.Length;
            firstBestRoute = new List<int>();
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
                firstBestRoute.Add(i + 1);
                bestRoute.Add(-1);
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

            nodes.First().FirstRaiting += ReductMatrix(nodes, matrix);

            while (nodes != null)
            {
                if (nodes.Count == 2)
                {
                    if (nodes.First().SecondRaiting <= nodes.Last().SecondRaiting)
                    {
                        nodes.Remove(nodes.Last());
                    }
                    else
                    {
                        nodes.Remove(nodes.First());
                    }
                    nodes.First().FirstRaiting = nodes.First().SecondRaiting;
                }
                if (Smin <= nodes.First().SecondRaiting)
                {
                    //Console.WriteLine($"firstBestRoute {Smin}");
                    return firstBestRoute;
                }

                #region Если в матрица 2х2 то распредялем оставшиеся пути и завершаем алогритм
                if (nodes.First().matrix.Count == 2)
                {
                    for (int i = 0; i < nodes.First().matrix.Count; i++)
                    {
                        for (int j = 0; j < nodes.First().matrix.Count; j++)
                        {
                            if (matrix[i][j] == 0)
                            {
                                bestRoute[nodes.First().startTowns[i]] = nodes.First().finishTowns[j];
                                nodes.First().startTowns.RemoveAt(i);
                                nodes.First().finishTowns.RemoveAt(j);
                                bestRoute[nodes.First().startTowns.First()] = nodes.First().finishTowns.First();

                                Console.WriteLine(new String('-', 50));

                                for (int k = 0; k < bestRoute.Count; k++)
                                {
                                    Console.Write($"{k + 1}->{bestRoute[k] + 1}, ");
                                }
                                Console.WriteLine();
                                Console.WriteLine($"{nodes.First().SecondRaiting.ToString()}");

                                return bestRoute;
                            }
                        }
                    }
                    break;
                } 
                #endregion

                Tuple<int, int, float> nullElement = Tuple.Create(0, 0, 0f);

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

                /*находим нулевой элемент */
                nullElement = nodes.First().nullElements.Where(x => x.Item3 == nodes.First().nullElements.Max(y => y.Item3)).First();
                /*добавляем новую дорогу в общий путь*/
                bestRoute[nodes.First().startTowns[nullElement.Item1]] = nodes.First().finishTowns[nullElement.Item2];
                /*присваиваем бесконечность*/
                matrix[nullElement.Item1][nullElement.Item2] = float.PositiveInfinity;

                nodes.First().SecondRaiting = nodes.First().FirstRaiting;

                //ConsolePrintMatrix(nodes.First());

                List<List<float>> newMatrix = CreateNewMatrix(matrix);

                nodes.First().SecondRaiting += ReductMatrix(nodes, matrix);

                /*Удаление ребра из матрицы(Включение маршрута)*/
                List<int> newStartTowns = nodes.First().startTowns.Where(x => x != nodes.First().startTowns.ElementAt(nullElement.Item1)).ToList();
                List<int> newFinishTowns = nodes.First().finishTowns.Where(x => x != nodes.First().finishTowns.ElementAt(nullElement.Item2)).ToList();

                /*Удаление*/
                DeletePath(nullElement, newMatrix);

                SetInfinity(newMatrix);

                float newRaiting = nodes.First().FirstRaiting;

                newRaiting += ReductMatrix(nodes, newMatrix);

                nodes.Insert(0, new Node()
                {
                    startTowns = newStartTowns,
                    finishTowns = newFinishTowns,
                    matrix = newMatrix,
                    FirstRaiting = nodes.First().FirstRaiting,
                    SecondRaiting = newRaiting
                });

                for (int i = 0; i < bestRoute.Count; i++)
                {
                    //Console.Write($"{i + 1}->{bestRoute[i] + 1}, ");
                }
                //Console.WriteLine();
            }

            return bestRoute;
        }

        private static List<List<float>> CreateNewMatrix(List<List<float>> matrix)
        {
            List<List<float>> newMatrix = new List<List<float>>();
            /*Инициализация*/
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

            return newMatrix;
        }

        private static void SetInfinity(List<List<float>> newMatrix)
        {
            int I = newMatrix.FindIndex(x => !x.Contains(float.PositiveInfinity));
            if (I != -1)
            {
                for (int i = 0; i < newMatrix.Count; i++)
                {
                    for (int j = 0; j < newMatrix.Count; j++)
                    {
                        if (newMatrix[j][i] == float.PositiveInfinity)
                            break;

                        if (j == newMatrix.Count - 1)
                            newMatrix[I][i] = float.PositiveInfinity;
                    }
                }
            }
        }

        private static void DeletePath(Tuple<int, int, float> tuple, List<List<float>> newMatrix)
        {
            for (int i = 0; i < newMatrix.Count; i++)
            {
                newMatrix[i].RemoveAt(tuple.Item2);
            }
            newMatrix.RemoveAt(tuple.Item1);
        }

        private static float ReductMatrix(List<Node> nodes, List<List<float>> matrix)
        {
            float sum = 0;
            /*Находим минимумы по строкам и вычитаем из матрицы*/
            for (int i = 0; i < matrix.Count; i++)
            {
                float minimumInRow = matrix[i].Min();
                sum += minimumInRow;
                for (int j = 0; j < matrix.Count; j++)
                {
                    matrix[i][j] -= minimumInRow;
                }
            }
            /*Находим минимумы по столбцам и вычитаем*/
            for (int i = 0; i < matrix.Count; i++)
            {
                float minimumInColumn = nodes.First().GetMinInMatrixColumn(i);
                sum += minimumInColumn;
                for (int j = 0; j < matrix.Count; j++)
                {
                    matrix[j][i] -= minimumInColumn;
                }
            }
            return sum;
        }
    }

    class Node
    {
        public Node PrevNode { get; set; }
        public float FirstRaiting { get; set; }
        public float SecondRaiting { get; set; }
        public List<List<float>> matrix { get; set; }
        public List<int> startTowns { get; set; }
        public List<int> finishTowns { get; set; }
        public List<Tuple<int,int,float>> nullElements { get; set; }

        public Node()
        {
            
            matrix = new List<List<float>>();
            
            startTowns = new List<int>();
            finishTowns = new List<int>();
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
