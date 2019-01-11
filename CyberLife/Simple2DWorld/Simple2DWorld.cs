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
    public class Simple2DWorld
    {
        public delegate void ReactionDelegate();
        public const double OrganicZeroEnergyFactor = 0.1;
        public const double OrganicCollapseEnergyFactor = 0.3;
        public const double OutflowEnergyFactor = 0.01;

        #region fields

        protected Dictionary<string, IPhenomen> _naturalPhenomena;
        protected Map _map;
        protected string _name;
        protected IVisualizer _visualizer;
        protected int _age;
        private Dictionary<string, IState> _states;
        protected Dictionary<string, ReactionDelegate> _reactions;

        #endregion


        #region properties

        public Dictionary<string, IState> States { get => _states; }
        public string Name { get => _name; set => _name = value; }
        public IVisualizer Visualizer { get => _visualizer; set => _visualizer = value; }//todo
        public int Age { get { return _age; } }
        internal Dictionary<string, IPhenomen> NaturalPhenomena { get => _naturalPhenomena; }
        internal Map Map { get => _map; }

        #endregion


        #region methods

        /// <summary>
        /// Вызывает обновление мира
        /// </summary>
        public void Update()
        {
            foreach (IPhenomen phenomen in _naturalPhenomena.Values)
                phenomen.Update(this);
            foreach (var state in _states.Values)
            {
                state.Update(this);
            }
            _energyReaction();
            _genotypeReaction();
            _colorReaction();
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
        private static List<BotLifeForm> _getLifeForms(int count, int width, int height)
        {
            Dictionary<Point, BotLifeForm> lifeForms = new Dictionary<Point, BotLifeForm>(count);
            for (int i = 0; i < count; i++)
            {
                BotLifeForm bot = new BotLifeForm(width, height, lifeForms);
                lifeForms.Add(bot.Point, bot);
            }

            return lifeForms.Values.ToList();
        }



        /// <summary>
        /// Инициализирует базовые реакции Simple2DWorld
        /// </summary>
        private void _InitReactions()
        {
            if (_reactions == null)
                _reactions = new Dictionary<string, ReactionDelegate>();
            if (_reactions.Count > 0)
                return;

            _reactions.Add("EnergyReaction", _energyReaction);
            _reactions.Add("GenotypeReaction", _genotypeReaction);
            _reactions.Add("ColorReaction", _colorReaction);
        }



        /// <summary>
        /// Реакция, отвечающая за изменение состояния 
        /// EnergyState у форм жизни
        /// </summary>
        /// <param name="world">Обрабатываемый мир</param>
        private void _energyReaction()
        {
            List<Point> deadBots = new List<Point> { };
            foreach (BotLifeForm bot in Map)
            {
                if (bot.Dead)
                    deadBots.Add(bot.Point);
                bot.Updated = false;
            }
            foreach (Point botPoint in deadBots)
            {
                BotLifeForm bot = (BotLifeForm)Map.LifeForms[botPoint.X, botPoint.Y];
                Map.AddOrganic(bot);
                Map.Remove(botPoint.X, botPoint.Y);
                bot.Energy = (int)(EnergyState.MaxEnergy * (
                    bot.EnergyState == EnergyStates.EnergyCollapse ?
                        OrganicCollapseEnergyFactor :
                                      OrganicZeroEnergyFactor));
            }

        }



        /// <summary>
        /// Реакция, отвечающая за выполнение действий ботом
        /// </summary>
        private void _genotypeReaction()
        {
            const int descendantPrice = 500;
            int worldWidth = Map.Width;
            int worldHeight = Map.Height;
            BotLifeForm botOnPlace;
            int X;
            int Y;
            foreach (BotLifeForm bot in Map)
            {
                if (!bot.Updated)
                {
                    bot.Energy -= 10;
                    bot.Updated = true;
                    switch (bot.Action)
                    {
                        case Actions.Move:
                            X = bot.Point.X;
                            Y = bot.Point.Y;
                            GetXY(ref X, ref Y, bot.Direction);
                            if (Map.IsPlaceEmpty(X, Y, out botOnPlace))
                            {
                                Map.Remove(bot.Point.X, bot.Point.Y);
                                bot.Point = new Point(X, Y);
                                Map.Add(bot);
                            }
                            break;
                        case Actions.Eat:
                            X = bot.Point.X;
                            Y = bot.Point.Y;
                            GetXY(ref X, ref Y, bot.Direction);
                            if (!Map.IsPlaceEmpty(X, Y, out botOnPlace))
                            {
                                if (botOnPlace.Dead)
                                {
                                    bot.Energy += botOnPlace.Energy;
                                    Map.RemoveOrganic(botOnPlace.Point.X, botOnPlace.Point.Y);
                                }
                                else
                                {
                                    bot.Energy += (int)(botOnPlace.Energy * 0.7);
                                    Map.Remove(botOnPlace.Point.X, botOnPlace.Point.Y);
                                }
                                bot.LastEnergyActions.Enqueue(Actions.Eat);
                            }
                            break;
                        case Actions.DoDescendant:
                            X = bot.Point.X;
                            Y = bot.Point.Y;
                            GetXY(ref X, ref Y, bot.Direction);
                            if (Map.IsPlaceEmpty(X, Y, out botOnPlace) && bot.EnergyState == EnergyStates.CanReproduce)
                            {
                                Map.Add(new BotLifeForm(new Point(X, Y), ((BotLifeForm)bot)));
                                bot.Energy -= descendantPrice;
                            }
                            break;
                        case Actions.ForsedReproduction:
                            X = bot.Point.X;
                            Y = bot.Point.Y;
                            if (Map.IsAroundEmpty(ref X, ref Y))
                            {
                                Map.Add(new BotLifeForm(new Point(X, Y), ((BotLifeForm)bot)));
                                bot.Energy -= descendantPrice;
                            }
                            break;
                        case Actions.Photosynthesis:
                            NaturalPhenomena["SunPhenomen"].GetEffects(bot);
                            break;
                        case Actions.Extraction:
                            NaturalPhenomena["MineralsPhenomen"].GetEffects(bot);
                            break;
                        case Actions.CheckEnergy:
                            //todo
                            break;
                    }
                }
            }  
        }



        private void _colorReaction()
        {

        }



        /// <summary>
        /// Получает координаты X и Y из заданного направления
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="direction"></param>
        public void GetXY(ref int X, ref int Y, Directions direction)
        {
            switch (direction)
            {
                case Directions.TopLeft:
                    X--;
                    Y--;
                    break;
                case Directions.Top:
                    Y--;
                    break;
                case Directions.TopRight:
                    X++;
                    Y--;
                    break;
                case Directions.Right:
                    X++;
                    break;
                case Directions.BottomRight:
                    X++;
                    Y++;
                    break;
                case Directions.Bottom:
                    Y++;
                    break;
                case Directions.BottomLeft:
                    X--;
                    Y++;
                    break;
                case Directions.Left:
                    X--;
                    break;
                default:
                    throw new ArgumentException("Неопределенное направление  " + direction);
            }
            if (Y > _map.Height - 1)
                Y = this._map.Height - 1;
            if (Y < 0)
                Y = 0;
            if (X > this._map.Width - 1)
                X = 0;
            if (X < 0)
                X = this._map.Width - 1;
        }



        /// <summary>
        /// Производит инициализацию феноменов
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Dictionary<string, IPhenomen> GetPhenomens(int x, int y)
        {
            Dictionary<string, IPhenomen> ret = new Dictionary<string, IPhenomen> { };
            SunPhenomen sun = new SunPhenomen(x, y);
            MineralsPhenomen minerals = new MineralsPhenomen(x, y);
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
        public Simple2DWorld(string name, IVisualizer visualizer, Dictionary<string, IPhenomen> phenomens, Map map)
        {
            _map = map;
            if (name == "" || name == null)
            {
                ArgumentException ex = new ArgumentException("Name shouldn't be empty", nameof(name));
                throw ex;
            }
            if (phenomens == null)
            {
                ArgumentNullException ex = new ArgumentNullException(nameof(phenomens));
                throw ex;
            }
            if (visualizer == null)
            {
                ArgumentNullException ex = new ArgumentNullException(nameof(visualizer));
                throw ex;
            }
            if (map == null)
            {
                ArgumentNullException ex = new ArgumentNullException(nameof(map));
                throw ex;
            }
            _naturalPhenomena = phenomens;
            _visualizer = visualizer;
            _name = name;
            _reactions = new Dictionary<string, ReactionDelegate>();
            _InitReactions();
        }


        /// <summary>
        /// Производит первичную инициализацию мира
        /// </summary>
        /// <param name="width">Ширина карты мира</param>
        /// <param name="height">Высота карты мира</param>
        /// <param name="lifeFormCount">Количество форм жизни</param>
        public Simple2DWorld(int width = 1000, int height = 1000, int lifeFormCount = 100) :
            this("Simple2DWorld", new Simple2dVisualizer(), GetPhenomens(width, height), new Map(width, height, _getLifeForms(lifeFormCount, width, height), null))
        {
            _states = new Dictionary<string, IState> { };
            EnergyState energy = new EnergyState();
            GenotypeState genotype = new GenotypeState();
            ColorState color = new ColorState(ColorType.Default);
            _states.Add("EnergyState", energy);
            _states.Add("GenotypeState", genotype);
            _states.Add("ColorState", color);
        }
        #endregion
    }
}