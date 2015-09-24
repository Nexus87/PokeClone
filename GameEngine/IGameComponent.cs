using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    interface IGameComponent
    {
        void Start();
        void Update();
        void Stop();

        void Pause();
        void Resume();
    }
}
