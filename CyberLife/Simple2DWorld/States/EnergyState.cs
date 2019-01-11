using CyberLife.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberLife.Simple2DWorld
{
    public enum EnergyStates
    {
        Dead,
        CanReproduce,
        ForsedReproduction,
        EnergyCollapse,
        Alive
    }
    class EnergyState : IState
    {
        public const int MaxEnergy = 5000;

        #region fields

        #endregion


        #region properties

        #endregion


        #region methods

        /// <summary>
        /// Вызывает обновление состояния энергии всех ботов
        /// </summary>
        /// <param name="world"></param>
        public void Update(Simple2DWorld world)
        {
            int height = world.Map.LifeForms.GetLength(0);
            int width = world.Map.LifeForms.GetLength(1);
            Parallel.For(0, height, y =>
            {
            for (int x = 0; x < width; x++)
            {
                if (world.Map.LifeForms[x, y] != null)
                    {
                        world.Map.LifeForms[x, y].EnergyState = GetState(world.Map.LifeForms[x, y]);
                    }
                }
            });
        }



        /// <summary>
        /// получает флаг бота на основании текущей энергии бота
        /// </summary>
        /// <returns></returns>
        private EnergyStates GetState(BotLifeForm bot)
        {
            EnergyStates flag;
            if (bot.Energy < 0)
            {
                flag = EnergyStates.Dead;
                bot.Dead = true;
                return flag;
            }
            if (bot.Energy >= MaxEnergy)
            {
                flag = EnergyStates.EnergyCollapse;
                bot.Dead = true;
                return flag;
            }
            if (bot.Energy >= MaxEnergy * 0.6)
            {
                flag = EnergyStates.ForsedReproduction;
                return flag;
            }
            if (bot.Energy >= MaxEnergy * 0.3)
            {
                flag = EnergyStates.CanReproduce;
                return flag;
            }
            flag = EnergyStates.Alive;
            return flag;

        }

        #endregion


        #region constructors

        public EnergyState()
        {

        }

        #endregion
    }
}