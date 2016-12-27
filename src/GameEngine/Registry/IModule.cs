using GameEngine.TypeRegistry;

namespace GameEngine.Core.Registry
{
    public interface IModule
    {
        string ModuleName { get; }
        void RegisterTypes(IGameTypeRegistry registry);
        void Start(IGameComponentManager manager, IGameTypeRegistry registry);
        void Stop(IGameComponentManager engine);
    }
}
