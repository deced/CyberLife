using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberLife.Simple2DWorld
{

    class MineralsPhenomen : IPhenomen
    {
        private int _baseIntensity = 100;
        private const int PercentOfMap = 50;

        #region fields

        private Place _place;

        #endregion


        #region  properties

        public int Percent { get => PercentOfMap; }

        public int BaseIntensity { get { return _baseIntensity; } set { _baseIntensity = value; } }

        #endregion


        #region Methods

        /// <summary>
        /// Определяет, оказывает ли феномен воздействие на форму жизни и возвращает 
        /// результат этого воздействия
        /// </summary>
        /// <param name="point">Точка</param>
        /// <param name="lifeFormMetadataMetadata">метаданные форммы жизни, находящейся в этой точке</param>
        /// <returns>Эффект воздействия феномена</returns>
        public void GetEffects(BotLifeForm bot)
        {
            if (isIn(bot.Point))
            {
                double depthFactor = 1 / (1 + ((double)(_place[1].Y - bot.Point.Y) / _place[0].Y));
                ((BotLifeForm)bot).Energy += (int)(BaseIntensity * depthFactor);
                ((BotLifeForm)bot).LastEnergyActions.Enqueue(Actions.Extraction);
            }
        }



        /// <summary>
        /// Возвращает экземпляр  класса LifeFormPlace, представляющий пространство,
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



        public void Update(Simple2DWorld worldMetadata)
        {

        }

        #endregion


        #region  constructors

        /// <summary>
        /// Инициализирует экземпляр MineralsPhenomen,
        /// занимающий нижнюю половину площади карты
        /// </summary>
        /// <param name="mapSize">Размер карты</param>
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