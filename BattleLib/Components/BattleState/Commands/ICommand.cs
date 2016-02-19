using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.BattleState.Commands
{
    public enum CommandType
    {
        Exit,
        Change,
        Item,
        Move
    }

    public interface ICommand
    {
        ClientIdentifier Source { get; }
        CommandType Type { get; }
        int Priority { get; }
        void Execute(IBattleRules rules, BattleData data);
    }
}
