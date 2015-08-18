using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Interfaces
{
    public enum CommandType
    {
        Exit,
        Change,
        Item,
        Move
    }

    public interface IClientCommand
    {
        CommandType Type{ get; }
        void execute(CommandReceiver receiver);
    }


}
