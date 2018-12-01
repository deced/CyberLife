using CyberLife.Interfaces;
using CyberLife.Simple2DWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberLife
{
    public struct Point
    {
        static Random rnd = new Random();
        #region fields

        int _x;
        int _y;

        #endregion


        #region properties

        /// <summary>
        /// Абцисса точки
        /// </summary>
        public int X
        {
            get => _x;
            set => _x = value;
        }

        /// <summary>
        /// Ордината точки
        /// </summary>
        public int Y
        {
            get => _y;
            set => _y = value;
        }

        #endregion


        #region methods

        /// <summary>
        /// Преобразует Point в инициализирующую строку,
        /// которая может быть в дальнейшем использована для 
        /// реконструкции экземпляра класса. 
        /// </summary>
        /// <returns>Строка вида "X|Y" </returns>
        public override string ToString()
        {
            return "" + _x + "|" + _y;
        }



        /// <summary>
        /// Создает экземпляр класса из специальной инициализирующей строки
        /// </summary>
        /// <param name="str">Строка вида X|Y</param>
        /// <returns>Точка, описанная данной строкой</returns>
        public static Point FromString(string str)
        {
            Point pnt;
            try
            {
                var coords = str.Trim(' ').Split('|');

                pnt = new Point(int.Parse(coords[0]), int.Parse(coords[1]));

            }
            catch (FormatException)
            {
                throw new ArgumentException("String isn't contain point", nameof(str));

            }
            catch (IndexOutOfRangeException)
            {

                throw new ArgumentException("String isn't contain point", nameof(str));
            }
            return pnt;
        }



        public static Point RandomPoint(MapSize map, Dictionary<Point, ILifeForm> lifeForms = null)
        {
            if (lifeForms == null)
                return new Point(rnd.Next(0, map.Width), rnd.Next(0, map.Height));
            while (true)
            {
                Point point = new Point(rnd.Next(0, map.Width), rnd.Next(0, map.Height));
                if (!lifeForms.ContainsKey(point))
                    return point;
            }
        }

        #endregion


        #region constructors

        /// <summary>
        /// Инициализирует экземпляр точки
        /// </summary>
        /// <param name="x">Абцисса точки</param>
        /// <param name="y">Ордината точки</param>
        public Point(int x, int y)
        {
            _x = x;
            _y = y;
        }

        #endregion
    }
}