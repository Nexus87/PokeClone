using Base;

namespace BattleLib
{
    public interface IBattleRules
    {
        bool CanEscape();
        bool CanChange();
        bool ExecMove(ICharacter source, Move move, ICharacter target);
    }
}
