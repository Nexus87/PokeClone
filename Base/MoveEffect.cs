﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    public interface MoveEffect
    {
        void executeEffect(ICharakter source, EffectFacade facade);
    }
}