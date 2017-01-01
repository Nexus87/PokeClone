using GameEngine.TypeRegistry;

namespace GameEngine.Core.ModuleManager
{
    internal interface IModuleManager
    {
        void RegisterModule(IModule module);
        void StartModule(string moduleName, IGameComponentManager componentManager);
        IGameTypeRegistry TypeRegistry { get; }
    }
}
