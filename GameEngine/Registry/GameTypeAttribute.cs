﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Registry
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class GameTypeAttribute : Attribute
    {
    }
}
