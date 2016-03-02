using Base;

namespace BattleLib.Components.BattleState
{
    public interface IBattleState
    {
        IBattleState Update(BattleData data);

        void SetCharacter(ClientIdentifier id, Pokemon pkmn);
        void SetMove(ClientIdentifier id, Move move);
        void SetItem(ClientIdentifier id, Item item);
    }
}
