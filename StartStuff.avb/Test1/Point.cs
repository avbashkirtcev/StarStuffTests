using System;

namespace StarStuff.avb.Test1
{
    // точка маршрута
    public interface IPoint : IComparable<IPoint>, IEquatable<IPoint>
    {
        int Id { get; }
        string Name { get; set; }
    }

    // точка маршрута
    public class Point : IPoint
    {
        public Point(int id, string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name", "Невозможно создать точку маршрута!");
            }
            Id = id;
            Name = name;
        }
        //-------------------------------------------------------------------------------------------------

        #region свойства

        /// <summary>
        /// уникальный идентификатор точки
        /// </summary>
        public int Id { get; private set; }
        //-------------------------------------------------------------------------------------------------

        /// <summary>
        /// наименование исходной точки
        /// </summary>
        public string Name { get; set; }
        //-------------------------------------------------------------------------------------------------
        #endregion свойства

        #region перегруженнные базовые методы

        /// <summary>
        /// возвращаем Id
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Id;
        }
        //-------------------------------------------------------------------------------------------------

        /// <summary>
        /// сравниваем строготипизированные объекты
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as IPoint);
        }
        //-------------------------------------------------------------------------------------------------

        /// <summary>
        /// карточки сравниваем по Id
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(IPoint other)
        {
            return other != null && other.Id == Id;
        }
        //-------------------------------------------------------------------------------------------------

        /// <summary>
        /// выводим имя
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name ?? string.Empty;
        }
        //-------------------------------------------------------------------------------------------------

        /// <summary>
        /// сравнение по Id
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(IPoint other)
        {
            if (other == null) return -1;
            return Id.CompareTo(other.Id);
        }
        //-------------------------------------------------------------------------------------------------
        #endregion перегруженнные базовые методы
    }
}
