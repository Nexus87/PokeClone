
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
        void Execute(CommandExecuter executer, BattleData data);
    }
}
