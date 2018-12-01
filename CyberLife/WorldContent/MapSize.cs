using System;
namespace CyberLife
{
    /// <summary>
    /// Представляет собой размер поля
    /// </summary>
    public class MapSize
    {
        #region fields

        private int _width;
        private int _height;

        #endregion


        #region properties

        /// <summary>
        /// Ширина карты
        /// </summary>
        public int Width { get => _width; set => _width = value; }

        /// <summary>
        /// Высота карты
        /// </summary>
        public int Height { get => _height; set => _height = value; }


        /// <summary>
        /// Число точек на поле
        /// </summary>
        public int CountOfPoint => _width * _height;

        #endregion


        #region methods

        #endregion


        #region constructors

        /// <summary>
        /// Инициализирует экземпляр MapSize из
        /// ширины и длины поля.
        /// </summary>
        /// <param name="width">Ширина поля</param>
        /// <param name="height">Высота поля</param>
        public MapSize(int width, int height)
        {
            if (width < 0 || height < 0)
                throw new ArgumentException("_width and height parameters should be positive.");
            _width = width;
            _height = height;

        }

        #endregion
    }
}