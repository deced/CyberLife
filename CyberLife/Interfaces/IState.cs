using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberLife.Interfaces
{
    public interface IState
    {
        void Update(Simple2DWorld.Simple2DWorld simple2DWorld);
    }
}
