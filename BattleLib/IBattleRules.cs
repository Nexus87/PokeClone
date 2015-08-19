using Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib
{
    public interface IBattleRules
    {
        bool canEscape();
        bool canChange();
        bool execMove(ICharakter source, Move move, ICharakter target);
    }
}
