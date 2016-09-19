using BattleLib.Components.BattleState;
using BattleLib.Components.BattleState.Commands;
using System.Collections.Generic;

namespace PokemonGame.Rules
{
    public class DummyScheduler : ICommandScheduler
    {
        private List<ICommand> commands = new List<ICommand>();

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


        public void AppendCommands(IEnumerable<ICommand> commands)
        {
            this.commands.AddRange(commands);
        }
    }
}