using GameEngine.Entities;
using GameEngine.Graphics.Textures;
using GameEngine.TypeRegistry;

namespace GameEngine.Core.ModuleManager
{
    public interface IModule
    {
        string ModuleName { get; }
        void RegisterTypes(IGameTypeRegistry registry);
        void AddTextureConfigurations(TextureConfigurationBuilder builder);
        void AddBuilderAndRenderer();
        void Start(IGameComponentManager manager, IInputHandlerManager inputHandlerManager, IGameTypeRegistry registry);
        void Stop(IGameComponentManager engine, IInputHandlerManager inputHandlerManager);
    }
}
