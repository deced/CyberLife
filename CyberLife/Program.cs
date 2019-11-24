using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CyberLife.Simple2DWorld;

namespace CyberLife
{
    class Program
    {
        #region Параметры мира

        static int sizeX = 150; // размер мира по X

        static int sizeY = 100; // размер мира по Y

        static int lifeFormsCount = 5000; // количество форм жизни, создающихся при старте мира

        #endregion

        static void Main(string[] args)
        {
            BotLifeForm.MutationPercent = 25; // шанс мутации
            if (sizeX % 2 != 0)
                sizeX++;
            if (sizeY % 2 != 0)
                sizeY++;
            Simple2DWorld.Simple2DWorld world = new Simple2DWorld.Simple2DWorld(sizeX, sizeY, lifeFormsCount); // создаём мир с размерами (sizeX;sizeY) с lifeFormsCount формами жизни
            IVisualizer visualizer = new Simple2dVisualizer();
            world.Visualizer = visualizer;
            MainForm mainForm = new MainForm(world);
            mainForm.ShowDialog();
        }
    }
}