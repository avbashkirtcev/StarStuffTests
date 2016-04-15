using System;
using System.Collections.Generic;
using System.Linq;

namespace StarStuff.avb.Test2
{
    public static class SalesManager
    {
        static int _saleId = 0;
        static List<Sales> _sales;

        // количество первичных покупок продукта
        public static IQueryable<ProductFirstSalesCountView> ProductFirstSalesCountView()
        {
            var qProductView = from p in ProductMinDaysView()
                               group p.Date by p.ProductId into g
                               select new ProductFirstSalesCountView { ProductId = g.Key, Count = g.Distinct().Count() };

            return qProductView;
        }
        //-------------------------------------------------------------------------------------------------

        // минимальные даты когда производилась покупка товара
        public static IQueryable<ProductMinDaysView> ProductMinDaysView()
        {
            // Запрос вычисляет все минимальные даты когда производилась покупка товара
            var qProductMinDays = from p in GetQ()
                                  where p.Date == (
                                     // минимальная дата покупки продукта в разрезе текущиего клиента
                                     from x in GetQ()
                                     where x.ProductId == p.ProductId && x.CustomerId == p.CustomerId
                                     orderby x.Date
                                     select x.Date).FirstOrDefault()
                                  select new ProductMinDaysView { ProductId = p.ProductId, Date = p.Date };

            return qProductMinDays;
        }
        //-------------------------------------------------------------------------------------------------

        static IEnumerable<Sales> GetSales()
        {
            if (_sales == null)
            {
                _sales = new List<Sales>();
                _sales.AddRange(new[] {
                    new Sales {Id = ++_saleId, CustomerId = 1, ProductId = 1, Date = new DateTime(2016, 1, 1) },
                    new Sales {Id = ++_saleId, CustomerId = 1, ProductId = 1, Date = new DateTime(2016, 1, 2) },
                    new Sales {Id = ++_saleId, CustomerId = 1, ProductId = 2, Date = new DateTime(2016, 1, 1) },
                    new Sales {Id = ++_saleId, CustomerId = 2, ProductId = 3, Date = new DateTime(2016, 1, 1) },
                    new Sales {Id = ++_saleId, CustomerId = 1, ProductId = 3, Date = new DateTime(2016, 1, 2) },
                    new Sales {Id = ++_saleId, CustomerId = 2, ProductId = 3, Date = new DateTime(2016, 1, 1) }
                });
            }
            return _sales;
        }
        //-------------------------------------------------------------------------------------------------

        public static void PrintAllSales()
        {
            Console.WriteLine("Все продажи");
            Console.WriteLine("Id\tPrdId\tCstId\tDate");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine(string.Join("\r\n", GetQ().Select(i => string.Format("{0}\t{1}\t{2}\t{3}", i.Id, i.ProductId, i.CustomerId, i.Date))));
            Console.WriteLine();
        }
        //-------------------------------------------------------------------------------------------------

        public static void PrintProductFirstSalesCountView()
        {
            Console.WriteLine("количество первичных покупок продукта");
            Console.WriteLine("PrdId\tCount");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine(string.Join("\r\n", ProductFirstSalesCountView().Select(i => string.Format("{0}\t{1}", i.ProductId, i.Count))));
            Console.WriteLine();
        }
        //-------------------------------------------------------------------------------------------------

        public static void PrintProductMinDaysView()
        {
            Console.WriteLine("минимальные даты когда производилась покупка товара");
            Console.WriteLine("PrdId\tDate");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine(string.Join("\r\n", ProductMinDaysView().Select(i => string.Format("{0}\t{1}", i.ProductId, i.Date))));
            Console.WriteLine();
        }
        //-------------------------------------------------------------------------------------------------

        static IQueryable<Sales> GetQ()
        {
            return GetSales().AsQueryable();
        }
        //-------------------------------------------------------------------------------------------------
    }
}
