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
        /// <param name="worldMetadata"></param>
        public void Update(Simple2DWorld world)
        {
            int height = world.Map.LifeForms.GetLength(0);
            int width = world.Map.LifeForms.GetLength(1);
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
            const int MaxBotEnergy = 5000;
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
                case ColorType.GenomDisplay: // переделать
                    for (int i = 0; i < 64; i++) 
                    {
                        if (i + 2 >= 64)
                            break;
                        R += Convert.ToByte((bot.Genom[i] % 2));
                        G += Convert.ToByte((bot.Genom[i + 1] % 3));
                        B += Convert.ToByte((bot.Genom[i + 2] % 5));
                        i += 2;
                    }
                    bot.Color = Color.FromArgb(R*11, G*6, B*3);
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