using System.Collections.Generic;
using System.Reflection;
using GameEngine.TypeRegistry;

namespace GameEngine.Core.ModuleManager
{
    public static class Extensions
    {
        public static void ScanAssemblies(this IGameTypeRegistry registry, IEnumerable<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
            {
                registry.ScanAssembly(assembly);
            }
        }
    }
}