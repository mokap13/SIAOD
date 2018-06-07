using System;

namespace PatternAdapter
{
    class Program
    {
        static void Main(string[] args)
        {
            // Человек
            Person person = new Person();
            // Стандартная база данных с которой раньше работали
            StandartDataBase standatdDb = new StandartDataBase();

            Console.WriteLine("Сохранение в стандартную БД: ");
            person.SaveData(standatdDb);
            Console.WriteLine();
           
            SpecificDataBase specificDb = new SpecificDataBase();
           
            IStandartDb standartDb = new SpecificToStandartAdapter(specificDb);

            Console.WriteLine("Сохранение в специфическую БД через объект адаптер: ");
            person.SaveData(standartDb);
            Console.WriteLine();
            Console.Read();
        }
    }
    class Person
    {
        private string name = "Иван";
        private string surName = "Иванович";
        public void SaveData(IStandartDb standartDb)
        {
            standartDb.SaveAllName(name + " " + surName);
        }
    }
    //Стандартная БД требует имя и фамилию в одно поле
    interface IStandartDb
    {
        void SaveAllName(string allName);
    }
    class StandartDataBase : IStandartDb
    {
        public void SaveAllName(string allName)
        {
            Console.WriteLine(allName);
        }
    }

    //Стандартная БД требует имя и фамилию в 2 разных поля
    interface ISpecificDb
    {
        void SaveName(string name);
        void SaveSurName(string surName);
    }
    class SpecificDataBase : ISpecificDb
    {
        public void SaveName(string name)
        {
            Console.WriteLine(name);
        }

        public void SaveSurName(string surName)
        {
            Console.WriteLine(surName);
        }
    }
    
    class SpecificToStandartAdapter : IStandartDb
    {
        SpecificDataBase specificDb;
        public SpecificToStandartAdapter(SpecificDataBase c)
        {
            specificDb = c;
        }

        public void SaveAllName(string allName)
        {
            specificDb.SaveName("***** " + allName.Split(' ')[0] + " *****");
            specificDb.SaveSurName("***** " + allName.Split(' ')[1] + " *****");
        }
    }
}
