using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberLife.Interfaces
{
    public interface IState
    {
        /// <summary>
        /// Вызывает обновление мира
        /// </summary>
        /// <param name="simple2DWorld">Мир, для которого происходит обновление</param>
        void Update(Simple2DWorld.Simple2DWorld simple2DWorld);
    }
}
