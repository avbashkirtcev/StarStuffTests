using System;
using System.Collections.Generic;
using System.Linq;

namespace StarStuff.avb.Test1
{
    /// <summary>
    /// универсальное двоичное дерево
    /// для n объектов сложность будет составлять O(n log(n))
    /// в худшем случае O(n²)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinTree<T> where T : IComparable<T>
    {
        public BinTree(T value)
        {
            Value = value;
        }
        //-------------------------------------------------------------------------------------------------

        public BinTree(IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source", "Невозможно создать дерево!");
            }

            bool first = true;
            foreach (T i in source)
            {
                if (first)
                {
                    Value = i;
                    first = false;
                }
                else
                {
                    Add(i);
                }
            }
        }
        //-------------------------------------------------------------------------------------------------

        // узел для меньших значений
        public BinTree<T> Left { get; set; }

        // узел для больших значений
        public BinTree<T> Right { get; set; }

        // текущее значение
        public T Value { get; set; }

        /// <summary>
        /// метод сортировки
        /// </summary>
        /// <param name="source">обязательный параметр</param>
        /// <returns></returns>
        public static IEnumerable<T> Sort(IEnumerable<T> source)
        {
            // буфер для возвращения результата
            var ret = new List<T>(source.Count());

            // заполняем дерево
            var t = new BinTree<T>(source);

            // заполняем буфер
            t.Read((v) => { ret.Add(v); });
            return ret;
        }

        /// <summary>
        /// добавить узел в дерево
        /// </summary>
        /// <param name="value"></param>
        public void Add(T value)
        {
            // сравниваем элементы
            var cmp = value.CompareTo(Value);

            // если элемент меньше текущего заносим его в левую часть
            if (cmp < 0)
            {
                if (Left == null)
                {
                    Left = new BinTree<T>(value);
                }
                else
                {
                    // делаем рекурсию по наименьшим элементам
                    Left.Add(value);
                }
            }
            // в остальных случаях заносим его в правую часть
            else if (Right == null)
            {
                Right = new BinTree<T>(value);
            }
            else
            {
                // делаем рекурсию по наибольшим элементам
                Right.Add(value);
            }
        }
        //-------------------------------------------------------------------------------------------------

        /// <summary>
        /// получить содержимое всех узлов в упорядоченном виде
        /// </summary>
        /// <param name="onReadValue">функция обработки полученного значения</param>
        public void Read(Action<T> onReadValue)
        {
            if (Left != null)
            {
                // сначала проходим по элементам с наименьшими значениями
                Left.Read(onReadValue);
            }

            // возвращаем элемент
            onReadValue(Value);

            if (Right != null)
            {
                // затем проходим по элементам с наибольшими значениями
                Right.Read(onReadValue);
            }
        }
        //-------------------------------------------------------------------------------------------------
    }
}
