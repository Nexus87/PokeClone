using BattleLib.Components.BattleState.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib
{
    public class Gen1CommandScheduler : ICommandScheduler
    {
        List<ICommand> commands = new List<ICommand>();

        public void AppendCommand(ICommand command)
        {
            commands.Add(command);
        }

        public void ClearCommands()
        {
            commands.Clear();
        }

        public IEnumerable<ICommand> ScheduleCommands()
        {
            return commands;
        }
    }
}
