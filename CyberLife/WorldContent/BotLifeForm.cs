using CyberLife.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberLife.Simple2DWorld
{
    public enum Directions
    {
        TopLeft = 1,
        Top,
        TopRight,
        Right,
        BottomRight,
        Bottom,
        BottomLeft,
        Left = 8,

        None = 0
    }
    public enum Actions
    {
        CheckEnergy = 1,
        Photosynthesis,
        Move,
        Eat,
        DoDescendant,
        Extraction,
        ForsedReproduction = 7,
        None = 0
    }
    public enum ColorType
    {
        Default = 0,
        EnergyDisplay = 1
    }
    public enum EnergyStates
    {
        Dead,
        CanReproduce,
        ForsedReproduction,
        EnergyCollapse,
        Alive
    }
    public class BotLifeForm : ILifeForm
    {
        static Random rnd = new Random();
        public static ColorType colorType;
        delegate void Action(Simple2DWorld world);
        #region fields

        Action _action;
        private List<byte> _genom;
        private Color _color;
        private Queue<Actions> _lastEnergyActions;
        private byte _YTK;
        private Directions _direction;
        private bool _dead;
        private EnergyStates _energyState;
        private int _energy;
        private Point _point;
        private static byte _mutationPercent;

        #endregion


        #region properties

        public List<byte> Genom { get => _genom; set => _genom = value; }
        public Color Color { get => _color; set => _color = value; }
        public Queue<Actions> LastEnergyActions { get => _lastEnergyActions; set => _lastEnergyActions = value; }
        public byte YTK { get => _YTK; set => _YTK = value; }
        public Directions Direction { get => _direction; set => _direction = value; }
        public bool Dead { get => _dead; set => _dead = value; }
        public EnergyStates EnergyState { get => _energyState; set => _energyState = value; }
        public int Energy { get => _energy; set => _energy = value; }
        public Point Point { get => _point; set => _point = value; }
        public static byte MutationPercent { get => _mutationPercent; set => _mutationPercent = value; }

        #endregion


        #region methods

        public List<byte> GetCommonGenom()
        {
           // Random random = new Random();
            List<byte> Genom = new List<byte>
             { 6,6,4,5,5,6,3,3,6,5,2,3,6,4,4,4,3,4,2,2,5,3,4,4,3,5,3,4,4,6,3,2,4,2,5,5,3,6,2,6,6,4,6,2,5,4,2,4,2,2,4,5,2,3,6,4,4,2,4,2,5,6,5,6 };
            //List<int> workingList = Enumerable.Repeat(2, 64).ToList();
            /*                     .Concat(Enumerable.Repeat(2, 10))
                                  .Concat(Enumerable.Repeat(3, 10))
                                  .Concat(Enumerable.Repeat(4, 10))
                                  .Concat(Enumerable.Repeat(5, 10))
                                .Concat(Enumerable.Repeat(6, 14)).ToList();*/
            //  foreach (int i in workingList)
            //  {
            //    Genom.Add(Convert.ToByte(i));
            // }
            return Genom;
        }

        public void Update(Simple2DWorld world)
        {
            GetEnergyState();
            GetAction();
            _action(world);
            SetColor();
            Energy -= 10;
        }

        public void SetColor()
        {
            if (!Dead)
            {
                if (LastEnergyActions.Count >= 15)
                    LastEnergyActions.Dequeue();
                const int MaxBotEnergy = 5000;
                byte R = 0;
                byte G = 0;
                byte B = 0;
                byte part = 0;
                switch (colorType)
                {
                    case ColorType.Default:
                        foreach (Actions Action in LastEnergyActions)
                        {
                            switch (Action)
                            {
                                case Actions.Extraction:
                                    B++;
                                    break;
                                case Actions.Photosynthesis:
                                    G++;
                                    break;
                                case Actions.Eat:
                                    R++;
                                    break;
                            }
                        }
                        if (R < 0 || G < 0 || B < 0)
                            throw new ArgumentException("Один из параметров RGB был отрицательным");
                        if (R + G + B != 0)
                            part = Convert.ToByte(255 / (R + G + B));
                        Color = Color.FromArgb((part * R), (part * G), (part * B));
                        break;
                    case ColorType.EnergyDisplay:
                        R = 255;
                        G = (byte)(255 - (Energy / (double)MaxBotEnergy) * 255);
                        Color = Color.FromArgb(R, G, B);
                        break;
                }
            }
            else
            {
                Color = Color.FromArgb(132, 132, 132);
            }
        }

        public void NextStep()
        {
            YTK = (byte)((YTK + 1) % 64);
        }

        public void GetAction()
        {
            if (EnergyState == EnergyStates.ForsedReproduction)
            {
                _action = ForsedReproduction;
                NextStep();
            }
            else
            {
                switch (Genom[YTK])
                {
                    case 1:
                        _action = None;
                        NextStep();
                        Direction = (Directions)((Genom[YTK] / 8) + 1);
                        NextStep();
                        // SetAction(lifeForm);
                        break;
                    case 2:
                        _action = Photosynthesis;
                        NextStep();
                        break;
                    case 3:
                        _action = Extraction;
                        NextStep();
                        break;
                    case 4:
                        _action = DoDescendant;
                        NextStep();
                        Direction = (Directions)((Genom[YTK] / 8) + 1);
                        NextStep();
                        break;
                    case 5:
                        _action = Eat;
                        NextStep();
                        Direction = (Directions)((Genom[YTK] / 8) + 1);
                        NextStep();
                        break;
                    case 6:
                        _action = Move;
                        NextStep();
                        Direction = (Directions)((Genom[YTK] / 8) + 1);
                        NextStep();
                        break;


                    default:
                        try
                        {
                            YTK = Convert.ToByte(((YTK + Genom[YTK]) % 63));
                        }
                        catch (Exception e)
                        {
                            throw new ArgumentException("Недопустимое значение YTK", (((YTK + Genom[YTK]) % 63)).ToString(), e);
                        }
                        _action = None;
                        Direction = Directions.None;
                        NextStep();
                        // SetAction(lifeForm);

                        break;
                }
            }
        }

        public void GetEnergyState()
        {
            GetState();
        }

        private byte GetState()
        {
            int MaxEnergy = 5000;
            if (Energy < 0)
            {
                EnergyState = EnergyStates.Dead;
                Dead = true;
                return 0;
            }
            if (Energy >= MaxEnergy)
            {
                EnergyState = EnergyStates.EnergyCollapse;
                Dead = true;
                return 0;
            }
            if (Energy >= MaxEnergy * 0.6)
            {
                EnergyState = EnergyStates.ForsedReproduction;
                return 0;
            }
            if (Energy >= MaxEnergy * 0.3)
            {
                EnergyState = EnergyStates.CanReproduce;
                return 0;
            }
            EnergyState = EnergyStates.Alive;
            return 0;
        }

        #region actions

        public void ForsedReproduction(Simple2DWorld world)
        {
            int X = Point.X;
            int Y = Point.Y;
            if (world.IsAroundEmpty(ref X, ref Y))
            {
                BotLifeForm lifeForm = new BotLifeForm(new Point(X, Y), this);
                world.LifeForms.Add(lifeForm.Point, lifeForm);
                Energy -= 500;
            }
        }

        public void DoDescendant(Simple2DWorld world)
        {
            if (EnergyState == EnergyStates.CanReproduce)
            {
                int X = Point.X;
                int Y = Point.Y;
                BotLifeForm bot;
                world.GetXY(ref X, ref Y, Direction);
                if (world.IsPlaceEmpty(X, Y, out bot))
                {
                    BotLifeForm lifeForm = new BotLifeForm(new Point(X, Y), this);
                    world.LifeForms.Add(lifeForm.Point, lifeForm);
                    Energy -= 500;
                }
            }
        }

        public void None(Simple2DWorld world) { }

        public void Move(Simple2DWorld world)
        {
            BotLifeForm bot;
            int X = Point.X;
            int Y = Point.Y;
            world.GetXY(ref X, ref Y, Direction);
            if (world.IsPlaceEmpty(X, Y, out bot))
            {
                world.LifeForms.Remove(Point);
                Point = new Point(X, Y);
                world.LifeForms.Add(Point, this);
            }
        }

        public void Eat(Simple2DWorld world)
        {
            int X = Point.X;
            int Y = Point.Y;
            BotLifeForm bot;
            world.GetXY(ref X, ref Y, Direction);
            if (!world.IsPlaceEmpty(X, Y, out bot))
            {
                if (bot.Dead)
                {
                    bot.Energy += bot.Energy;
                    world.Organic.Remove(bot.Point);
                }
                else
                {
                    bot.Energy += (int)(bot.Energy * 0.7);
                    world.LifeForms.Remove(bot.Point);
                }
                LastEnergyActions.Enqueue(Actions.Eat);
            }
        }

        public void Extraction(Simple2DWorld world)
        {
            int effect = world.NaturalPhenomena["MineralsPhenomen"].GetEffects(Point);
            if (effect != 0)
            {
                LastEnergyActions.Enqueue(Actions.Extraction);
                Energy += effect;
            }
        }

        public void Photosynthesis(Simple2DWorld world)
        {
            int effect = world.NaturalPhenomena["SunPhenomen"].GetEffects(Point);
            if (effect != 0)
            {
                LastEnergyActions.Enqueue(Actions.Photosynthesis);
                Energy += effect;
            }
        }

        #endregion

        #endregion


        #region constructors

        /// <inheritdoc />
        /// <summary>
        /// Инициализирует форму жизни "Бот",используется при создании мира
        /// </summary>
        /// <param name="place">Пространство, занимаемое ботом</param>
        public BotLifeForm(Point point) 
        {
            Dead = false;
            Genom = GetCommonGenom();
            LastEnergyActions = new Queue<Actions> { };
            Energy = 300;
            Point = point;
        }

        /// <summary>
        /// Инициализирует форму жизни "Бот",используется при отпочковывании
        /// </summary>
        /// <param name="point"></param>
        /// <param name="byBot"></param>
        public BotLifeForm(Point point, BotLifeForm byBot)
        {
            Point = point;
            Dead = false;
            LastEnergyActions = new Queue<Actions> { };
            if (byBot.LastEnergyActions.Count != 0)
            {
                LastEnergyActions.Enqueue(byBot.LastEnergyActions.Peek());
            }
            Genom = new List<byte> { };
            foreach (byte i in byBot.Genom)
            {
                Genom.Add(i);
            }
            Color = byBot.Color;
            if (rnd.Next(100) < MutationPercent) 
            {
                Genom[rnd.Next(0, 64)] = (Byte)rnd.Next(0, 64);
            }
            Energy = 300;
        }
        /// <summary>
        /// Инициирует бота базовыми состояниями и случайной точкой на карте.
        /// </summary>
        /// <param name="mapsize">Размер карты</param>
        public BotLifeForm(MapSize mapsize, Dictionary<Point, ILifeForm> bots) : this(Point.RandomPoint(mapsize, bots))
        {

        }

        #endregion
    }
}
