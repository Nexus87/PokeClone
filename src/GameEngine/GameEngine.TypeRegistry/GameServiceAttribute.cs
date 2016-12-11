using System;

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
