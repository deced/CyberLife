using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberLife.Interfaces
{
    public interface ILifeForm
    {
        void Update(Simple2DWorld.Simple2DWorld world);
        Point Point
        { get; }
    }
}
