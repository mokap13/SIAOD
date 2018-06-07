using System;

namespace OS_lab_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int.TryParse(Console.ReadLine(), out int x);
            int.TryParse(Console.ReadLine(), out int y);

            Console.WriteLine($"before swap: x = {x} \t y = {y}");
            Swap(ref x, ref y);
            Console.WriteLine($"after swap: x = {x} \t y = {y}");
            Console.Read();
        }
        static void Swap(ref int x, ref int y)
        {
            int temp;
            temp = x;
            x = y;
            y = temp;
        }
    }
}
