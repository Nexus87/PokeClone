using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Registry
{
    public interface IModuleRegistry
    {
        void RegisterModule(Assembly moduleAssembly);
        IReadOnlyList<string> RegisteredModuleNames { get; }
    }
}
