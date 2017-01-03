using GameEngine.Components;
using GameEngine.Graphics.Textures;
using GameEngine.TypeRegistry;

namespace GameEngine.Core.ModuleManager
{
    public interface IModule
    {
        string ModuleName { get; }
        void RegisterTypes(IGameTypeRegistry registry);
        void AddTextureConfigurations(TextureConfigurationBuilder builder);
        void Start(IGameComponentManager manager, IInputHandlerManager inputHandlerManager, IGameTypeRegistry registry);
        void Stop(IGameComponentManager engine, IInputHandlerManager inputHandlerManager);
    }
}
