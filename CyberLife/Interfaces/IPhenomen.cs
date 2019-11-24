using System.Collections.Generic;
using System;
using CyberLife.Simple2DWorld;

namespace CyberLife
{
    public interface IPhenomen
    {
        int BaseIntensity
        {
            get;
            set;
        }

        /// <summary>
        /// Вызывает обновление этого природного явления на основании
        /// данных окружающей среды.
        /// </summary>
        /// <param name="world">Мир, для которого происходит обновление</param>
        void Update(Simple2DWorld.Simple2DWorld world);

        /// <summary>
        /// Получает эффекты воздействия этого феномена на бота.
        /// </summary>
        /// <param name="lifeForm">Бот, который получает эффекты феномена</param>
        void GetEffects(BotLifeForm lifeForm);


        /// <summary>
        /// Проверяет, попадает ли точка под воздействие этого природного явления
        /// </summary>
        /// <param name="point">Точка пространства</param>
        /// <returns>Попадает?</returns>
        bool isIn(Point point);


        /// <summary>
        /// Получает пространство, на котором действует это природное явление
        /// </summary>
        /// <returns></returns>
        Place GetItPlace();

    }
}