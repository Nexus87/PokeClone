using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base;

namespace BattleLib.Interfaces
{
    public interface ICommandReceiver
    {
        void ClientExit(AbstractClient source);
        void ExecMove(AbstractClient source, Move move, int targetId);
        void ExecChange(AbstractClient source, ICharakter charakter);
    }
}
