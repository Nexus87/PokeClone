using System;

namespace GameEngine.TypeRegistry
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
