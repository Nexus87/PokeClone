using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Registry
{
    [AttributeUsage(AttributeTargets.Class)]
    public class GameComponentAttribute : Attribute
    {
        public GameComponentAttribute(Type registerType = null, bool singleInstance = false)
        {
            RegisterType = registerType;
            SingleInstance = singleInstance;
        }
        public Type RegisterType { get; set; }
        public bool SingleInstance { get; set; }
    }
}
