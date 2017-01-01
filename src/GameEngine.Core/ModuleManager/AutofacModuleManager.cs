using System;
using System.Collections.Generic;
using GameEngine.TypeRegistry;

namespace GameEngine.Core.ModuleManager
{
    internal class AutofacModuleManager : IModuleManager
    {
        private readonly Dictionary<string, IModule> _registeredModules = new Dictionary<string, IModule>();

        public void RegisterModule(IModule module)
        {
            _registeredModules.Add(module.ModuleName, module);
            module.RegisterTypes(TypeRegistry);
        }


        public IGameTypeRegistry TypeRegistry { get; } = new AutofacGameTypeRegistry();


        public void StartModule(string moduleName, IGameComponentManager componentManager)
        {
            if (!_registeredModules.ContainsKey(moduleName))
                throw new InvalidOperationException("Unkown module name: " + moduleName);

            _registeredModules[moduleName].Start(componentManager, TypeRegistry);
        }
    }
}
