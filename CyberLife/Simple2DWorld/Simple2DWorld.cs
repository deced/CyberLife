using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using CyberLife.Interfaces;
using CyberLife.Platform.World_content;

namespace CyberLife.Simple2DWorld
{
    public class Simple2DWorld : World
    {

        public const double OrganicZeroEnergyFactor = 0.1;
        public const double OrganicCollapseEnergyFactor = 0.3;
        public const double OutflowEnergyFactor = 0.01;

        #region fields

        private Dictionary<Point, ILifeForm> _organic;

        #endregion


        #region properties

        public Dictionary<Point, ILifeForm> Organic { get => _organic; set => _organic = value; }

        #endregion


        #region methods

        public override void Update()
        {
            foreach (IPhenomen phenomen in _naturalPhenomena.Values)
                phenomen.Update(this);
            foreach (BotLifeForm bot in LifeForms.Values.ToList()) 
            {
                bot.Update(this);
            }
            _energyReaction();
            _visualizer.Update(this);
            _age++;
        }

        /// <summary>
        /// Получает список форм жизни "Бот" для первичной инициализации 
        /// Simple2DWorld
        /// </summary>
        /// <param name="count">Число форм жизни</param>
        /// <param name="width">Ширина карты</param>
        /// <param name="height">Высота карты</param>
        /// <returns></returns>
        private static Dictionary<Point, ILifeForm> _getLifeForms(int count, int width, int height)
        {
            MapSize mapsize = new MapSize(width, height);
            Dictionary<Point, ILifeForm> lifeForms = new Dictionary<Point, ILifeForm>(count);
            for (int i = 0; i < count; i++)
            {
                BotLifeForm bot = new BotLifeForm(mapsize, lifeForms);
                lifeForms.Add(bot.Point, bot);
            }

            return lifeForms;
        }



        /// <summary>
        /// Инициализирует базовые реакции Simple2DWorld
        /// </summary>
       /* private void _InitReactions()
        {
            if (_reactions == null)
                _reactions = new Dictionary<string, ReactionDelegate>();
            if (_reactions.Count > 0)
                return;

            _reactions.Add("EnergyReaction", _energyReaction);
            _reactions.Add("GenotypeReaction", _genotypeReaction);
            _reactions.Add("ColorReaction", _colorReaction);
        }*/



        /// <summary>
        /// Реакция, отвечающая за изменение состояния 
        /// EnergyState у форм жизни
        /// </summary>
        /// <param name="world">Обрабатываемый мир</param>
        private void _energyReaction()
        {

            IEnumerable<Point> deadBots = LifeForms
                .Where(
                    x => ((BotLifeForm)x.Value).Dead == true
                ).Select(x => (Point)x.Key).ToArray();
            foreach (var botPoint in deadBots)
            {
                BotLifeForm bot = (BotLifeForm)LifeForms[botPoint];
                Organic.Add(botPoint, bot);
                LifeForms.Remove(botPoint);
                bot.Energy = (int)(5000 * (
                    bot.EnergyState == EnergyStates.EnergyCollapse ?
                        OrganicCollapseEnergyFactor :
                                      OrganicZeroEnergyFactor));
            }
                /* foreach (BotLifeForm bot in Organic.Values.ToList())
                 {
                     bot.Energy -= (int)(bot.Energy * OutflowEnergyFactor);
                 }

                 Point[] rottenOrganic = Organic.Where(x => ((BotLifeForm)x.Value).Energy <= 0)
                     .Select(x => x.Key).ToArray();

                 foreach (var botId in rottenOrganic)
                 {
                     Organic.Remove(botId);
                 }*/

            }



 
        public void GetXY(ref int X, ref int Y, Directions direction)
        {
            switch (direction)
            {
                case Directions.TopLeft:
                    X--;
                    Y++;
                    break;
                case Directions.Top:
                    Y++;
                    break;
                case Directions.TopRight:
                    X++;
                    Y++;
                    break;
                case Directions.Right:
                    X++;
                    break;
                case Directions.BottomRight:
                    X++;
                    Y--;
                    break;
                case Directions.Bottom:
                    Y--;
                    break;
                case Directions.BottomLeft:
                    X--;
                    Y--;
                    break;
                case Directions.Left:
                    X--;
                    break;
                default:
                    throw new ArgumentException("Неопределенное направление  " + direction);
            }
            if (Y > _size.Height - 1)
                Y = this._size.Height - 1;
            if (Y < 0)
                Y = 0;
            if (X > this._size.Width - 1)
                X = 0;
            if (X < 0)
                X = this._size.Width - 1;
        }

        /// <summary>
        /// Определяет,свободна ли выбранная клетка,и если нет,то возвращает форму жизни в данной клетке
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="botOnPlace"></param>
        /// <returns></returns>
        public bool IsPlaceEmpty(int X, int Y, out BotLifeForm botOnPlace)
        {
            if (LifeForms.ContainsKey(new Point(X, Y)))
            {
                botOnPlace = (BotLifeForm)LifeForms[new Point(X, Y)];
                return false;
            }
            if (Organic.ContainsKey(new Point(X, Y)))
            {
                botOnPlace = (BotLifeForm)Organic[new Point(X, Y)];
                return false;
            }
            botOnPlace = null;
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
                if (workY > Size.Height - 1)
                    workY = Size.Height - 1;
                if (workY < 0)
                    workY = 0;
                if (workX > Size.Width - 1)
                    workX = 0;
                if (workX < 0)
                    workX = Size.Width - 1;
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
                if (workY > Size.Height - 1)
                    workY = Size.Height - 1;
                if (workY < 0)
                    workY = 0;
                if (workX > Size.Width - 1)
                    workX = 0;
                if (workX < 0)
                    workX = Size.Width - 1;
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
                if (workY > Size.Height - 1)
                    workY = Size.Height - 1;
                if (workY < 0)
                    workY = 0;
                if (workX > Size.Width - 1)
                    workX = 0;
                if (workX < 0)
                    workX = Size.Width - 1;
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

        public static Dictionary<string, IPhenomen> GetPhenomens(MapSize map)
        {
            Dictionary<string, IPhenomen> ret = new Dictionary<string, IPhenomen> { };
            SunPhenomen sun = new SunPhenomen(map);
            MineralsPhenomen minerals = new MineralsPhenomen(map);
            SeasonsPhenomen seasons = new SeasonsPhenomen();
            ret.Add("SunPhenomen", sun);
            ret.Add("MineralsPhenomen", minerals);
            ret.Add("SeasonsPhenomen", seasons);
            return ret;
        }
        #endregion


        #region constructors

        /// <inheritdoc />
        /// <summary>
        /// Инициализирует Simple2DWorld из заданных параметров
        /// </summary>
        /// <param name="name">Название мира</param>
        /// <param name="environment">Окружающая среда мира</param>
        /// <param name="visualizer">Визуализатор для мира</param>
        /// <param name="lifeForms">Формы жизни</param>
        /// <param name="organic">"мертвые" формы жизни, являющиеся органикой</param>
        public Simple2DWorld(string name, IVisualizer visualizer, Dictionary<Point, ILifeForm> lifeForms, Dictionary<Point, ILifeForm> organic, Dictionary<string, IPhenomen> phenomens, MapSize map) :
            base(name, visualizer, lifeForms, phenomens, map)
        {
            _organic = new Dictionary<Point, ILifeForm>();
            if (organic != null)
            {
                foreach (var pair in organic)
                {
                    _organic.Add(pair.Key, pair.Value);
                }
            }
            //_InitReactions();

        }


        /// <summary>
        /// Производит первичную инициализацию мира
        /// </summary>
        /// <param name="width">Ширина карты мира</param>
        /// <param name="height">Высота карты мира</param>
        /// <param name="lifeFormCount">Количество форм жизни</param>
        public Simple2DWorld(int width = 1000, int height = 1000, int lifeFormCount = 100) :
            this("Simple2DWorld", new Simple2dVisualizer(), _getLifeForms(lifeFormCount, width, height), null, GetPhenomens(new MapSize(width, height)), new MapSize(width, height))
        {

        }
        #endregion
    }
}