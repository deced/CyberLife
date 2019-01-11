using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using System.Drawing.Imaging;

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
            map = new Bitmap(world.Map.Width, world.Map.Height, PixelFormat.Format32bppArgb);
            BitmapData bmd = map.LockBits(new Rectangle(0, 0, map.Width, map.Height),
                                  ImageLockMode.ReadWrite,
                                  map.PixelFormat);
            int PixelSize = 4; 
            unsafe
            {
                for (int y = 0; y < bmd.Height; y++)
                {
                    byte* row = (byte*)bmd.Scan0 + (y * bmd.Stride);

                    for (int x = 0; x < bmd.Width; x++)
                    {
                        if (world.Map.LifeForms[x, y] != null)
                        {
                            Color color = world.Map.LifeForms[x, y].Color;
                            row[x * PixelSize] = color.B;   //Blue  
                            row[x * PixelSize + 1] = color.G; //Green 
                            row[x * PixelSize + 2] = color.R;   //Red   
                            row[x * PixelSize + 3] = 255;  //Alpha 
                        }
                        else if (world.Map.Organic[x, y] != null)
                        {
                            row[x * PixelSize] = 132;   //Blue  
                            row[x * PixelSize + 1] = 132; //Green 
                            row[x * PixelSize + 2] = 132;   //Red   
                            row[x * PixelSize + 3] = 255;  //Alpha 
                        }
                    }
                }
                map.UnlockBits(bmd);
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