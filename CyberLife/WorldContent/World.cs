using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using CyberLife;
using CyberLife.Simple2DWorld;
using CyberLife.Interfaces;

namespace CyberLife
{
    /// <summary>
    /// Реаилзует цельный мир.
    /// </summary>
    public class World
    { 

        #region fields

        protected Dictionary<string, IPhenomen> _naturalPhenomena;
        protected MapSize _size;
        protected string _name;
        protected IVisualizer _visualizer;
        protected Dictionary<Point, ILifeForm> _lifeForms;
        protected int _age;

        #endregion


        #region properties

        public string Name { get => _name; set => _name = value; }
        public Dictionary<Point, ILifeForm> LifeForms { get => _lifeForms; }
        public IVisualizer Visualizer { get => _visualizer; set => _visualizer = value; }//todo
        public int Age { get { return _age; } }
        internal Dictionary<string, IPhenomen> NaturalPhenomena { get => _naturalPhenomena; }
        internal MapSize Size { get => _size; }

        #endregion


        #region methods

        /// <summary>
        /// Вызывает обновление всех компонентов мира. 
        /// </summary>
        public virtual void Update()
        {
            // nothing?
        }

        #endregion


        #region constructors
        /// <summary>
        /// Инициализирует мир на основе его компонентов
        /// </summary>
        /// <param name="name">Название мира</param>
        /// <param name="environment">Окружающая среда для этого мира</param>
        /// <param name="visualizer">Визуализатор, предназначенный для отрисовки компонентов мира</param>
        /// <param name="lifeForms">Формы жизни</param>
        public World(string name, IVisualizer visualizer, Dictionary<Point, ILifeForm> lifeForms, Dictionary<string, IPhenomen> phenomens, MapSize mapSize)
        {
            if (lifeForms == null)
            {
                ArgumentNullException ex = new ArgumentNullException(nameof(lifeForms));
                throw ex;
            }

            if (lifeForms.Count == 0)
            {
                ArgumentException ex = new ArgumentException("List of life forms shouldn\t be empty.", nameof(lifeForms));
                throw ex;
            }

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
            if (mapSize == null)
            {
                ArgumentNullException ex = new ArgumentNullException(nameof(mapSize));
                throw ex;
            }
            _size = mapSize;
            _naturalPhenomena = phenomens;
            _visualizer = visualizer;
            _name = name;
            _lifeForms = new Dictionary<Point, ILifeForm>();
            foreach (var pair in lifeForms) 
            {
                _lifeForms.Add(pair.Key, pair.Value);
            }

        }

        #endregion
    }
}