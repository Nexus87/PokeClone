using Base;

namespace BattleLib.Interfaces
{
    public interface ICommandReceiver
    {
        void ClientExit(AbstractClient source);
        void ExecMove(AbstractClient source, Move move, int targetId);
        void ExecChange(AbstractClient source, ICharacter character);
    }
}
