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
        void Execute(ICommandReceiver receiver);
    }


}
