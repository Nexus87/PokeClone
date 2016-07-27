using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Registry
{
    [AttributeUsage(AttributeTargets.Class)]
    public class GameServiceAttribute : Attribute
    {
        public GameServiceAttribute(Type serviceType)
        {
            ServiceType = serviceType;
        }

        public Type ServiceType { get; private set; }
    }
}
