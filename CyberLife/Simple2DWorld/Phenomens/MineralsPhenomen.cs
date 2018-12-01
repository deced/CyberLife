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
        public int GetEffects(Point point)
        {
            if (isIn(point))
            {
                double depthFactor = 1 / (1 + ((double)(_place[1].Y - point.Y) / _place[0].Y));
                return (int)(BaseIntensity * depthFactor);

            }
            return 0;
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



        public void Update(World worldMetadata)
        {

        }

        #endregion


        #region  constructors

        /// <summary>
        /// Инициализирует экземпляр MineralsPhenomen,
        /// занимающий нижнюю половину площади карты
        /// </summary>
        /// <param name="mapSize">Размер карты</param>
        public MineralsPhenomen(MapSize mapSize)
        {
            if (mapSize == null)
            {
                ArgumentNullException ex = new ArgumentNullException(nameof(mapSize));
                throw ex;
            }
            List<Point> points = new List<Point>(2);          
            points.Add(new Point(0,Convert.ToInt32( mapSize.Height * (100 - PercentOfMap) / 100d)));
            points.Add(new Point(mapSize.Width, mapSize.Height));
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