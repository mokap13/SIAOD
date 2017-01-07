using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// Реализация алгоритма Дейкстры. Содержит матрицу смежности в виде массивов вершин и ребер
/// </summary>
class DekstraAlgorim
{
    public List<Town> Towns { get; private set; }
    public List<Road> Roads { get; private set; }
    public Town StartTown { get; private set; }

    public DekstraAlgorim(List<Town> towns, List<Road> roads)
    {
        this.Towns = towns;
        this.Roads = roads;
    }
    /// <summary>
    /// Запуск алгоритма расчета
    /// </summary>
    /// <param name="startTown"></param>
    public void AlgoritmRun(Town startTown)
    {
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
                float newValue = startTown.ValueMetka + GetRoad(nextTown, startTown).Weight;
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
        List<Road> roads = new List<Road>();
        roads = GetMinimumRoad(startTown);
        if(roads.Count != 0)
            Roads.Add(new Road(roads));
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
        IEnumerable<Road> road = from r in Roads where  (r.SecondTown == firstTown && r.FirstTown == secondTown) select r;
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

    public float[,] GetFullMatrix(List<Town> towns)
    {
        int size = towns.Count;
        float[,] matrix = new float[size, size];
        for (int i = 0; i < size; i++)
        {
            AlgoritmRun(towns[i]);
            for (int j = 0; j < size; j++)
            {
                matrix[i, j] = GetMinimumRoad(towns[j])[;
            }
        }
        return matrix;
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
            roads.Add(GetRoad(tempTown.prevTown, tempTown));
            tempTown = tempTown.prevTown;
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
        this.Weight = FirstTown.ValueMetka;
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
        ValueMetka = int.MaxValue;
        IsChecked = false;
        Name = name;
        prevTown = new Town();
    }
    public Town()
    {
    }
}

// <summary>
/// для печати графа
/// </summary>
static class PrintGrath
{
    public static List<string> PrintAllTowns(DekstraAlgorim da)
    {
        List<string> retListOfPoints = new List<string>();
        foreach (Town town in da.Towns)
        {
            retListOfPoints.Add(string.Format($"point name={town.Name}, point value={town.ValueMetka}, predok={town.prevTown.Name ?? "нет предка"}"));
        }
        return retListOfPoints;
    }
    public static List<string> PrintAllMinRoads(DekstraAlgorim da)
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

    public static List<string> PrintRoads(DekstraAlgorim da)
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
}

class DekstraException : ApplicationException
{
    public DekstraException(string message) : base(message)
    {
    }
}

public class DeikstraAlgorytm
{
    public static void Execute()
    {
        List<Town> towns = new List<Town>()
        {
            new Town("1"),
            new Town("2"),
            new Town("3"),
            new Town("4"),
            new Town("5"),
            new Town("6"),
            new Town("7")
        };
        
        List<Road> roads = new List<Road>() {
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
        DekstraAlgorim da = new DekstraAlgorim(towns, roads);
        da.AlgoritmRun(towns[6]);
        
        List<string> b = PrintGrath.PrintRoads(da);
        for (int i = 0; i < b.Count; i++)
            Console.WriteLine(b[i]);
        Console.ReadKey(true);
    }
}