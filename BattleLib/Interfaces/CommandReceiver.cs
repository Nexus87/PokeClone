using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base;

namespace BattleLib.Interfaces
{
    public interface CommandReceiver
    {
        void clientExit(IBattleClient source);
        void execMove(IBattleClient source, Move move, int targetId);
        void execChange(IBattleClient source, ICharakter charakter);
    }
}
