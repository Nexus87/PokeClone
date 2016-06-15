using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Registry
{
    [AttributeUsage(AttributeTargets.Constructor)]
    public class ConstructorParameterAttribute : Attribute
    {
        public IReadOnlyCollection<Tuple<Type, Type>> Parameters { get; private set; }
        
        public ConstructorParameterAttribute(params Tuple<Type, Type>[] defaultParameter)
        {
            Parameters = defaultParameter;
        }
    }
}
