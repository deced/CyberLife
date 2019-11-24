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

        /// <summary>
        /// Двумерный массив, содержащий живых ботов
        /// </summary>
        public BotLifeForm[,] LifeForms { get => _lifeForms; set => _lifeForms = value; }

        /// <summary>
        /// Двумерный массив, содержащий органику
        /// </summary>
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
        /// <param name="bot">Бот, который будет добавлен на карту</param>
        public void Add(BotLifeForm bot)
        {
            if (LifeForms[bot.Point.X, bot.Point.Y] != null)
                throw new ArgumentException("Клетка уже используется");
            LifeForms[bot.Point.X, bot.Point.Y] = bot;
        }



        /// <summary>
        /// Используется для удаления живого бота
        /// </summary>
        /// <param name="x">Координата удаляемого бота по X</param>
        /// <param name="y">Координата удаляемого бота по Y</param>
        public void Remove(int x, int y)
        {
            if (LifeForms[x, y] == null)
                throw new ArgumentException("Клетка уже пуста");
            LifeForms[x, y] = null;
        }



        /// <summary>
        /// Используется для добавления органики
        /// </summary>
        /// <param name="bot">Органика, которая будет добавлена на карту</param>
        public void AddOrganic(BotLifeForm bot)
        {
            if (Organic[bot.Point.X, bot.Point.Y] != null)
                throw new ArgumentException("Клетка уже используется");
            Organic[bot.Point.X, bot.Point.Y] = bot;
        }



        /// <summary>
        /// Используется для удаления органики
        /// </summary>
        /// <param name="x">Координата удаляемой органики по X</param>
        /// <param name="y">Координата удаляемой органики по Y</param>
        public void RemoveOrganic(int x, int y)
        {
            if (Organic[x, y] == null)
                throw new ArgumentException("Клетка уже пуста");
            Organic[x, y] = null;
        }



        /// <summary>
        /// Определяет,свободна ли выбранная клетка,и если нет,то возвращает форму жизни в данной клетке
        /// </summary>
        /// <param name="X">Координата клетки по X</param>
        /// <param name="Y">Координата клетки по Y</param>
        /// <param name="botOnPlace">Экземпляр бота, находящегося в этой клетке</param>
        /// <returns>Если клетка пуста, то возвращает true, иначе false</returns>
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
        /// Определяет дружеского бота с наименьшим количеством энергии
        /// </summary>
        /// <param name="x">Координата бота по X</param>
        /// <param name="y">Координата бота по Y</param>
        /// <param name="bot">Возвращаемый бот с наименьшим количеством энергии</param>
        /// <returns>Возврачает false, если рядом нет союзного бота, иначе возвращает true</returns>
        public bool GetLowEnergyFriend(int x, int y, out BotLifeForm lowEBot)
        {

            List<BotLifeForm> bots = new List<BotLifeForm> { };
            BotLifeForm bot;
            for (int i = 1; i < 4; i++)
            {
                if (y > Height - 1)
                    y = Height - 1;
                if (y < 0)
                    y = 0;
                if (x > Width - 1)
                    x = 0;
                if (x < 0)
                    x = Width - 1;
                if (!IsPlaceEmpty(x, y, out bot))
                {
                    bots.Add(bot);
                }
                y--;
            }
            x++;
            y--;
            for (int i = 1; i < 4; i++)
            {
                y++;
                if (i == 2)
                {
                    continue;
                }
                if (y > Height - 1)
                    y = Height - 1;
                if (y < 0)
                    y = 0;
                if (x > Width - 1)
                    x = 0;
                if (x < 0)
                    x = Width - 1;
                if (!IsPlaceEmpty(x, y, out bot))
                {
                    bots.Add(bot);
                }
            }
            x++;
            for (int i = 1; i < 4; i++)
            {
                if (y > Height - 1)
                    y = Height - 1;
                if (y < 0)
                    y = 0;
                if (x > Width - 1)
                    x = 0;
                if (x < 0)
                    x = Width - 1;
                if (!IsPlaceEmpty(x, y, out bot))
                {
                    bots.Add(bot);
                }
                y--;
            }
            if (bots.Count == 0)
            {
                lowEBot = null;
                return false;
            }
            lowEBot = bots[0];
            for(int i = 1;i< bots.Count;i++)
            {
                if (lowEBot.Energy > bots[i].Energy)
                    lowEBot = bots[0];
            }
            return true;
        }
        /// <summary>
        /// Определяет,есть ли вокруг данной клетки свободное пространство
        /// </summary>
        /// <param name="X">Исходная координата, после выполнения метода возвращается координата свободной клетки по оси X</param>
        /// <param name="Y">Исходная координата, после выполнения метода возвращается координата свободной клетки по оси Y</param>
        /// <returns>Если вокруг клетки есть свободное пространство, возвращает true, иначе false</returns>
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
        /// Возвращает IEnumerator для взаимодействия с foreach
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
        /// размеров поля, массивов форм жизни и органики.
        /// </summary>
        /// <param name="width">Ширина поля</param>
        /// <param name="height">Высота поля</param>
        /// <param name="lifeForms">Массив форм жизни</param>
        /// <param name="organic">Массив органики</param>
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
                    OrganicCount++;
                }
            }
            if (lifeForms != null)
            {
                foreach (BotLifeForm bot in lifeForms)
                {
                    Add(bot);
                    LifeFormsCount++;
                }
            }
        }

        #endregion
    }
}