using System.Collections.Generic;
using BattleMode.Entities.BattleState;
using BattleMode.Entities.BattleState.Commands;
using GameEngine.TypeRegistry;

namespace PokemonGame.Rules
{
    [GameService(typeof(ICommandScheduler))]
    public class DummyScheduler : ICommandScheduler
    {
        private readonly List<ICommand> _commands = new List<ICommand>();

        public void AppendCommand(ICommand command)
        {
            _commands.Add(command);
        }

        public void ClearCommands()
        {
            _commands.Clear();
        }

        public IEnumerable<ICommand> ScheduleCommands()
        {
            return _commands;
        }


        public void AppendCommands(IEnumerable<ICommand> commandList)
        {
            _commands.AddRange(commandList);
        }
    }
}