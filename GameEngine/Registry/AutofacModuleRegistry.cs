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
        public void RegisterModule(Assembly moduleAssembly)
        {
            foreach (var type in moduleAssembly.GetTypes()
                .Where(t => Attribute.IsDefined(t, typeof(ModuleRegistrationAttribute))))
            {
                var method = type.GetMethods()
                    .Where(m => Attribute.IsDefined(m, typeof(ModuleInitAttribute)))
                    .First();

                method.Invoke(Activator.CreateInstance(type), new object[]{registry});
            }

                

        }
    }
}
