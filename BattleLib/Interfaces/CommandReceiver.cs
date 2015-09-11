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
        void clientExit(AbstractClient source);
        void execMove(AbstractClient source, Move move, int targetId);
        void execChange(AbstractClient source, ICharakter charakter);
    }
}
