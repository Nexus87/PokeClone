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
        bool CanEscape();
        bool CanChange();
        bool ExecMove(ICharacter source, Move move, ICharacter target);
    }
}
