using ConsoleApplication3.ComponentOfVehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            IWarehouse warehouse = new Warehouse("Склад");
            Component engine = new DizelEngine("Двигатель");
            
            
            Component shtanga = new CompoziteComponent("Штанга");
            Component koromislo = new CompoziteComponent("Коромысло");
            Component klapan = new CompoziteComponent("Клапан");
            klapan.Add(new StrongScrew("Шуруп"),6);
            Component golovkaCilindra = new CompoziteComponent("Головка Цилиндра");
            engine.Add(shtanga);
            engine.Add(koromislo);
            engine.Add(klapan,2);
            engine.Add(golovkaCilindra);
            engine.Add(new StrongScrew("Шуруп"), 20);
            warehouse.Add(engine,2);
            warehouse.Add(koromislo, shtanga, klapan, golovkaCilindra);

            Console.WriteLine("Создать новую деталь");
            Console.WriteLine("Просмотр существующих деталей");
            warehouse.Display();
            Console.ReadLine();
        }
    }
}
