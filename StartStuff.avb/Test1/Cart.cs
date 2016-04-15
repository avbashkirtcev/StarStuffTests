using System;

namespace StarStuff.avb.Test1
{
    // маршрут
    public interface ICart : IEquatable<ICart>, IComparable<ICart>
    {
        int Id { get; }
        IPoint Start { get; set; }
        IPoint End { get; set; }
    }

    // маршрут
    public class Cart : ICart
    {
        IPoint _start;
        IPoint _end;

        public Cart(int id, IPoint start, IPoint end)
        {
            Id = id;
            Start = start;
            End = end;
        }
        //-------------------------------------------------------------------------------------------------

        #region свойства

        /// <summary>
        /// уникальный идентификатор маршрута
        /// </summary>
        public int Id { get; private set; }
        //-------------------------------------------------------------------------------------------------

        /// <summary>
        /// начальная точка маршрута
        /// не может быть null и не должна совпадать с концом
        /// </summary>
        public IPoint Start
        {
            get { return _start; }
            set
            {
                if (_start != value)
                {
                    CheckStart(value);
                    _start = value;
                }
            }
        }
        //-------------------------------------------------------------------------------------------------

        /// <summary>
        /// конечная точка назначения
        /// не может быть null и не должна совпадать с началом
        /// </summary>
        public IPoint End
        {
            get { return _end; }
            set
            {
                if (_end != value)
                {
                    CheckEnd(value);
                    _end = value;
                }
            }
        }
        //-------------------------------------------------------------------------------------------------
        #endregion свойства

        #region внутренние методы

        /// <summary>
        /// проверка корректности исходной точки
        /// </summary>
        /// <param name="value"></param>
        protected virtual void CheckStart(IPoint value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("start", "Начало маршрута не может быть null!");
            }
            if (value.Equals(End))
            {
                throw new InvalidOperationException(string.Format("Невозможно назначить исходную точку для \"{0}\" - она не должна совпадать с конечной!", this));
            }
        }
        //-------------------------------------------------------------------------------------------------

        /// <summary>
        /// проверка корректности точки назначения
        /// </summary>
        /// <param name="value"></param>
        protected virtual void CheckEnd(IPoint value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("start", "Конечная точка маршрута не может быть null!");
            }
            if (value.Equals(Start))
            {
                throw new InvalidOperationException(string.Format("Невозможно назначить конечную точку для \"{0}\" - она не должна совпадать с исходной!", this));
            }
        }
        //-------------------------------------------------------------------------------------------------
        #endregion внутренние методы

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
            return Equals(obj as ICart);
        }
        //-------------------------------------------------------------------------------------------------

        /// <summary>
        /// карточки сравниваем по Id
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ICart other)
        {
            return other != null && other.Id == Id;
        }
        //-------------------------------------------------------------------------------------------------

        public override string ToString()
        {
            var s = Start == null ? "null" : Start.ToString();
            var e = End == null ? "null" : End.ToString();
            return string.Format("{0} >> {1}", s, e);
        }
        //-------------------------------------------------------------------------------------------------

        /// <summary>
        /// маршруты упорядочены по связке: начальная точка текущего маршрута - конечная точка предыдущего маршрута
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(ICart other)
        {
            if (other == null) return -1;
            return Start.CompareTo(other.End);
        }
        //-------------------------------------------------------------------------------------------------
        #endregion перегруженнные базовые методы
    }
}
