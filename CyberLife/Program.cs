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

        static void Main(string[] args)
        {
            BotLifeForm.MutationPercent = 25;
            Simple2DWorld.Simple2DWorld world = new Simple2DWorld.Simple2DWorld(1000, 1000, 500000);
            IVisualizer visualizer = new Simple2dVisualizer();
            world.Visualizer = visualizer;
            MainForm mainForm = new MainForm(world);
            mainForm.ShowDialog();
        }
    }
}