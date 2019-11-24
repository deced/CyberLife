using CyberLife.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberLife.Simple2DWorld
{
    public enum ColorType
    {
        Default = 0,
        EnergyDisplay = 1,
        GenomDisplay
    }

    class ColorState : IState
    {

        #region fields

        private ColorType _colorType;

        #endregion


        #region properties

        public ColorType ColorType
        {
            get { return _colorType; }
            set { _colorType = value; }
        }

        #endregion


        #region methods

        /// <summary>
        /// Обновляет информацию о последних действиях получения энергии формы жизни
        /// </summary>
        /// <param name="world">Мир, для которого происходит обновление</param>
        public void Update(Simple2DWorld world)
        {
            int height = world.Map.LifeForms.GetLength(1);
            int width = world.Map.LifeForms.GetLength(0);
            Parallel.For(0, height, y =>
            {
                for(int x = 0;x < width;x++)
                {
                    BotLifeForm bot = world.Map.LifeForms[x, y];
                    if (world.Map.LifeForms[x, y] != null)
                    {
                        if (!bot.Dead)
                        {
                            if (bot.LastEnergyActions.Count >= 15)
                                bot.LastEnergyActions.Dequeue();
                            SetRGB(bot);
                        }
                        else
                        {
                            bot.Color = Color.FromArgb(132, 132, 132);
                        }
                    }
                }
            });
        }



        /// <summary>
        /// Устанавливает RGB в соответствии с последними действиями формы жизни
        /// </summary>
        public void SetRGB(BotLifeForm bot)
        {
            const int MaxBotEnergy = 1500;
            byte R = 0;
            byte G = 0;
            byte B = 0;
            byte part = 0;
            switch (_colorType)
            {
                case ColorType.Default:
                    foreach (Actions Action in bot.LastEnergyActions)
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
                    bot.Color = Color.FromArgb((part * R), (part * G), (part * B));
                    break;
                case ColorType.EnergyDisplay:
                    R = 255;
                    G = (byte)(255 - (bot.Energy / (double)MaxBotEnergy) * 255);
                    bot.Color = Color.FromArgb(R, G, B);
                    break;
                case ColorType.GenomDisplay: 
                        R = (byte)bot.FriendId;
                        G = (byte)(bot.FriendId* bot.FriendId);
                        B = (byte)(bot.FriendId /Math.E);
                    bot.Color = Color.FromArgb(R,G,B);
                    break;
            }
        }

        #endregion


        #region constructors

        public ColorState( ColorType colorType)
        {
            this._colorType = colorType;
        }

        #endregion
    }
}