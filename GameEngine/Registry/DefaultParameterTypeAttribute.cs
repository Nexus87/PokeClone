using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Registry
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
    public class DefaultParameterTypeAttribute : Attribute
    {
        public IReadOnlyCollection<Tuple<Type, Type>> Parameters { get; private set; }
        
        public DefaultParameterTypeAttribute(String parameterName, Type resolvedType)
        {
            ParameterName = parameterName;
            ResolveType = resolvedType;
        }

        public String ParameterName { get; set; }

        public Type ResolveType { get; set; }
    }
}
