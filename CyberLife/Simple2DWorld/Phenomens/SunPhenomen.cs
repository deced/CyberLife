using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using CyberLife.Platform.World_content;

namespace CyberLife.Simple2DWorld
{
    class SunPhenomen : IPhenomen
    {
        private const int PercentOfMap = 50;
        private const double NormalPowerFactor = 1;
        private const double LowPowerFactor = 0.5;
        private const double HightPowerFactor = 1.5;


        #region fields

        private int _baseIntensity = 90;
        private double _powerFactor;

        private Place _place;

        #endregion


        #region  properties

        public int BaseIntensity { get { return _baseIntensity; } set { _baseIntensity = value; } }

        #endregion


        #region Methods


        /// <summary>
        /// Вызывает обновление интенсивности в зависимости от текущего сезона
        /// </summary>
        /// <param name="world">Мир, для которого происходит обновление</param>
        public void Update(Simple2DWorld world)
        {
            if (!world.NaturalPhenomena.ContainsKey("SeasonsPhenomen"))
            {
                ArgumentException ex = new ArgumentException("world metadata isn't contains SeasonPhenomen metadata", nameof(world));
                throw ex;
            }

            Season season = ((SeasonsPhenomen)world.NaturalPhenomena["SeasonsPhenomen"]).CurSeason;
            switch (season)
            {
                case Season.Autumn:
                case Season.Spring:
                    {
                        _powerFactor = NormalPowerFactor;
                    }
                    break;

                case Season.Summer:
                    {
                        _powerFactor = HightPowerFactor;
                    }
                    break;

                case Season.Winter:
                    {
                        _powerFactor = LowPowerFactor;
                    }
                    break;

                default:
                    {
                        Exception ex = new Exception("Impossible");
                        throw ex;
                    }

            }

        }



        /// <summary>
        /// Определяет, оказывает ли феномен воздействие на форму жизни и оказывает это воздействие
        /// </summary>
        /// <param name="bot">Бот, получающий эффект</param>
        public void GetEffects(BotLifeForm bot)
        {
            Point botPoint = new Point(bot.Point.X, bot.Point.Y);
            if (isIn(botPoint))
            {
                double depthFactor = 1 / (1 + (double)(botPoint.Y - _place[0].Y) / _place[1].Y);
                bot.Energy += (int)(BaseIntensity * _powerFactor * depthFactor);
                bot.LastEnergyActions.Enqueue(Actions.Photosynthesis);
            }

        }



        /// <summary>
        /// Проверяет, попадает ли точка под действие природного явления
        /// </summary>
        /// <param name="point">Точка для проверки</param>
        /// <returns>Попадает?</returns>
        public bool isIn(Point point)
        {
            return _place.IsIn(point);
        }



        /// <summary>
        /// Возвращает экземпляр  класса Place, представляющий пространство,
        /// на которое воздействует этот феномен
        /// </summary>
        public Place GetItPlace()
        {
            return _place;
        }

        #endregion


        #region  constructors

        /// <summary>
        /// Инициализирует экземпляр SunPhenomen,
        /// занимающий верхнюю половину площади карты
        /// </summary>
        /// <param name="x">Размер карты по X</param>
        /// <param name="y">Размер карты по Y</param>
        public SunPhenomen(int x,int y)
        {
            _powerFactor = NormalPowerFactor;
            List<Point> points = new List<Point>(2);
            points.Add(new Point(0, 0));
            points.Add(new Point(x, (int)Math.Round(y * PercentOfMap/100d)));
            _place = new Place(points, PlaceType.Rectangle);
            _place.PlaceType = PlaceType.Rectangle;
        }



        /// <summary>
        /// Инициализирует экземпляр SunPhenomen, 
        /// занимающий указанное place пространство
        /// </summary>
        /// <param name="place">Пространство, которое будет занимать феномен</param>
        public SunPhenomen(Place place)
        {
            if (place == null)
            {
                throw new ArgumentNullException();
            }

            _place = place;
            _powerFactor = NormalPowerFactor;

        }

        #endregion
    }
}