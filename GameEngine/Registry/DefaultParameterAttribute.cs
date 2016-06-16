using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Registry
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
    public class DefaultParameterAttribute : Attribute
    {
        public String ParameterName { get; set; }
        public Object ResourceKey { get; set; }
        
        public DefaultParameterAttribute(String parameterName, Object resourceKey)
        {
            ParameterName = parameterName;
            ResourceKey = resourceKey;
        }
    }
}
