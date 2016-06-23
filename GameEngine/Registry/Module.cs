using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Registry
{
    public class Module
    {
        public IScreen Screen { get; private set; }
        public IReadOnlyList<GameEngine.IGameComponent> GameComponents { get; private set; }
    }
}
