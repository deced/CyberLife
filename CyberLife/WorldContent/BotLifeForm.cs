using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberLife.Simple2DWorld
{
    public class BotLifeForm
    {
        static Random rnd = new Random();
        #region fields

        private int _energy;
        private Point _point;
        private List<byte> _genom;
        private byte _YTK;
        private EnergyStates _energyState;
        private bool _dead;

        private Color _color;
        private Queue<Actions> _lastEnergyActions;

        private Directions _direction;
        private Actions _action;

        private static byte _mutationPercent;

        #endregion


        #region properties

        public int Energy { get => _energy; set => _energy = value; }
        public Point Point { get => _point; set => _point = value; }
        public List<byte> Genom { get => _genom; set => _genom = value; }
        public byte YTK { get => _YTK; set => _YTK = value; }
        public EnergyStates EnergyState { get => _energyState; set => _energyState = value; }
        public bool Dead { get => _dead; set => _dead = value; }

        public Color Color { get => _color; set => _color = value; }
        public Queue<Actions> LastEnergyActions { get => _lastEnergyActions; set => _lastEnergyActions = value; }

        public Directions Direction { get => _direction; set => _direction = value; }
        public Actions Action { get => _action; set => _action = value; }

        public static byte MutationPercent { get => _mutationPercent; set => _mutationPercent = value; }

        public bool Updated { get; set; } // убрать

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



        public override string ToString()
        {
            return "[" + Point.X.ToString() + "|" + Point.Y.ToString() + "] Action: " + Action.ToString(); ;
        }

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
            Updated = false;
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
                if (rnd.Next(2) == 1)
                    Genom[rnd.Next(0, 64)] = (Byte)rnd.Next(0, 7);
                else
                    Genom[rnd.Next(0, 64)] = (Byte)rnd.Next(0, 64);
            }
            Energy = 300;
            Updated = true;
        }


        /// <summary>
        /// Инициирует бота базовыми состояниями и случайной точкой на карте.
        /// </summary>
        /// <param name="mapsize">Размер карты</param>
        public BotLifeForm(int x,int y, Dictionary<Point, BotLifeForm> bots) : this(Point.RandomPoint(x,y, bots))
        {

        }

        #endregion
    }
}
