﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Registry
{
    [AttributeUsage(AttributeTargets.Method, Inherited=false)]
    public class ModuleInitAttribute : Attribute
    {
    }
}
