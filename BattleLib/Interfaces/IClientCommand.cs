using BattleLib.Components.BattleState;
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
        int Priority { get; }
        void Execute(IBattleRules rules, BattleData data);
        void Execute(ICommandReceiver receiver);
        void Execute();
    }


}
