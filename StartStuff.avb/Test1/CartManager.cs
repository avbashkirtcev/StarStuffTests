using System;
using System.Collections.Generic;
using System.Linq;

namespace StarStuff.avb.Test1
{
    // менеджер маршрутов
    public static class CartManager
    {
        static int _pointId = 0;        // id точки
        static int _cartId = 0;         // id маршрута
        static List<IPoint> _points;    // точки
        static List<ICart> _carts;      // маршруты

        /// <summary>
        /// инициализирует и возвращает список всех маршрутов
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ICart> GetCarts()
        {
            if (_carts == null)
            {
                _carts = new List<ICart>();

                _carts.AddRange(new[] {
                    new Cart(++_cartId, GetPoint("Мельбурн"), GetPoint("Кельн")),
                    new Cart(++_cartId, GetPoint("Москва1"), GetPoint("Париж")),
                    new Cart(++_cartId, GetPoint("Москва10"), GetPoint("Рим")),
                    new Cart(++_cartId, GetPoint("Париж"), GetPoint("Москва10")),
                    new Cart(++_cartId, GetPoint("Кельн"), GetPoint("Москва1")),
                    new Cart(++_cartId, GetPoint("1"), GetPoint("4")),
                    new Cart(++_cartId, GetPoint("6"), GetPoint("7")),
                    new Cart(++_cartId, GetPoint("5"), GetPoint("6")),
                    new Cart(++_cartId, GetPoint("4"), GetPoint("5")),
                });
            }
            return _carts;
        }
        //-------------------------------------------------------------------------------------------------

        /// <summary>
        /// возвращает точку маршрута
        /// если такой точки еще нет в базе, то она добавлеятся
        /// </summary>
        /// <param name="name">наименование точки, !может быть null</param>
        /// <returns></returns>
        public static IPoint GetPoint(string name)
        {
            _points = _points ?? new List<IPoint>();
            var p = _points.FirstOrDefault(i => i.Name == name);
            if (p == null)
            {
                p = new Point(++_pointId, name);
                _points.Add(p);
            }
            return p;
        }
        //-------------------------------------------------------------------------------------------------

        /// <summary>
        /// выводит на консоль элементы произвольного списка
        /// </summary>
        public static void Print<T>(IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source", "Ошибка вызова метода Print!");
            }

            var ret = string.Join("\r\n", source);
            Console.WriteLine(ret);
            Console.WriteLine();
        }
        //-------------------------------------------------------------------------------------------------

        /// <summary>
        /// сортировка произвольного списка
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<T> Sort<T>(IEnumerable<T> source) where T : IComparable<T>
        {
            return BinTree<T>.Sort(source);
        }
        //-------------------------------------------------------------------------------------------------
    }
}
