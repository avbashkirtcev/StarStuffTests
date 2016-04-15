using StarStuff.avb.Test1;
using StarStuff.avb.Test2;
using System;
using System.Diagnostics;
using System.Linq;

namespace StarStuff.avb
{
    class Program
    {
        static void Main(string[] args)
        {
            RunTest_SortCart();
            RunTest_Linq();

            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }

        static void RunTest_SortCart()
        {
            var source = CartManager.GetCarts();
            var count = source.Count();

            Console.WriteLine("TEST #1");
            Console.WriteLine("Неупорядоченный список маршрутов");
            Console.WriteLine("----------------------------------------------------");
            CartManager.Print(source);

            Console.WriteLine("Упорядоченный список маршрутов");
            Console.WriteLine("----------------------------------------------------");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var ordered = CartManager.Sort(source);
            sw.Stop();
            CartManager.Print(ordered);
            Console.WriteLine("Время выполнения " + (sw.ElapsedMilliseconds).ToString()+"ms на " + count.ToString() + " записей.");
            Console.WriteLine();
        }

        static void RunTest_Linq()
        {
            Console.WriteLine("TEST #2");
            SalesManager.PrintAllSales();
            SalesManager.PrintProductFirstSalesCountView(); // основой запрос
            SalesManager.PrintProductMinDaysView(); // для отладки
        }
    }
}
