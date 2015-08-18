using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    public enum Stats{
        HP,
        Atk,
        Def,
        SpAtk,
        SpDef,
        Speed
    }
    interface EffectFacade
    {
        void manipulateState(Stats stat, int modifier);
        void manipulateCondition(StatusCondition condition);

        ICharakter getCharakter();
    }
}
