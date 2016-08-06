using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Registry
{
    public interface IModuleRegistry
    {
        void RegisterModule(Assembly moduleAssembly);
        void RegisterModule(IModule module);
        void StartModule(string moduleName, IGameComponentManager componentManager);
        IReadOnlyList<string> RegisteredModuleNames { get; }
        IGameTypeRegistry TypeRegistry { get; }
    }
}
