using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Registry
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ModuleRegistrationAttribute : Attribute
    {
        public ModuleRegistrationAttribute(string moduleName)
        {
            ModuleName = moduleName;
        }

        public string ModuleName { get; set; }
    }
}
