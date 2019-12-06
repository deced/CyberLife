using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberLife.Simple2DWorld
{

    class MineralsPhenomen : IPhenomen
    {
        private const int PercentOfMap = 50;
        private const int NormalIntensity = 80;

        #region fields

        private int _baseIntensity = 90;
        private Place _place;

        #endregion


        #region  properties

        public int Percent { get => PercentOfMap; }

        public int BaseIntensity { get { return _baseIntensity; } set { _baseIntensity = value; } }

        #endregion


        #region Methods

        /// <summary>
        /// Получает эффекты воздействия этого феномена на бота
        /// </summary>
        /// <param name="lifeForm">Бот, который получает эффекты феномена</param>
        public void GetEffects(BotLifeForm bot)
        {
            if (isIn(bot.Point))
            {
                double depthFactor = 1 / (1 + ((double)(_place[1].Y - bot.Point.Y) / _place[0].Y));
                bot.Energy += (int)(BaseIntensity * depthFactor);
                bot.LastEnergyActions.Enqueue(Actions.Extraction);
            }
        }



        /// <summary>
        /// Возвращает экземпляр  класса Place, представляющий пространство,
        /// на  которое воздействует этот феномен
        /// </summary>
        public Place GetItPlace()
        {
            return _place;
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
        /// Вызывает обновление феномена
        /// </summary>
        /// <param name="world">Мир, для которого происходит обновление</param>
        public void Update(Simple2DWorld world)
        {

        }

        #endregion


        #region  constructors

        /// <summary>
        /// Инициализирует экземпляр MineralsPhenomen,
        /// занимающий нижнюю половину площади карты
        /// </summary>
        /// <param name="x">Размер мира по X</param>
        /// <param name="y">Размер мира по Y</param>
        public MineralsPhenomen(int x,int y)
        {
            List<Point> points = new List<Point>(2);          
            points.Add(new Point(0,Convert.ToInt32(y * (100 - PercentOfMap) / 100d)));
            points.Add(new Point(x, y));
            _place = new Place(points, PlaceType.Rectangle);
            _place.PlaceType = PlaceType.Rectangle;
        }


        /// <summary>
        /// Инициализирует экземпляр MineralsPhenomen, 
        /// занимающий указанное place пространство
        /// </summary>
        /// <param name="place">Пространство, которое будет занимать феномен</param>
        public MineralsPhenomen(Place place)
        {
            if (place == null)
            {
                throw new ArgumentNullException();
            }

            _place = place;

        }

        #endregion
    }
}