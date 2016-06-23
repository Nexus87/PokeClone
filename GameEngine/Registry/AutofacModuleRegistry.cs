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
        private IGameTypeRegistry registry = new AutofacGameTypeRegistry();
        private List<string> registeredModules = new List<string>();

        public void RegisterModule(Assembly moduleAssembly)
        {
            foreach (var type in moduleAssembly.GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(IModule))))
            {
                var module = (IModule) Activator.CreateInstance(type);
                registeredModules.Add(module.ModuleName);
                module.RegisterTypes(registry);
            }
        }


        public IReadOnlyList<string> RegisteredModuleNames
        {
            get { return registeredModules.AsReadOnly(); }
        }
    }
}
