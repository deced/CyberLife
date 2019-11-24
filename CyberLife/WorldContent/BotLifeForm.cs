using System;
using System.Collections.Generic;
using System.Drawing;

namespace CyberLife.Simple2DWorld
{
    public class BotLifeForm
    {
        public const int FriendlyMutations = 2; // количество мутаций, в пределах которых бот не становится чужим
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
        private byte _mutationCount;
        private int _friendId;
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

        public bool Updated { get; set; }
        public int FriendId { get => _friendId; set => _friendId = value; }

        #endregion


        #region methods

        /// <summary>
        /// Определяет, принадлежат ли боты к одной колонии
        /// </summary>
        /// <param name="friend">Бот, чей геном сравнивается с текущим экзмепляром</param>
        /// <returns>Принадлежат?</returns>
        public bool IsFriend(BotLifeForm friend)
        {
            if (friend.FriendId == this.FriendId)
                return true;
            return false;
        }



        /// <summary>
        /// Возвращает стандартный геном, используется при создании мира
        /// </summary>
        /// <returns>Стандартный геном бота</returns>
        public List<byte> GetCommonGenom()
        {
            Random random = new Random();
            List<byte> genom = new List<byte> { };
            for (int i = 0; i < 64; i++)
            {
                if (random.Next(3) != 1)
                 genom.Add((byte)random.Next(7));
                 else
                   genom.Add((byte)random.Next(64));
            }            
            return genom;
        }



        /// <summary>
        /// Возвращает строковое представление бота
        /// </summary>
        /// <returns>Строковое представление бота</returns>
        public override string ToString()
        {
            return "[" + Point.X.ToString() + "|" + Point.Y.ToString() + "] Action: " + Action.ToString() + "; Updated: " + Updated;
        }

        #endregion


        #region constructors


        /// <summary>
        /// Инициализирует форму жизни "Бот",используется при создании мира
        /// </summary>
        /// <param name="point">Точка, занимаемая ботом</param>
        public BotLifeForm(Point point)
        {
            _mutationCount = 0;
            Dead = false;
            Genom = GetCommonGenom();
            FriendId = Genom.GetHashCode();
            LastEnergyActions = new Queue<Actions> { };
            Energy = 300;
            Point = point;
            Updated = false;
        }


        /// <summary>
        /// Инициализирует форму жизни "Бот",используется при отпочковывании
        /// </summary>
        /// <param name="point">Точка, потомка</param>
        /// <param name="byBot">Родитель</param>
        public BotLifeForm(Point point, BotLifeForm byBot)
        {
            FriendId = byBot.FriendId;
            _mutationCount = byBot._mutationCount;
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
                _mutationCount++;
                if (rnd.Next(3) != 1)
                    Genom[rnd.Next(0, 64)] = (Byte)rnd.Next(0, 7);
                else
                    Genom[rnd.Next(0, 64)] = (Byte)rnd.Next(0, 64);
            }
            if (_mutationCount >= FriendlyMutations)
            {
                _mutationCount = 0;
                FriendId = Genom.GetHashCode();
            }
            Energy = 300;
            Updated = true;
        }


        /// <summary>
        /// Инициирует бота базовыми состояниями и случайной точкой на карте.
        /// </summary>
        /// <param name="x">Размер мира по X</param>
        /// <param name="y">Размер мира по Y</param>
        /// <param name="bots">Список ботов</param>
        public BotLifeForm(int x, int y, Dictionary<Point, BotLifeForm> bots) : this(Point.RandomPoint(x, y, bots))
        {

        }

        #endregion
    }
}
