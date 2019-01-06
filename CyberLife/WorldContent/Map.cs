using CyberLife.Simple2DWorld;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CyberLife
{
    /// <summary>
    /// Представляет собой карту мира
    /// </summary>
    public class Map
    {
        #region fields

        private int _width;
        private int _height;
        private BotLifeForm[,] _lifeForms;
        private BotLifeForm[,] _organic;


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

        public BotLifeForm[,] LifeForms { get => _lifeForms; set => _lifeForms = value; }

        public BotLifeForm[,] Organic { get => _organic; set => _organic = value; }

        /// <summary>
        /// Число живых ботов
        /// </summary>
        public int LifeFormsCount { get; set; }

        /// <summary>
        /// Число органики
        /// </summary>
        public int OrganicCount { get; set; }

        #endregion


        #region methods

        /// <summary>
        /// Используется для добавления живого бота
        /// </summary>
        /// <param name="bot"></param>
        public void Add(BotLifeForm bot)
        {
            if (LifeForms[bot.Point.X, bot.Point.Y] != null)
                throw new ArgumentException("Клетка уже использовна");
            LifeForms[bot.Point.X, bot.Point.Y] = bot;
            LifeFormsCount++;
        }



        /// <summary>
        /// Используется для удаления живого бота
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Remove(int x, int y)
        {
            if (LifeForms[x, y] == null)
                throw new ArgumentException("Клетка уже пуста");
            LifeForms[x, y] = null;
            LifeFormsCount--;
        }



        /// <summary>
        /// Используется для добавления органики
        /// </summary>
        /// <param name="bot"></param>
        public void AddOrganic(BotLifeForm bot)
        {
            if (Organic[bot.Point.X, bot.Point.Y] != null)
                throw new ArgumentException("Клетка уже использовна");
            Organic[bot.Point.X, bot.Point.Y] = bot;
            OrganicCount++;
        }



        /// <summary>
        /// Используется для удаления органики
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void RemoveOrganic(int x, int y)
        {
            if (Organic[x, y] == null)
                throw new ArgumentException("Клетка уже пуста");
            Organic[x, y] = null;
            OrganicCount--;
        }



        /// <summary>
        /// Определяет,свободна ли выбранная клетка,и если нет,то возвращает форму жизни в данной клетке
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="botOnPlace"></param>
        /// <returns></returns>
        public bool IsPlaceEmpty(int x, int y, out BotLifeForm bot)
        {
            if (LifeForms[x, y] != null)
            {
                bot = LifeForms[x, y];
                return false;
            }
            if (Organic[x, y] != null)
            {
                bot = Organic[x, y];
                return false;
            }
            bot = null;
            return true;
        }



        /// <summary>
        /// Определяет,есть ли вокруг данной клетки свободное пространство,возвращает bool и передаёт координаты клетки в ref параметрах,
        /// в противном случае возвращает false и исходные координаты центральной клетки
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        public bool IsAroundEmpty(ref int X, ref int Y)
        {
            BotLifeForm bot = null;
            int workX = X;
            int workY = Y;
            workY++;
            workX--;
            for (int i = 1; i < 4; i++)
            {
                if (workY > Height - 1)
                    workY = Height - 1;
                if (workY < 0)
                    workY = 0;
                if (workX > Width - 1)
                    workX = 0;
                if (workX < 0)
                    workX = Width - 1;
                if (IsPlaceEmpty(workX, workY, out bot))
                {
                    X = workX;
                    Y = workY;
                    return true;
                }
                workY--;
            }
            workX++;
            workY--;
            for (int i = 1; i < 4; i++)
            {
                workY++;
                if (i == 2)
                {
                    continue;
                }
                if (workY > Height - 1)
                    workY = Height - 1;
                if (workY < 0)
                    workY = 0;
                if (workX > Width - 1)
                    workX = 0;
                if (workX < 0)
                    workX = Width - 1;
                if (IsPlaceEmpty(workX, workY, out bot))
                {
                    X = workX;
                    Y = workY;
                    return true;
                }
            }
            workX++;
            for (int i = 1; i < 4; i++)
            {
                if (workY > Height - 1)
                    workY = Height - 1;
                if (workY < 0)
                    workY = 0;
                if (workX > Width - 1)
                    workX = 0;
                if (workX < 0)
                    workX = Width - 1;
                if (IsPlaceEmpty(workX, workY, out bot))
                {
                    X = workX;
                    Y = workY;
                    return true;
                }
                workY--;
            }
            return false;
        }



        /// <summary>
        /// Возвращает IEnumerator для удобного взаимодействия с foreach
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            foreach (BotLifeForm bot in LifeForms)
            {
                if (bot != null)
                {
                    yield return bot;
                }
            }
        }
        #endregion


        #region constructors

        /// <summary>
        /// Инициализирует экземпляр MapSize из
        /// размеров поля.
        /// </summary>
        /// <param name="width">Ширина поля</param>
        /// <param name="height">Высота поля</param>
        /// <param name="lifeForms">Формы жизни</param>
        /// <param name="organic">Органика</param>
        public Map(int width, int height, List<BotLifeForm> lifeForms, List<BotLifeForm> organic)
        {
            if (width < 0 || height < 0)
                throw new ArgumentException("_width and height parameters should be positive.");
            _width = width;
            _height = height;
            LifeForms = new BotLifeForm[width, height];
            Organic = new BotLifeForm[width, height];
            if (organic != null)
            {
                foreach (BotLifeForm bot in organic)
                {
                    AddOrganic(bot);
                }
            }
            if (lifeForms != null)
            {
                foreach (BotLifeForm bot in lifeForms)
                {
                    Add(bot);
                }
            }
        }

        #endregion
    }
}