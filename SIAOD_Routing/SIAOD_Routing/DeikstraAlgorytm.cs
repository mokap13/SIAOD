using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// Реализация алгоритма Дейкстры. Содержит матрицу смежности в виде массивов вершин и ребер
/// </summary>
class DekstraAlgoritm
{
    public List<Town> Towns { get; private set; }
    public List<Road> Roads { get; private set; }
    public List<Road> newRoads { get; private set; }
    public Town StartTown { get; private set; }

    public DekstraAlgoritm(List<Town> towns, List<Road> roads)
    {   
        this.Towns = towns;
        this.Roads = roads;
        newRoads = new List<Road>();
    }
    /// <summary>
    /// Запуск алгоритма расчета
    /// </summary>
    /// <param name="startTown"></param>
    public void AlgoritmRun(Town startTown)
    {
        foreach (var town in this.Towns)
        {
            town.IsChecked = false;
            town.ValueMetka = float.PositiveInfinity;
        }
        startTown.ValueMetka = 0;

        if (this.Towns.Count() == 0 || this.Roads.Count() == 0)
        {
            throw new DekstraException("Массив вершин или ребер не задан!");
        }
        else
        {
            StartTown = startTown;
            OneStep(startTown);
            foreach (Town town in Towns)
            {
                Town anotherTown = GetAnotherUncheckedPoint();
                if (anotherTown != null)
                {
                    OneStep(anotherTown);
                }
                else
                {
                    break;
                }

            }
        }

    }
    /// <summary>
    /// Метод, делающий один шаг алгоритма. Принимает на вход вершину
    /// </summary>
    /// <param name="startTown"></param>
    public void OneStep(Town startTown)
    {
        foreach (Town nextTown in Pred(startTown))
        {
            if (nextTown.IsChecked == false)//не отмечена
            {
                float newValue = startTown.ValueMetka + GetRoad(startTown,nextTown).Weight;
                if (nextTown.ValueMetka > newValue)
                {
                    nextTown.ValueMetka = newValue;
                    nextTown.prevTown = startTown;
                }
                else
                {

                }
            }
        }
        //List<Road> roads = new List<Road>();
        //roads = GetMinimumRoad(startTown);
        //if (roads.Count != 0)
        //    CompoziteRoads.Add(new Road(roads));

        startTown.IsChecked = true;//вычеркиваем
    }
    /// <summary>
    /// Поиск соседей для вершины. Для неориентированного графа ищутся все соседи.
    /// </summary>
    /// <param name="currentTown"></param>
    /// <returns></returns>
    private IEnumerable<Town> Pred(Town currentTown)
    {
        IEnumerable<Town> firstTown = 
            from ft in Roads where ft.FirstTown == currentTown select ft.SecondTown;
        //IEnumerable<Town> secondTown = 
        //    from st in Roads where st.SecondTown == currentTown select st.FirstTown;
        //IEnumerable<Town> totalTowns = firstTown.Concat(secondTown);
        return firstTown;
    }
    /// <summary>
    /// Получаем ребро, соединяющее 2 входные точки
    /// </summary>
    /// <param name="firstTown"></param>
    /// <param name="secondTown"></param>
    /// <returns></returns>
    private Road GetRoad(Town firstTown, Town secondTown)
    {//ищем ребро по 2 точкам
        List<Road> road = Roads.Where(x => x.FirstTown == firstTown && x.SecondTown == secondTown).ToList();
        if (road.Count() == 0)
        {
            throw new DekstraException("Не найдено ребро между соседями!");
        }
        else
        {
            return road.First();
        }
    }
    /// <summary>
    /// Получаем очередную неотмеченную вершину, "ближайшую" к заданной.
    /// </summary>
    /// <returns></returns>
    private Town GetAnotherUncheckedPoint()
    {
        IEnumerable<Town> pointsuncheck = from p in Towns where p.IsChecked == false select p;
        if (pointsuncheck.Count() != 0)
        {
            float minVal = pointsuncheck.First().ValueMetka;
            Town minPoint = pointsuncheck.First();
            foreach (Town p in pointsuncheck)
            {
                if (p.ValueMetka < minVal)
                {
                    minVal = p.ValueMetka;
                    minPoint = p;
                }
            }
            return minPoint;
        }
        else
        {
            return null;
        }
    }

    public List<Town> MinPath1(Town endTown)
    {
        List<Town> towns = new List<Town>();
        Town tempTown = new Town();
        tempTown = endTown;
        while (tempTown != this.StartTown)
        {
            towns.Add(tempTown);
            tempTown = tempTown.prevTown;
        }

        return towns;
    }

    public List<Road> GetMinimumRoad(Town endTown)
    {
        List<Road> roads = new List<Road>();
        Town tempTown = new Town();
        tempTown = endTown;
        while (tempTown != this.StartTown)
        {
            Road tempRoad = GetRoad(tempTown.prevTown, tempTown);
            tempTown = tempTown.prevTown;
        }
        
        return roads;
    }

    public float[][] GetFullMatrix(List<Town> towns)
    {
        newRoads.Clear();

        int size = towns.Count;
        float[][] matrix = new float[size][];
        for (int i = 0; i < matrix.Length; i++)
        {
            matrix[i] = new float[size];
        }
        List<Town> minPath;
        for (int i = 0; i < size; i++)
        {
            AlgoritmRun(towns[i]);
            //List<string> str = PrintGrath.PrintAllMinRoads(this);
            List<Road> roads = GetRoads();
            newRoads.AddRange(roads);
            for (int j = 0; j < size; j++)
            {
                minPath = this.MinPath1(towns[j]);
                if (minPath.Count == 0)
                    matrix[i][j] = float.PositiveInfinity;
                else
                    matrix[i][j] = minPath.First().ValueMetka;
            }
  //          List<Road> roads = PrintGrath.GetMinRoads(this);
        }
        return matrix;
    }

    public List<Road> GetRoads()
    {
        List<Road> roads = new List<Road>();
        List<Road> allRoads = new List<Road>();
        Town tempTown = StartTown;
        foreach (Town townFinish in this.Towns)
        {
            if (townFinish != this.StartTown)
            {
                List<Town> towns = new List<Town>();
                towns.AddRange(this.MinPath1(townFinish));
                towns.Add(StartTown);
                roads.Add(new Road(towns
                    .Where(x=>x!=towns.Last())
                    .Select(x => GetRoad(x.prevTown, x)).Reverse()
                    .ToList()));
            }
        }
        return roads;
    }
}

/// <summary>
/// Класс, реализующий ребро
/// </summary>
class Road
{
    public Town FirstTown { get; private set; }
    public Town SecondTown { get; private set; }
    public float Weight { get; private set; }
    public List<Road> compoziteRoad { get; set; }
    public Road(Town first, Town second, float valueOfWeight)
    {
        this.FirstTown = first;
        this.SecondTown = second;
        this.Weight = valueOfWeight;
    }
    public Road(List<Road> compoziteRoad)
    {
        this.compoziteRoad = compoziteRoad;
        this.FirstTown = compoziteRoad.First().FirstTown;
        this.SecondTown = compoziteRoad.Last().SecondTown;
        this.Weight = compoziteRoad.Sum(x=>x.Weight);
    }
}

/// <summary>
/// Класс, реализующий вершину графа
/// </summary>
class Town
{
    public float ValueMetka { get; set; }
    public string Name { get; private set; }
    public bool IsChecked { get; set; }
    public Town prevTown { get; set; }
    public Town(int value, bool ischecked)
    {
        ValueMetka = value;
        IsChecked = ischecked;
        prevTown = new Town();
    }
    public Town(string name)
    {
        ValueMetka = float.PositiveInfinity;
        IsChecked = false;
        Name = name;
        prevTown = new Town();
    }
    public Town()
    {
    }

    public override string ToString()
    {
        return this.Name;
    }
}

// <summary>
/// для печати графа
/// </summary>
static class PrintGrath
{
    public static List<string> PrintAllTowns(DekstraAlgoritm da)
    {
        List<string> retListOfPoints = new List<string>();
        foreach (Town town in da.Towns)
        {
            retListOfPoints.Add(string.Format($"point name={town.Name}, point value={town.ValueMetka}, predok={town.prevTown.Name ?? "нет предка"}"));
        }
        return retListOfPoints;
    }
    public static List<string> PrintAllMinRoads(DekstraAlgoritm da)
    {
        List<string> retListOfPointsAndPaths = new List<string>();
        foreach (Town town in da.Towns)
        {
            if (town != da.StartTown)
            {         
                string str = string.Empty;
                foreach (Town town1 in da.MinPath1(town))
                {
                    str += $"{town1.Name} ";
                }
                retListOfPointsAndPaths.Add($"Point ={town.Name},MinPath from {da.StartTown.Name} = {str} *** {town.ValueMetka}");
            }
        }
        return retListOfPointsAndPaths;
    }
    
    public static List<string> PrintRoads(DekstraAlgoritm da)
    {
        List<string> retListOfPointsAndPaths = new List<string>();
        foreach (Town town in da.Towns)
        {
            if (town != da.StartTown)
            {
                string str = string.Empty;
                foreach (Road road in da.GetMinimumRoad(town))
                {
                    str += $"{road.SecondTown.Name} ";
                }
                retListOfPointsAndPaths.Add($"Point ={town.Name},MinPath from {da.StartTown.Name} = {str} *** {town.ValueMetka}");
            }
        }
        return retListOfPointsAndPaths;
    }
    public static float[] GetRowMinRoads(List<Town> towns,DekstraAlgoritm da)
    {
        int size = towns.Count;
        float[] row = new float[size];
        for (int i = 0; i < size; i++)
        {
            if (towns[i].Name != da.Towns[i].Name)
                continue;
            if (da.Towns[i] != da.StartTown)
            {
                row[i] = da.Towns[i].ValueMetka;
            }
            else
            {
                row[i] = float.PositiveInfinity;
            }
        }

        return row;
    }
    public static void PrintMatrix(float[][] matrix)
    {
        for (int i = 0; i < matrix.Length; i++)
        {
            for (int j = 0; j < matrix.Length; j++)
            {
                if (matrix[i][j] == float.PositiveInfinity)
                    Console.Write("M");
                else
                    Console.Write(matrix[i][j]);
                Console.Write("  ");
            }
            Console.WriteLine();
        }
    }
    public static List<Road> GetMinRoads(DekstraAlgoritm da)
    {
        List<Road> roads = new List<Road>();
        List<Road> allRoads = new List<Road>();
        foreach (Town town in da.Towns)
        {
            if (town != da.StartTown)
            {
                foreach (Road road in da.GetMinimumRoad(town))
                {
                    roads.Add(road);      
                }
            }
            if(roads.Count != 0)
            {
                Road compoziteRoad = new Road(roads);
                allRoads.Add(compoziteRoad);
                roads = new List<Road>();
            }
            
        }
        return allRoads;
    } 
}

class DekstraException : ApplicationException
{
    public DekstraException(string message) : base(message)
    {
    }
}

public class DeikstraAlgorytm
{
    private static List<Town> towns;
    private static List<Road> roads;

    private static void Init()
    {
        towns = new List<Town>()
        {
            new Town("1"),
            new Town("2"),
            new Town("3"),
            new Town("4"),
            new Town("5"),
            new Town("6"),
            new Town("7")
        };

        roads = new List<Road>() {
            new Road(towns[0],towns[1],12),
            new Road(towns[0],towns[3],28),

            new Road(towns[1],towns[0],12),
            new Road(towns[1],towns[2],10),
            new Road(towns[1],towns[3],43),

            new Road(towns[2],towns[1],10),
            new Road(towns[2],towns[4],10),

            new Road(towns[3],towns[0],28),
            new Road(towns[3],towns[1],43),
            new Road(towns[3],towns[2],17),

            new Road(towns[4],towns[1],31),
            new Road(towns[4],towns[2],10),
            new Road(towns[4],towns[5],8),

            new Road(towns[5],towns[4],14),
            new Road(towns[5],towns[6],6),

            new Road(towns[6],towns[5],6)
        };
    }
    public static void Execute()
    {
        Init();
        
        DekstraAlgoritm da = new DekstraAlgoritm(towns, roads);

        float[][] matrix = da.GetFullMatrix(towns);
        PrintGrath.PrintMatrix(matrix);

        Console.ReadKey(true);
    }
}