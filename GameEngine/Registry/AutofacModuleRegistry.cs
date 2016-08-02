using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Registry
{
    public class AutofacModuleRegistry : IModuleRegistry
    {
        readonly IGameTypeRegistry registry = new AutofacGameTypeRegistry();
        readonly Dictionary<string, IModule> registeredModules = new Dictionary<string, IModule>();

        public void RegisterModule(Assembly moduleAssembly)
        {
            foreach (var type in moduleAssembly.GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(IModule))))
            {
                var module = (IModule) Activator.CreateInstance(type);
                RegisterModule(module);
            }
        }

        public void RegisterModule(IModule module)
        {
            registeredModules.Add(module.ModuleName, module);
            module.RegisterTypes(registry);
        }

        public IReadOnlyList<string> RegisteredModuleNames
        {
            get { return registeredModules.Keys.ToList(); }
        }


        public IGameTypeRegistry TypeRegistry {get{return registry;}}


        public void StartModule(string moduleName, PokeEngine engine)
        {
            if (!registeredModules.ContainsKey(moduleName))
                throw new InvalidOperationException("Unkown module name: " + moduleName);

            registeredModules[moduleName].Start(engine, registry);
        }
    }
}
