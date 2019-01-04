﻿using System;
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
        private int _baseIntensity = 100;
        private const double NormalPowerFactor = 1;
        private const double LowPowerFactor = 0.5;
        private const double HightPowerFactor = 1.5;
        private const double SunDepthFactor = 0.5;


        #region fields

        private double _powerFactor;

        private Place _place;

        public int BaseIntensity { get { return _baseIntensity; } set { _baseIntensity = value; } }

        #endregion


        #region  properties



        #endregion

        #region Methods


        /// <summary>
        /// Вызывает обновление интенсивности в зависимости от текущего сезона
        /// </summary>
        /// <param name="worldMetadata">Метаданные мира. В окружающей среде должен быть феномен времен года.</param>
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
        /// Определяет, оказывает ли феномен воздействие на форму жизни и возвращает 
        /// результат этого воздействия
        /// </summary>
        /// <param name="point">Точка</param>
        /// <param name="lifeFormMetadataMetadata">метаданные форммы жизни, находящейся в этой точке</param>
        /// <returns>Эффект воздействия феномена</returns>
        public void GetEffects(BotLifeForm bot)
        {
            Point botPoint = new Point(bot.Point.X, bot.Point.Y);
            if (isIn(botPoint))
            {
                double depthFactor = 1 / (1 + (double)(botPoint.Y - _place[0].Y) / _place[1].Y);
                ((BotLifeForm)bot).Energy += (int)(BaseIntensity * _powerFactor * depthFactor);
                ((BotLifeForm)bot).LastEnergyActions.Enqueue(Actions.Photosynthesis);
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



        /// <inheritdoc />
        /// <summary>
        /// Возвращает экземпляр  класса LifeFormPlace, представляющий пространство,
        /// на  которое воздействует этот феномен
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
        /// <param name="mapSize">Размер карты</param>
        public SunPhenomen(int x,int y)
        {
            _powerFactor = NormalPowerFactor;
            List<Point> points = new List<Point>(2);
            points.Add(new Point(0, 0));
            points.Add(new Point(x, (int)Math.Round(y * SunDepthFactor)));
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