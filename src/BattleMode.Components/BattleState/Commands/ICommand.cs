using BattleMode.Shared;

namespace BattleMode.Components.BattleState.Commands
{
    public interface ICommand
    {
        ClientIdentifier Source { get; }
        int Priority { get; }
        void Execute(CommandExecuter executer);
    }
}
