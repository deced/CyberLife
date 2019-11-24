using CyberLife.Simple2DWorld;
using System;
using System.Collections.Generic;

namespace CyberLife.Platform.World_content
{
    public enum Season
    {
        Winter = 0,
        Spring = 1,
        Summer = 2,
        Autumn = 3
    }
    public class SeasonsPhenomen : IPhenomen
    {

        #region field

        private Season _season;
        private int _step;
        private Place _place;

        #endregion


        #region properties

        public int Step
        {
            get { return _step; }
        }
        public Season CurSeason
        {
            get { return _season; }
        }

        public int BaseIntensity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        #endregion


        #region methods

        /// <summary>
        /// Получает пространство, на котором действует это природное явление
        /// </summary>
        /// <returns></returns>
        public Place GetItPlace()
        {
            return _place;
        }



        /// <summary>
        /// Gроверяет, попадает ли точка под воздействие этого природного явления
        /// </summary>
        /// <param name="point">Точка пространства</param>
        /// <returns>Попадает?</returns>
        public bool isIn(Point point)
        {
            return true;
        }



        /// <summary>
        /// Вызывает обновление этого природного явления на основании
        /// окружающей среды.
        /// </summary>
        /// <param name="world">Мир, для которого происходит обновление</param>
        public void Update(Simple2DWorld.Simple2DWorld world)
        {
            _step = world.Age;
            ChangeSeason();
        }



        /// <summary>
        /// Изменяет сезон в соответствии с ходом
        /// </summary>
        private void ChangeSeason()
        {
            byte season = (byte)((_step % 360) / 90);
            _season = (Season)Enum.GetValues(typeof(Season)).GetValue(season);
        }



        /// <summary>
        /// Получает эффекты воздействия этого феномена на бота.
        /// </summary>
        /// <param name="lifeForm">Бот, который получает эффекты феномена</param>
        public void GetEffects(BotLifeForm lifeForm)
        {

        }

        #endregion


        #region constructors



        /// <summary>
        /// Инициализирует экземпляр SeasonsPhenomen
        /// </summary>
        public SeasonsPhenomen()
        {
            _place = Place.Everything();
            _place.PlaceType = PlaceType.Rectangle;
            _step = 0;
        }

        #endregion
    }
}