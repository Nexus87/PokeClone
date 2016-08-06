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
        void Start(IGameComponentManager manager, IGameTypeRegistry registry);
        void Stop(IGameComponentManager engine);
    }
}
