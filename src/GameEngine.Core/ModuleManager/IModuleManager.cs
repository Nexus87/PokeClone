using GameEngine.Graphics;
using GameEngine.TypeRegistry;

namespace GameEngine.Core.ModuleManager
{
    internal interface IModuleManager
    {
        void RegisterModule(IModule module);
        void StartModule(string moduleName);
        IGameTypeRegistry TypeRegistry { get; }
        void RegisterTypes();
        void AddTextureConfigurations(TextureConfigurationBuilder builder);
    }
}
