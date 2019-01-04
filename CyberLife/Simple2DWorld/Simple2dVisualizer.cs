using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace CyberLife.Simple2DWorld
{
    public class Simple2dVisualizer : IVisualizer
    {
        #region fields

        private Bitmap map;

        #endregion


        #region properties

        public Bitmap Map { get { return map; } }

        #endregion


        #region methods

        /// <summary>
        /// Вызывает обновление цветов форм жизни
        /// </summary>
        /// <param name="metadata"></param>
        public void Update(Simple2DWorld world)
        {
            map = new Bitmap(world.Map.Width, world.Map.Height);
            foreach (BotLifeForm bot in world.Map)
            {
                map.SetPixel(bot.Point.X, bot.Point.Y, bot.Color);
            }
            foreach (BotLifeForm bot in world.Map.Organic)
            {
                if (bot != null)
                    map.SetPixel(bot.Point.X, bot.Point.Y, bot.Color);
            }
        }



        /// <summary>
        /// Получает текущую карту
        /// </summary>
        /// <returns></returns>
        public Bitmap GetMap()
        {
            return map;
        }

        #endregion


        #region constructors

        #endregion
    }
}