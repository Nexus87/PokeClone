using GameEngine.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public interface IModule
    {
        string ModuleName { get; }
        void RegisterTypes(IGameTypeRegistry registry);
        void Start(PokeEngine engine, IGameTypeRegistry registry);
        void Stop(PokeEngine engine);
    }
}
