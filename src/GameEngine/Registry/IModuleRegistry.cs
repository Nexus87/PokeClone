using System.Collections.Generic;
using System.Reflection;

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
