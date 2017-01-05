using System;
using System.Collections.Generic;
using GameEngine.Entities;
using GameEngine.Graphics.Textures;
using GameEngine.TypeRegistry;

namespace GameEngine.Core.ModuleManager
{
    internal class AutofacModuleManager : IModuleManager
    {
        private readonly Dictionary<string, IModule> _registeredModules = new Dictionary<string, IModule>();

        public void RegisterModule(IModule module)
        {
            _registeredModules.Add(module.ModuleName, module);
        }


        public IGameTypeRegistry TypeRegistry { get; } = new AutofacGameTypeRegistry();


        public void AddTextureConfigurations(TextureConfigurationBuilder builder)
        {
            foreach (var modules in _registeredModules.Values)
            {
                modules.AddTextureConfigurations(builder);
            }
        }

        public void AddBuilderAndRenderer()
        {
            foreach (var module in _registeredModules.Values)
            {
                module.AddBuilderAndRenderer();
            }
        }
        public void RegisterTypes()
        {
            foreach (var modules in _registeredModules.Values)
            {
                modules.RegisterTypes(TypeRegistry);
            }
        }

        public void StartModule(string moduleName)
        {
            if (!_registeredModules.ContainsKey(moduleName))
                throw new InvalidOperationException("Unkown module name: " + moduleName);

            var componentManager = TypeRegistry.ResolveType<IGameComponentManager>();
            var inputHandlerManager = TypeRegistry.ResolveType<IInputHandlerManager>();
            _registeredModules[moduleName].Start(componentManager, inputHandlerManager, TypeRegistry);
        }
    }
}
