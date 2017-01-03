using BattleMode.Shared;

namespace BattleMode.Entities.BattleState.Commands
{
    public interface ICommand
    {
        ClientIdentifier Source { get; }
        int Priority { get; }
        void Execute(CommandExecuter executer);
    }
}
