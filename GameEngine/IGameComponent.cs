using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public interface IGameComponent
    {
        void Initialize();
        void Update(GameTime time);
    }
}
