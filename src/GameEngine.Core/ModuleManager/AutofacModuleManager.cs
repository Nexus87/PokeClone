using System.Collections.Generic;
using GameEngine.Graphics.Textures;
using GameEngine.TypeRegistry;

namespace GameEngine.Core.ModuleManager
{
    internal class AutofacModuleManager : IModuleManager
    {
        private readonly Dictionary<string, IModule> _registeredModules = new Dictionary<string, IModule>();
        private readonly List<IContentModule> _contentModules = new List<IContentModule>();

        public void RegisterModule(IModule module)
        {
            _registeredModules.Add(module.ModuleName, module);
        }

        public void RegisterContentModule(IContentModule module)
        {
            _contentModules.Add(module);
        }

        public IGameTypeRegistry TypeRegistry { get; } = new AutofacGameTypeRegistry();


        public void AddTextureConfigurations(TextureConfigurationBuilder builder)
        {
            _contentModules.ForEach(x => x.AddTextureConfigurations(builder));
        }

        public void RegisterTypes()
        {
            foreach (var modules in _registeredModules.Values)
            {
                modules.RegisterTypes(TypeRegistry);
            }
        }
    }
}
