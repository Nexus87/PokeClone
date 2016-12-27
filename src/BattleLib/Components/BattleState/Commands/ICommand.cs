
namespace BattleMode.Core.Components.BattleState.Commands
{
    public interface ICommand
    {
        ClientIdentifier Source { get; }
        int Priority { get; }
        void Execute(CommandExecuter executer);
    }
}
