using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    public enum State{
        HP,
        Atk,
        Def,
        SpAtk,
        SpDef,
        Speed
    }
    public interface EffectFacade
    {
        void manipulateTargetState(State stat, int modifier);
		void manipulateSourceState(State stat, int modifier);
        void manipulateTargetCondition(StatusCondition condition);
		void manipulateSourceCondition(StatusCondition condition);

        ICharakter getCharakter();
    }
}
